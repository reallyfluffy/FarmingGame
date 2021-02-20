using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTooltip : MonoBehaviour {

	private SpriteRenderer m_icon; //at the moment this is not used, but we could add tooltips to show which tool to use on items
	private TextMesh m_instruction;
	private MeshRenderer m_textRenderer;

	public void init (Sprite _icon = null, string _instruction = null)
	{
		m_icon = transform.Find("Icon").GetComponent<SpriteRenderer>();
		m_textRenderer= transform.Find("Text").GetComponent<MeshRenderer>();
		m_instruction = transform.Find("Text").GetComponent<TextMesh>();

		if(_icon != null)
		{
			m_icon.sprite = _icon;
			m_textRenderer.enabled = false;
		}
		else if(_instruction != null && _instruction != "")
		{
			m_icon.enabled = false;
			m_instruction.text = _instruction;
		}
	}
}
