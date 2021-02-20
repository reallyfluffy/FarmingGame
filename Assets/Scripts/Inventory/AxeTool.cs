using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AxeBehaviour")]
public class AxeTool : ItemBehaviour
{	
	public override void Use(FloorTile _hoveredTile)
	{
		Game.m_Me.WorldManager.Grid.ChopTreeOnTile(_hoveredTile);
	}
}
