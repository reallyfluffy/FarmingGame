using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {

    private Text m_txtCoins;

	void Start ()
    {
        m_txtCoins = GetComponent<Text>();
	}
	
	public void UpdateCoins(int _coins)
    {
        m_txtCoins.text = "Coins: " + _coins;
    }
}
