using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

	public enum TileState
	{
		Unfarmed,
		Tilled,
		Seeded
	};

	private Plant m_plant;
	private GameObject m_soil;
	private GameTooltip m_tooltip;

	public int XPos { get; private set; }
	public int ZPos { get; private set; }
	public bool Farmable { get; private set; }
	public bool Selectable { get; private set; }
	public TileObject TileObject {get; private set;}
	public TileState State {get; private set;}

	public void Init(int _x, int _z)
	{
		float xWorldPos = FloorGrid.m_minX + _x * FloorGrid.m_tileSize + FloorGrid.m_tileSize / 2f;
		float zWorldPos = FloorGrid.m_minZ + _z * FloorGrid.m_tileSize + FloorGrid.m_tileSize / 2f;
		XPos = _x;
		ZPos = _z;

		transform.position = new Vector3(xWorldPos, 0, zWorldPos);
		Farmable = true;
		Selectable = true;
		State = TileState.Unfarmed;
	}

	public void TillSoil()
	{
		if (!Farmable)
			return;

		if (State != TileState.Unfarmed)
			return;

		State = TileState.Tilled;

		if (m_soil == null)
		{
			m_soil = Game.m_Me.PlanetManager.getSoilPrefab();
			m_soil.transform.SetParent(transform);
			m_soil.transform.localPosition = new Vector3(0, 0, 0);
		}
	}

	public void AddSeed()
	{
		if (!Farmable)
			return;

		if (State != TileState.Tilled)
			return;

		State = TileState.Seeded;

		if (m_plant == null)
		{
			m_plant = Game.m_Me.PlanetManager.getPlantPrefab();
			m_plant.transform.SetParent(transform);
			m_plant.transform.localPosition = new Vector3(0, 0, 0);
		}

		m_plant.setSeeded();
	}

	public bool CutPlant()
	{
		if (State != TileState.Seeded)
			return false;
		if (m_plant.getState() != Plant.PlantState.mature)
			return false;

		State = TileState.Unfarmed;

		Destroy(m_soil);
		m_soil = null;
		Destroy(m_plant.gameObject);
		m_plant = null;
		return true;
	}

	public void AddObject(TileObject _object, bool _selecteable)
	{
		Farmable = false;
		Selectable = _selecteable;
		TileObject = _object;
		TileObject.transform.SetParent(transform);
		TileObject.transform.localPosition = new Vector3(0, 0, 0);
	}

	public void RemoveObject()
	{
		Farmable = true;
		TileObject = null;
	}

	public void SetFarmable(bool bFarmable)
	{
		Farmable = bFarmable;
	}

	public bool IsEmpty()
	{
		return TileObject == null;
	}
}
