using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemDatabase")]
public class ItemDatabase : ScriptableObject {
	public enum ItemType
	{ 
		Empty = 0, 
		HoeTool, 
		PlanterTool, 
		ScytheTool, 
		AxeTool, 
		FlowerCrop,
		Last 
	};

	[SerializeField] private ItemData[] m_items = new ItemData[(int)ItemType.Last];

	public ItemData GetItemByType(ItemType nId)
	{
		if ((int)nId > m_items.Length)
			return null;
		return m_items[(int)nId - 1];
	}
	
}
