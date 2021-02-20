using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheTool : Item
{ 
	public ScytheTool(ItemData _data) : base(_data)
	{
 
	}

	public override void Use(FloorTile _hoveredTile)
	{
		if (Game.m_Me.WorldManager.Grid.RemovePlantFromTile(_hoveredTile))
			Game.m_Me.Player.Inventory.AddItem(ItemDatabase.ItemType.FlowerCrop);
	}
}
