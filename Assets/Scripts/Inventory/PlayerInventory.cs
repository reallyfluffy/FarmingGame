using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public class InventoryItem
	{
		public int Quantity {get; private set;}
		public ItemData ItemData {get; private set;}
		public int InventorySlot {get; private set;}

		public void AddItem(int _amount)
		{
			Quantity += _amount;
		}

		public void RemoveItem(int _amount)
		{
			Quantity -= _amount;
		}

		public InventoryItem(int _amount, ItemData _data)
		{
			Quantity = _amount;
			ItemData = _data;
		}

		public void SetInventorySlot(int _index)
		{
			InventorySlot = _index;
		}
	}
	
	private Dictionary<ItemDatabase.ItemType, InventoryItem> m_heldItems = new Dictionary<ItemDatabase.ItemType, InventoryItem>();
	private int m_coins;

	public InventoryItem EquippedItem { get; private set; }
	public const int m_numItemSlots = 4;

	public void Init ()
	{
		EquippedItem = null;

		//player starts with tools
		AddItem(ItemDatabase.ItemType.HoeTool, 1);
		AddItem(ItemDatabase.ItemType.PlanterTool, 1);
		AddItem(ItemDatabase.ItemType.ScytheTool, 1);
		AddItem(ItemDatabase.ItemType.AxeTool, 1);
	}

	void Update()
	{
		ItemDatabase.ItemType type = UIManager.m_Me.InventoryUI.GetItemTypeForKeypress();
		if (type == ItemDatabase.ItemType.Empty)
			return;

		if (!m_heldItems.ContainsKey(type))
			return;

		InventoryItem selectedItem = m_heldItems[type];

		if (EquippedItem == selectedItem)
			return;
		if (selectedItem == null)
			return;

		EquippedItem = selectedItem;
		UIManager.m_Me.InventoryUI.UpdateEquippedItem(selectedItem);
	}

	public void UseTool(FloorTile _hoveredTile)
	{
		if (EquippedItem == null)
			return;

		EquippedItem.ItemData.ItemBehaviour.Use(_hoveredTile);
	}

	public void AddItem(ItemDatabase.ItemType _type, int _quantity)
	{
		//do we already own at least one of this type?
		if(m_heldItems.TryGetValue(_type, out InventoryItem item))
		{
			item.AddItem(_quantity);
			UIManager.m_Me.InventoryUI.AddItem(item, _quantity);
			return;
		}

		item = new InventoryItem(_quantity, Game.m_Me.ItemDataBase.GetItemByType(_type));
		m_heldItems[_type] = item;
		UIManager.m_Me.InventoryUI.AddItem(item, _quantity, true);
	}

	public void RemoveItem(InventoryItem _item, int _quantity)
	{
		if (!m_heldItems.ContainsKey(_item.ItemData.ItemType))
			return;

		UIManager.m_Me.InventoryUI.RemoveItem(_item, _quantity);
		if(_item.Quantity == 0)
		{
			m_heldItems.Remove(_item.ItemData.ItemType);

			if (_item.ItemData.ItemType == EquippedItem.ItemData.ItemType)
				EquippedItem = null;
		}
	}
	
	public InventoryItem FindItemToSell()
	{
		if (EquippedItem == null)
			return null;

		if (!EquippedItem.ItemData.IsSellable)
			return null;

		return EquippedItem;
	}

	public void SellEquippedItem(int _totalPrice)
	{
		EquippedItem.ItemData.ItemBehaviour.SellItem();
		RemoveItem(EquippedItem, EquippedItem.Quantity);
	}

	public int GetQuantity(ItemDatabase.ItemType _type)
	{
		if(m_heldItems.TryGetValue(_type, out InventoryItem item))
		{
			return item.Quantity;
		}
		return 0;
	}

}
