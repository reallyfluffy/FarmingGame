using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
	public int NumHeld { get; private set; } = 0;
	public int InventorySlot { get; private set; }
	public ItemData ItemData { get; private set; }

	public Item()
	{
	
	}

	public Item(ItemData _data)
	{
		ItemData = _data;
	}

	public void AddNumHeld(int _num)
	{
		NumHeld += _num;
	}

	public void SetInventorySlot(int _inventoryIndex)
	{
		InventorySlot = _inventoryIndex;
	}

	public virtual void SellItem()
	{
		if(ItemData.IsSellable)
		{
			Debug.Log("Sell functionality not implemented");
		}
	}

	public virtual void Use(FloorTile _hoveredTile)
	{
		Debug.Log("Use functionality not implemented");
	}
}
