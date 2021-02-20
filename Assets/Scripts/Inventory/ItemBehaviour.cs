using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemBehaviour : ScriptableObject
{
	[SerializeField] private GameObject m_visualPrefab;
	public int NumHeld { get; private set; } = 0;
	public ItemData ItemData { get; private set; }

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

	public GameObject CreateItemVisual(Transform _parent)
	{
		return Instantiate(m_visualPrefab, _parent);
	}
}
