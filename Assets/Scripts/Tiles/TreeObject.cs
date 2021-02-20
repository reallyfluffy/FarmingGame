using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : TileObject
{
	[SerializeField] private int m_health;
	private Rigidbody m_rigidBody;
	private Animator m_animator;

	void Start()
	{
		m_rigidBody = transform.GetComponent<Rigidbody>();
		m_animator = transform.GetComponent<Animator>();
	}

	public bool Chop() 
	{
		if (m_health == 0)
			return true;

		m_health--;
		m_animator.SetTrigger("ChopTree");

		if (m_health == 0)
		{
			FallDown();
			return true;
		}

		return false;
	}

	private void FallDown()
	{
		m_animator.enabled  = false;
		m_rigidBody.isKinematic = false;
		
		float thrust = 0.5f * m_rigidBody.mass;
		Vector3 playerToTree = (transform.position - Game.m_Me.Player.transform.position).normalized;
		m_rigidBody.AddForce(playerToTree * thrust, ForceMode.Impulse);
		StartCoroutine(Co_RemoveTree());
	}

	private IEnumerator Co_RemoveTree()
	{
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}
}
