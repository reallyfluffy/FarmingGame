using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeTool : Item
{
	public HoeTool(ItemData _data) : base(_data)
	{
 
	}

	public override void Use(FloorTile _hoveredTile)
	{
		Game.m_Me.WorldManager.Grid.TillSoilOnTile(_hoveredTile);
	}
}
