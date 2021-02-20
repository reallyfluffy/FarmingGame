using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] private InventoryUI m_inventoryUI;
	[SerializeField] private CoinUI m_coinUI;

	public InventoryUI InventoryUI => m_inventoryUI;
	public CoinUI CoinUI => m_coinUI;

	public static UIManager m_Me = null;

	void Awake()
	{
		if(m_Me != null)
		{
			Debug.LogError("Instance of UIManager already exists");
			Destroy(gameObject);
			return;
		}

		m_Me = this;
	}
}
