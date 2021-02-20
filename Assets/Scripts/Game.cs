using UnityEngine; 

class Game : MonoBehaviour
{
	public WorldManager WorldManager => m_worldManager;
	public PlantManager PlanetManager => m_plantManager;
	public ItemDatabase ItemDataBase => m_itemDatabase;
	public Player Player {get; private set;}

	[SerializeField] private ItemDatabase m_itemDatabase;
	[SerializeField] private WorldManager m_worldManager;
	[SerializeField] private PlantManager m_plantManager;
	[SerializeField] private Player m_playerPrefab;
	[SerializeField] private Transform m_playerSpawnPos;
	
	public static Game m_Me = null;

	void Awake()
	{
		if(m_Me != null)
		{
			Debug.LogError("There is already an instance of Game");
			Destroy(gameObject);
			return;
		}

		m_Me = this;
		Player = Instantiate(m_playerPrefab, transform);
		Player.transform.position = m_playerSpawnPos.position;
	}
}

