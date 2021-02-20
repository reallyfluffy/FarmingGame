using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterTool : Item
{
	public PlanterTool(ItemData _data) : base(_data)
	{
 
	}

	public override void Use(FloorTile _hoveredTile)
	{
	   Game.m_Me.WorldManager.Grid.AddSeedToTile(_hoveredTile);
	}
}
