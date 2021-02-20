using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : TileObject {
	private Animator m_animator;
	private GameTooltip m_tooltip;

	void Start ()
	{
		m_animator = GetComponent<Animator>();
	}
	
	void Update ()
	{
		if (!m_bCanInteract)
			return;

		//try to sell equipped item
		if (Input.GetKeyDown(KeyCode.X))
			SellItemForCoins();
	}

	private void SellItemForCoins()
	{
		Item item = Game.m_Me.Player.Inventory.FindItemToSell();

		if (item == null)
			return;

		int totalCoins = item.NumHeld * item.ItemData.Value;
 
		Game.m_Me.Player.Inventory.SellEquippedItem(totalCoins);
		Game.m_Me.Player.UpdateCoins(totalCoins);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Player>() == null)
			return;

		m_bCanInteract = true;
		m_animator.SetTrigger("PlayerEnter");

		//make a new tooltip
		if (m_tooltip == null)
		{
			m_tooltip = Object.Instantiate(Game.m_Me.WorldManager.TooltipPrefab);
			m_tooltip.init(null, "Press X");
			m_tooltip.transform.SetParent(transform);
			m_tooltip.transform.localPosition = new Vector3(0, 2, 0);
		}
		else
			m_tooltip.gameObject.SetActive(true);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<Player>() == null)
			return;

		//player has walked away
		m_tooltip.gameObject.SetActive(false);
		m_bCanInteract = false;
		m_animator.SetTrigger("PlayerExit");
	}
}
