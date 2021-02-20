using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrid : MonoBehaviour{

	private FloorTile[,] m_grid;
	private GameObject m_highlightObject;
	private FloorTile m_highlightedTile;
	private int m_gridWidth;
	private int m_gridHeight;

	public const float m_minX = 3f;
	private const float m_maxX = 27f;
	public const float m_minZ = -27f;
	private const float m_maxZ = -3f;

	public const float m_tileSize = 1f;

	public void Init(GameObject _highlightPrefab)
	{
		m_highlightObject = Object.Instantiate(_highlightPrefab);
		m_highlightObject.SetActive(false);
		m_gridWidth = Mathf.RoundToInt(m_maxX - m_minX / m_tileSize);
		m_gridHeight = Mathf.RoundToInt(m_maxZ - m_minZ / m_tileSize); 

		m_grid = new FloorTile[m_gridWidth, m_gridHeight];
		GameObject tileObject;

		for (int i = 0; i < m_gridWidth; i++)
		{
			for (int j = 0; j < m_gridHeight; j++)
			{
				tileObject = new GameObject("floorTile_" + i + "_" + j);
				tileObject.transform.SetParent(transform);
				m_grid[i, j] = tileObject.AddComponent<FloorTile>();
				m_grid[i, j].Init(i, j);
			}
		}
	}

	public void AddObjectToTile(TileObject _tileObject, int _x, int _y, bool _selectable)
	{
		if(_x > m_gridWidth -1 || _y > m_gridHeight - 1)
		{
			Debug.LogError("Grid position out of bounds");
			return;
		}

		FloorTile tile = m_grid[_x,_y];
		if (tile == null)
			return;

		tile.AddObject(_tileObject, _selectable);
	}

	public void AddObjectToRandomTile(TileObject _tileObject)
	{
		FloorTile tile = null; 
		int tries = 0;

		//try to find a free tile to use
		while(tries < 100)
		{
			tile = m_grid[Random.Range(0, m_gridWidth), Random.Range(0, m_gridHeight)];
			if (tile != null && tile.IsEmpty())
				break;

			tries++;
		}

		if (tile == null)
			return;

		tile.AddObject(_tileObject, true);
	}

	public void HighlightTile(FloorTile _tile)
	{
		if (_tile == m_highlightedTile)
			return;

		if (!_tile.Selectable)
			return;

		m_highlightedTile = _tile;

		if (_tile == null)
			m_highlightObject.SetActive(false);
		else
		{
			m_highlightObject.SetActive(true);
			m_highlightObject.transform.position = m_highlightedTile.transform.position;
		} 
	}

	public void RemoveHighlight()
	{
		if (m_highlightedTile == null)
			return;

		m_highlightObject.SetActive(false);
		m_highlightedTile = null;
	}

	public FloorTile GetTileAtPosition(Vector3 pos)
	{
		if(pos.x < m_minX || pos.x > m_maxX || pos.z < m_minZ || pos.z > m_maxZ)
			return null;

		int xIndex = Mathf.FloorToInt((pos.x - m_minX) / m_tileSize);
		int yIndex = Mathf.FloorToInt((pos.z - m_minZ) / m_tileSize);
		return m_grid[xIndex, yIndex];
	}

	public void AddSeedToTile(FloorTile _tile)
	{
		_tile.AddSeed();
	}

	public void TillSoilOnTile(FloorTile _tile)
	{
		_tile.TillSoil();
	}

	public bool RemovePlantFromTile(FloorTile _tile)
	{
		return _tile.CutPlant();
	}

	public void ChopTreeOnTile(FloorTile _tile)
	{
		//Would be better to just call use() on the tile's objects, maybe add some more specific item classes later!
		TileObject obj = _tile.TileObject;

		if (obj == null)
			return;

		TreeObject tree = obj as TreeObject;
		if (tree == null)
			return;

		if (tree.Chop())
			_tile.RemoveObject();
	}
}
