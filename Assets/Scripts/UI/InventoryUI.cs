using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	private ItemSlot[] m_itemSlots;
	private int m_equippedIndex = 0;

	public void Awake()
	{
		m_itemSlots = GetComponentsInChildren<ItemSlot>();
	}

	public void UpdateEquippedItem(PlayerInventory.InventoryItem _tool)
	{
		//update the ui to show which tool has been equipped
		m_equippedIndex = _tool.InventorySlot;
		m_itemSlots[_tool.InventorySlot].SetActive(true);

		for(int i = 0; i < m_itemSlots.Length; i++)
		{
			if (i == m_equippedIndex)
				continue;

			m_itemSlots[i].SetActive(false);
		}
	}

	public void AddItem(PlayerInventory.InventoryItem _item, int _num, bool _isNew = false)
	{
		ItemSlot slot = _isNew ? GetNextEmptySlotWithItem(_item) : GetSlotForItem(_item.ItemData.ItemType);
  
		if (slot == null)
			return;

		if (slot.NumHeld == 0)
			slot.InitWithItem(_item.ItemData.Sprite, _item.ItemData.ItemType);

		slot.AddNumHeld(_num, _item.ItemData.IsStackable);
	}

	public void RemoveItem(PlayerInventory.InventoryItem _item, int _num)
	{
		ItemSlot slot = m_itemSlots[_item.InventorySlot];

		if (slot.NumHeld == 0)
			return;

		slot.RemoveNumHeld(_num, _item.ItemData.IsStackable);
	}

	public ItemDatabase.ItemType GetItemTypeForKeypress()
	{
		for(int i = 0; i < m_itemSlots.Length; i++)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1 + i))
			{
				return m_itemSlots[i].ItemType;
			}
		}

		return ItemDatabase.ItemType.Empty;
	}

	private ItemSlot GetNextEmptySlotWithItem(PlayerInventory.InventoryItem _item)
	{
		ItemSlot pSlot;

		for (int i = 0; i < m_itemSlots.Length; i++)
		{
			pSlot = m_itemSlots[i];

			if (pSlot.IsEmpty())
			{
				_item.SetInventorySlot(i);
				return pSlot;
			}
		}

		return null;
	}

	private ItemSlot GetSlotForItem(ItemDatabase.ItemType _type)
	{
		foreach (ItemSlot pSlot in m_itemSlots)
		{
			if (pSlot.IsEmpty())
				continue;

			if (pSlot.ItemType == _type)
				return pSlot;
		}

		return null;
	}
}
