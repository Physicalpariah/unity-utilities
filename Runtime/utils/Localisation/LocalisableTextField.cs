//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Text))]
public class LocalisableTextField : MonoBehaviour {
	// Dependencies

	// Properties
	private string m_lowerText;
	public LocalisableField m_text;
	public TMP_Text m_label;
	// Initalisation Functions

	// Unity Callbacks
	private void OnEnable() {
		if (string.IsNullOrWhiteSpace(m_text.m_value)) { return; }
		if (m_label == null) { m_label = GetComponent<TMP_Text>(); }
		if (string.IsNullOrWhiteSpace(m_lowerText)) { m_lowerText = m_text.m_value.ToLower(); }
		m_label.text = Localisation.Get(m_lowerText);
	}
	// Public Functions

	// Private Functions


}