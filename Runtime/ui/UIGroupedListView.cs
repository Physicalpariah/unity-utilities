//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIGroupedListView<T> : UIListView<T> {
	// Dependencies

	// Properties
	[SerializeField] protected GameObject m_seperatorPrefab;

	public CoreEvent e_allGroupedItemsSpawned;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions
	public void Initialise(Dictionary<string, List<T>> data) {
		Initialise();

		if (m_spawnsOverTime) {
			StartCoroutine(DoGroupedSpawn(data));
		}
		else {
			foreach (KeyValuePair<string, List<T>> kvp in data) {
				if (m_seperatorPrefab != null) {
					CreateCell(kvp.Key, m_holder.transform, m_seperatorPrefab);
				}
				Append(kvp.Value);
			}

			if (e_allGroupedItemsSpawned != null) {
				e_allGroupedItemsSpawned();
			}
		}
	}

	private IEnumerator DoGroupedSpawn(Dictionary<string, List<T>> data) {
		WaitForSeconds sec = new(m_spawnIntervalInSeconds);
		foreach (KeyValuePair<string, List<T>> kvp in data) {
			if (m_seperatorPrefab != null) {
				CreateCell(kvp.Key, m_holder.transform, m_seperatorPrefab);
			}
			yield return sec;
			yield return DoSpawn(kvp.Value);
		}

		if (e_allGroupedItemsSpawned != null) {
			e_allGroupedItemsSpawned();
		}
	}

	// Private Functions

}
