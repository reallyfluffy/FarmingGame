using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour {

	[SerializeField] private Plant m_plantPrefab;

	[SerializeField] private GameObject m_soilPrefab;

	public Plant getPlantPrefab()
	{
		GameObject pPlantObject = Object.Instantiate(m_plantPrefab.gameObject);
		return pPlantObject.GetComponent<Plant>();
	}

	public GameObject getSoilPrefab()
	{
		GameObject pSoilObject = Object.Instantiate(m_soilPrefab);
		return pSoilObject;
	}
	
}
