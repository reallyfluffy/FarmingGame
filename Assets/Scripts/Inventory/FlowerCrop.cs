using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCrop : Item
{
	public FlowerCrop(ItemData _data) : base(_data)
	{
 
	}

	public override void Use(FloorTile _hoveredTile)
	{
		// TODO use functionality
	}

	public override void SellItem()
	{
		GameObject pObject = ItemData.CreateItemVisual(Game.m_Me.WorldManager.Shop.transform);

		//rotate it a bit so it falls into the 'shop' nicely
		const float zRot = 45f;
		const float yOffset = 2f;
		Quaternion pQuat = Quaternion.Euler(new Vector3(0f, 0f, zRot));
		pObject.transform.rotation = pQuat;

		Vector3 vOffset = new Vector3(0f, yOffset, 0f);
		pObject.transform.position += vOffset;
	}
}
