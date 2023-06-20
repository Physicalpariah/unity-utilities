//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_LocalisationStringValidator : MonoBehaviour {
	// Properties
	[SerializeField] private LocalisableField m_text;
	[SerializeField] private Text m_label;

	// Initalisation Functions

	// Unity Callbacks
	private void OnEnable() {
		m_label.text = Localisation.UseKey(m_text.m_value);


	}

	// Public Functions

	// Private Functions

}