//  Created by Matt Purchase.
//  Copyright (c) 2018 Matt Purchase. All rights reserved.
using UnityEngine;

// attached to worldspace object

public class ScreenSpaceObjectTracker : MonoBehaviour {

	// Properties
	[SerializeField] protected GameObject m_iconPrefab;
	public UI_ScreenSpaceIcon m_spawnedIcon { get; private set; }
	[SerializeField] protected bool m_activatesOnEnable = false;


	// Initalisation Functions
	private void OnEnable() {
		if (m_activatesOnEnable) {
			Activate();
		}
	}


	// Public Functions
	public virtual void Activate() {
		if (ScreenSpaceIconHandler.Instance == null) { Debug.LogError("Trying to start without an instance"); }

		ScreenSpaceIconHandler.Instance.AddIcon(m_iconPrefab, this);
	}


	public virtual void ShutDownIcon() {
		if (ScreenSpaceIconHandler.Instance == null) { Debug.LogError("Trying to shutdown without an instance"); }

		ScreenSpaceIconHandler.Instance.RemoveIcon(m_spawnedIcon);
		m_spawnedIcon = null;
	}


	public virtual void SetSpawnedIcon(UI_ScreenSpaceIcon icon) {
		m_spawnedIcon = icon;
	}


	// Private Functions

}