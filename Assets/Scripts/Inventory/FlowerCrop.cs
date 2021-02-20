using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FlowerBehaviour")]
public class FlowerCrop : ItemBehaviour
{
	public override void SellItem()
	{
		GameObject pObject = CreateItemVisual(Game.m_Me.WorldManager.Shop.transform);

		//rotate it a bit so it falls into the 'shop' nicely
		const float zRot = 45f;
		const float yOffset = 2f;
		Quaternion pQuat = Quaternion.Euler(new Vector3(0f, 0f, zRot));
		pObject.transform.rotation = pQuat;

		Vector3 vOffset = new Vector3(0f, yOffset, 0f);
		pObject.transform.position += vOffset;
	}
}
