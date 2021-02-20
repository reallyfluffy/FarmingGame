using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private float m_smoothTime = 0.3f;
	[SerializeField] private Vector3 m_offset = new Vector3(0f, 5f, -5f);
	
	private Vector3 m_velocity = Vector3.zero;
	private Transform m_target = null;
	
	void Start()
	{
		m_target = Game.m_Me.Player.transform;
	}

	void LateUpdate()
	{
		if(m_target != null)
		{
			Vector3 targetPos = m_target.position + m_offset;
			transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref m_velocity, m_smoothTime);
		}
	}
}
