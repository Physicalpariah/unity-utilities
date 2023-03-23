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
	[SerializeField] private LocalisableField m_text;
	[SerializeField] private TMP_Text m_label;
	// Initalisation Functions

	// Unity Callbacks
	private void OnEnable() {
		if (m_label == null) { m_label = GetComponent<TMP_Text>(); }
		m_label.text = Localisation.Get(m_text.m_value);
	}
	// Public Functions

	// Private Functions


}