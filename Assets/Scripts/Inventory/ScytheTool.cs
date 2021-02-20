using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScytheBehaviour")]
public class ScytheTool : ItemBehaviour
{ 
	public override void Use(FloorTile _hoveredTile)
	{
		if (Game.m_Me.WorldManager.Grid.RemovePlantFromTile(_hoveredTile))
			Game.m_Me.Player.Inventory.AddItem(ItemDatabase.ItemType.FlowerCrop);
	}
}
