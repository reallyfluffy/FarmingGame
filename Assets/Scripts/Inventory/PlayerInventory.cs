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
	private int m_numItems;
	private int m_coins;

	public InventoryItem m_equippedItem { get; private set; }
	public const int m_numItemSlots = 4;

	public void Init ()
	{
		m_equippedItem = null;
		m_numItems = 0;

		//player starts with tools
		AddItem(ItemDatabase.ItemType.HoeTool);
		AddItem(ItemDatabase.ItemType.PlanterTool);
		AddItem(ItemDatabase.ItemType.ScytheTool);
		AddItem(ItemDatabase.ItemType.AxeTool);
	}

	void Update()
	{
		ItemDatabase.ItemType type = UIManager.m_Me.InventoryUI.GetItemTypeForKeypress();
		if (type == ItemDatabase.ItemType.Empty)
			return;

		if (!m_heldItems.ContainsKey(type))
			return;

		InventoryItem selectedItem = m_heldItems[type];

		if (m_equippedItem == selectedItem)
			return;
		if (selectedItem == null)
			return;

		m_equippedItem = selectedItem;
		UIManager.m_Me.InventoryUI.UpdateEquippedItem(selectedItem);
	}

	public void UseTool(FloorTile _hoveredTile)
	{
		if (m_equippedItem == null)
			return;

		m_equippedItem.ItemData.ItemBehaviour.Use(_hoveredTile);
	}

	public void AddItem(ItemDatabase.ItemType _type)
	{
		//do we already own at least one of this type?
		if(m_heldItems.TryGetValue(_type, out InventoryItem item))
		{
			item.AddItem(1);
			UIManager.m_Me.InventoryUI.AddItem(item, 1);
			return;
		}

		item = new InventoryItem(1, Game.m_Me.ItemDataBase.GetItemByType(_type));
		m_heldItems[_type] = item;
		UIManager.m_Me.InventoryUI.AddItem(item, 1, true);
		m_numItems++;
	}

	public void RemoveAllOfItem(InventoryItem item)
	{
		if (m_heldItems[item.ItemData.ItemType].Quantity == 0)
			return;

		UIManager.m_Me.InventoryUI.RemoveItem(item, m_heldItems[item.ItemData.ItemType].Quantity);
		m_heldItems.Remove(item.ItemData.ItemType);
		
		m_numItems--;

		if (item.ItemData.ItemType == m_equippedItem.ItemData.ItemType)
			m_equippedItem = null;
	}
	
	public InventoryItem FindItemToSell()
	{
		if (m_equippedItem == null)
			return null;

		if (!m_equippedItem.ItemData.IsSellable)
			return null;

		return m_equippedItem;
	}

	public void SellEquippedItem(int _totalPrice)
	{
		m_equippedItem.ItemData.ItemBehaviour.SellItem();
		RemoveAllOfItem(m_equippedItem);
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
