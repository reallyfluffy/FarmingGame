using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
	[SerializeField] private ItemDatabase.ItemType m_itemType;
	[SerializeField] private ItemBehaviour m_itemBehaviour;
	[SerializeField] private Sprite m_sprite;
	[SerializeField] private int m_value;
	[SerializeField] private bool m_stackable;
	[SerializeField] private bool m_sellable;

	public ItemDatabase.ItemType ItemType => m_itemType;
	public ItemBehaviour ItemBehaviour => m_itemBehaviour;
	public Sprite Sprite => m_sprite;
	public int Value => m_value;
	public bool IsSellable => m_sellable;
	public bool IsStackable => m_stackable;
}
