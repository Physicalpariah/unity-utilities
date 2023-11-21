//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIListSeperator : UIListCell {
	// Dependencies

	// Properties
	[SerializeField] private TMP_Text m_title;
	// Initalisation Functions

	// Unity Callbacks  
	public override void InitRecycleable(object[] data) {
		base.InitRecycleable(data);
		string title = (string)m_data;
		m_title.text = title;
	}

	// Public Functions

	// Private Functions

}