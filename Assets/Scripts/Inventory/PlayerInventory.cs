using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
	
	private List<Item> m_heldItems = new List<Item>();
	private int m_numItems;
	private int m_coins;

	public Item m_equippedItem { get; private set; }
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

		int index = GetIndexForItemType(type);

		if (index >= m_numItems || index == -1)
			return;

		Item selectedItem = m_heldItems[index];

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

		m_equippedItem.Use(_hoveredTile);
	}

	private int GetIndexForItemType(ItemDatabase.ItemType type)
	{
		for(int i = 0; i < m_numItems; i++)
		{
			if (m_heldItems[i].ItemData.ItemType == type)
				return i;
		}

		return -1;
	}

	public void AddItem(ItemDatabase.ItemType _type)
	{
		//do we already own at least one of this type?
		foreach(Item heldItem in m_heldItems)
		{
			if (heldItem.ItemData.ItemType == _type)
			{
				heldItem.AddNumHeld(1);
				UIManager.m_Me.InventoryUI.AddItem(heldItem, 1);
				return;
			}
		}

		Item item = Game.m_Me.ItemDataBase.CreateItemByType(_type);
		item.AddNumHeld(1);
		m_heldItems.Add(item);
		UIManager.m_Me.InventoryUI.AddItem(item, 1);
		m_numItems++;
	}

	public void RemoveAllOfItem(Item item)
	{
		if (item.NumHeld == 0)
			return;

		m_heldItems.Remove(item);
		UIManager.m_Me.InventoryUI.RemoveItem(item, item.NumHeld);
		m_numItems--;

		if (item.ItemData.ItemType == m_equippedItem.ItemData.ItemType)
			m_equippedItem = null;
	}
	
	public Item FindItemToSell()
	{
		if (m_equippedItem == null)
			return null;

		if (!m_equippedItem.ItemData.IsSellable)
			return null;

		return m_equippedItem;
	}

	public void SellEquippedItem(int _totalPrice)
	{
		m_equippedItem.SellItem();
		RemoveAllOfItem(m_equippedItem);
	}

}
