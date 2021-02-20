using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public FloorGrid Grid { get; private set; }
	public Shop Shop { get; private set; }

	[SerializeField] private GameObject m_highlightPrefab;
	[SerializeField] private Shop m_shopPrefab;
	[SerializeField] private TreeObject m_treePrefab;
	[SerializeField] private GameTooltip m_tooltipPrefab;

	private List<TreeObject> m_trees;
	public GameTooltip TooltipPrefab => m_tooltipPrefab;

	void Awake ()
	{
		GameObject gridObject = new GameObject("floorGrid");
		gridObject.transform.SetParent(transform);
		Grid = gridObject.AddComponent<FloorGrid>();
		Grid.Init(m_highlightPrefab);

		InitWorldObjects();
	}

	void InitWorldObjects()
	{
		Shop = Object.Instantiate(m_shopPrefab);
		Grid.AddObjectToTile(Shop, 10, 15, false);

		TreeObject tree;
		int numTrees = Mathf.CeilToInt(Random.Range(3, 10));
		m_trees = new List<TreeObject>();

		for(int i = 0; i < numTrees; i++)
		{
			tree = Object.Instantiate(m_treePrefab);

			m_trees.Add(tree);
			Grid.AddObjectToRandomTile(tree);
		}
	}
}
