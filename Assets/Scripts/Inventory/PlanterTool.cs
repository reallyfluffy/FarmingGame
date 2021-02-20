using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlanterBehaviour")]
public class PlanterTool : ItemBehaviour
{
	public override void Use(FloorTile _hoveredTile)
	{
	   Game.m_Me.WorldManager.Grid.AddSeedToTile(_hoveredTile);
	}
}
