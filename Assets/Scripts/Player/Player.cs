using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public PlayerInventory Inventory { get; private set; }
	public int Coins { get; private set; }

	void Start ()
	{
		Inventory = transform.Find("Inventory").GetComponent<PlayerInventory>();
		Inventory.Init();
	}

	public void UpdateCoins(int _amt)
	{
		Coins += _amt;
		UIManager.m_Me.CoinUI.UpdateCoins(Coins);
	}

}
