using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HoeBehaviour")]
public class HoeTool : ItemBehaviour
{	public override void Use(FloorTile _hoveredTile)
	{
		Game.m_Me.WorldManager.Grid.TillSoilOnTile(_hoveredTile);
	}
}
