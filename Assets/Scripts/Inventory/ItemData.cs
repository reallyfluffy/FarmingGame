using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
	[SerializeField] private ItemDatabase.ItemType m_itemType;
	[SerializeField] private Sprite m_sprite;
	[SerializeField] private GameObject m_itemVisualPrefab;
	[SerializeField] private int m_value;
	[SerializeField] private bool m_stackable;
	[SerializeField] private bool m_sellable;

	public ItemDatabase.ItemType ItemType => m_itemType;
	public Sprite Sprite => m_sprite;
	public int InventorySlot { get; private set; }
	public int NumHeld { get; private set; }
	public int Value => m_value;
	public bool IsSellable => m_sellable;
	public bool IsStackable => m_stackable;

	public Item CreateItem()
	{
		switch(m_itemType)
		{
			default : return new Item(this);
			case ItemDatabase.ItemType.AxeTool : return new AxeTool(this);
			case ItemDatabase.ItemType.HoeTool : return new HoeTool(this);
			case ItemDatabase.ItemType.ScytheTool : return new ScytheTool(this);
			case ItemDatabase.ItemType.FlowerCrop : return new FlowerCrop(this);
			case ItemDatabase.ItemType.PlanterTool : return new PlanterTool(this);
		}
	}

	public GameObject CreateItemVisual(Transform _parent)
	{
		return Instantiate(m_itemVisualPrefab, _parent);
	}
}
