//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// goes onto main canvas.
// spawns icons, sets up tracking.

public class ScreenSpaceIconHandler : MonoBehaviour {

	// Properties
	private List<UI_ScreenSpaceIcon> m_spawnedIcons;
	[SerializeField] private Transform m_holder;
	[SerializeField] private bool m_setsOnEnable;

	private static ScreenSpaceIconHandler m_instance;
	public static ScreenSpaceIconHandler Instance {
		get {
			return m_instance;
		}
	}

	public static void SetInstance(ScreenSpaceIconHandler instance) {
		m_instance = instance;
	}

	public static void ClearInstance() {
		m_instance = null;
	}

	private bool m_portrait {
		get {
			if (Screen.height > Screen.width) {
				return true;
			}
			else {
				return false;
			}
		}
		set {
			m_portrait = value;
		}
	}

	public float m_xEdgeBuffer {
		get {
			if (m_portrait) {
				return 80;
			}
			else {
				return 10;
			}
		}
		set {
			m_xEdgeBuffer = value;
		}
	}
	public float m_topEdgeBuffer {
		get {
			if (m_portrait) {
				return 80;
			}
			else {
				return 10;
			}
		}
		set {
			m_xEdgeBuffer = value;
		}
	}
	public float m_bottomEdgeBuffer {
		get {
			if (m_portrait) {
				return 300;
			}
			else {
				return 10;
			}
		}
		set {
			m_xEdgeBuffer = value;
		}
	}

	// Initalisation Functions

	private void OnEnable() {
		if (m_setsOnEnable) {
			SetInstance(this);
		}

		Clear();
	}



	// Public Functions
	public void AddIcon(GameObject icon, ScreenSpaceObjectTracker tracker) {
		GameObject obj = ObjectUtils.SpawnUIWithData(icon, m_holder.gameObject, gameObject, tracker);
		obj.transform.SetAsFirstSibling();

		UI_ScreenSpaceIcon ico = obj.GetComponent<UI_ScreenSpaceIcon>();
		m_spawnedIcons.Add(ico);

		tracker.SetSpawnedIcon(ico);
	}


	public void RemoveIcon(UI_ScreenSpaceIcon icon) {
		m_spawnedIcons.Remove(icon);
		if (icon != null) {
			ObjectUtils.DespawnItem(icon.gameObject);
		}
	}

	// Private Functions
	private void Clear() {

		if (m_spawnedIcons == null) {
			m_spawnedIcons = new List<UI_ScreenSpaceIcon>();
		}

		for (int a = 0; a < m_spawnedIcons.Count; a++) {
			RemoveIcon(m_spawnedIcons[a]);
		}
	}
}

