using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float m_speed = 6f;

	private Vector3 m_movement;
	private Vector3 m_rotation;
	private Rigidbody m_playerRigidBody;
	private Animator m_animator;
	private FloorTile m_playerTile;
	private FloorTile m_hoveredTile;
	private bool m_isMoving = false;
	private float m_horizontalInput = 0f;
	private float m_verticalInput = 0f;

	void Awake ()
	{
		m_playerRigidBody = GetComponent<Rigidbody>();
		m_animator = GetComponentInChildren<Animator>();
	}
	
	void FixedUpdate ()
	{
		UpdateKeyboardInput();	
	}

	void Update ()
	{
		UpdateMouseInput(); 
	}

	public FloorTile GetHoveredTile()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		//check if we moused over the floor
		if (Physics.Raycast(camRay, out floorHit, 1000f, 1 << LayerMask.NameToLayer("Floor")))
		{
			FloorTile selectedTile = Game.m_Me.WorldManager.Grid.GetTileAtPosition(floorHit.point);

			if (selectedTile == null)
				return null;

			//ignore tiles that are not near to the player
			if (Mathf.Abs(m_playerTile.XPos - selectedTile.XPos) > 2 || Mathf.Abs(m_playerTile.ZPos - selectedTile.ZPos) > 2)
				return null;

			return selectedTile;
		}

		return null;
	}

	private void UpdateKeyboardInput()
	{
		//get input from keyboard, -1, 0 or 1
		m_horizontalInput= Input.GetAxisRaw("Horizontal");
		m_verticalInput = Input.GetAxisRaw("Vertical");

		m_isMoving = (Mathf.Abs(m_horizontalInput) > float.Epsilon || Mathf.Abs(m_verticalInput) > float.Epsilon);

		if(m_isMoving)
		{
			if(m_hoveredTile != null)
			{
				Game.m_Me.WorldManager.Grid.RemoveHighlight();
				m_hoveredTile = null;
			}
			Move();
		}

		m_animator.SetBool("isWalking", m_isMoving);
		m_playerTile = Game.m_Me.WorldManager.Grid.GetTileAtPosition(transform.position);
	}

	private void UpdateMouseInput()
	{
		if (m_isMoving)
			return;

		m_hoveredTile = GetHoveredTile();

		if (m_hoveredTile == null)
			return;

		Game.m_Me.WorldManager.Grid.HighlightTile(m_hoveredTile);

		if (!Input.GetMouseButtonDown(0))
			return;

		//make player face in the direction of the tile they clicked
		Vector3 playerToTile = m_hoveredTile.transform.position - transform.position;
		m_playerRigidBody.MoveRotation(Quaternion.LookRotation(playerToTile.normalized));

		Game.m_Me.Player.Inventory.UseTool(m_hoveredTile);
	}

	private void Move()
	{
		m_movement.Set(m_horizontalInput, 0.0f, m_verticalInput);
		m_movement = m_movement.normalized * m_speed * Time.deltaTime;
		m_playerRigidBody.MovePosition(transform.position + m_movement);

		m_rotation.Set(m_horizontalInput, 0.0f, m_verticalInput);
		m_rotation.Normalize();
		m_playerRigidBody.MoveRotation(Quaternion.LookRotation(m_rotation));
	}
}
