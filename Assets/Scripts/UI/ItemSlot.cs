using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {

	[SerializeField] public Color m_selectedColour;
	[SerializeField] public Color m_defaultColour;

	private Image m_background;
	private Image m_icon;
	private Text m_txtNum;
	private bool m_isActive;

	public int NumHeld { get; private set; } = 0;
	public ItemDatabase.ItemType ItemType { get; private set; } = ItemDatabase.ItemType.Empty;

	void Awake ()
	{
		m_background = transform.Find("Background").GetComponent<Image>();
		m_icon = transform.Find("Icon").GetComponent<Image>();
		m_txtNum = transform.Find("NumText").GetComponent<Text>();
		m_txtNum.enabled = false;
	}
	
	public void SetActive(bool _active)
	{
		if (_active == m_isActive)
			return;

		m_isActive = _active;
		m_background.color = m_isActive ? m_selectedColour : m_defaultColour;
	}

	public void AddNumHeld(int _num, bool _stackable)
	{
		if (NumHeld == 0)
			m_icon.enabled = true;

		NumHeld += _num;

		if (_stackable)
		{
			if (!m_txtNum.enabled)
				m_txtNum.enabled = true;

			m_txtNum.text = NumHeld.ToString();
		}
	}

	public void RemoveNumHeld(int nNum, bool bStackable)
	{
		NumHeld -=nNum;

		if(bStackable)
			m_txtNum.text = NumHeld.ToString();

		SetActive(false);

		if (NumHeld > 0)
			return;
	  
		m_icon.enabled = false;
		m_txtNum.enabled = false;
		ItemType = ItemDatabase.ItemType.Empty;
	}

	public void InitWithItem(Sprite pImg, ItemDatabase.ItemType nType)
	{
		m_icon.sprite = pImg;
		ItemType = nType;
	}

	public bool IsEmpty()
	{
		return ItemType == ItemDatabase.ItemType.Empty;
	}
}
