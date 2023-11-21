//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIGroupedListView<T> : UIListView<T> {
	// Dependencies

	// Properties
	[SerializeField] protected GameObject m_seperatorPrefab;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions
	public void Initialise(Dictionary<string, List<T>> data) {
		Initialise();


		foreach (KeyValuePair<string, List<T>> kvp in data) {
			if (m_seperatorPrefab != null) {
				CreateCell(kvp.Key, m_holder.transform, m_seperatorPrefab);
			}
			Append(kvp.Value);
		}
	}

	// Private Functions

}
