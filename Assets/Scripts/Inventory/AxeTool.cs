using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTool : Item
{	
	public AxeTool(ItemData _data) : base(_data)
	{
 
	}

	public override void Use(FloorTile _hoveredTile)
	{
		Game.m_Me.WorldManager.Grid.ChopTreeOnTile(_hoveredTile);
	}
}
