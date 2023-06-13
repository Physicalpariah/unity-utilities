//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIMediaQuery : MonoBehaviour {
	// Dependencies

	// Properties
	[SerializeField] private List<MediaQuery> m_queries;
	private n_mediaOrientation m_orientation;

	// Initalisation Functions
	private void OnEnable() {
		Repaint();
	}
	// Unity Callbacks
	private void Update() {
		bool repaints = false;

		if (DeviceUtils.isScreenPortrait != OrientationIsPortrait()) { repaints = true; }
		if (repaints == false) { return; }

		Repaint();
	}

	private void Repaint() {

		SetOrientation();

		MediaQuery activeOrientationQuery = null;


		foreach (MediaQuery query in m_queries) {
			query.Deactivate();
			if (m_orientation == query.m_trigger.m_orientation) {
				foreach (n_deviceType typ in query.m_trigger.m_devices) {
					if (DeviceUtils.m_deviceType == typ) {
						activeOrientationQuery = query;
					}
				}
			}
		}

		if (activeOrientationQuery != null) {
			activeOrientationQuery.Activate();
		}
	}


	// Public Functions

	// Private Functions
	private void SetOrientation() {
		if (DeviceUtils.isScreenPortrait) {
			m_orientation = n_mediaOrientation.portrait;
		}
		else {
			m_orientation = n_mediaOrientation.landscape;
		}
	}

	private bool OrientationIsPortrait() {
		if (m_orientation == n_mediaOrientation.portrait) {
			return true;
		}
		else {
			return false;
		}
	}
}


[Serializable]
public class MediaQuery {
	public string m_name;
	public MediaTrigger m_trigger;
	public List<MediaAction> m_actions;



	public void Activate() {
		LogUtils.LogPriority("Activating Query: " + m_name);
		foreach (MediaAction act in m_actions) {
			act.Apply();
		}
	}

	public void Deactivate() {
	}
}

[Serializable]
public struct MediaTrigger {
	public n_mediaQueryType m_type;
	public n_mediaOrientation m_orientation;
	public List<n_deviceType> m_devices;
}

[Serializable]
public struct MediaAction {
	public string m_name;
	public GameObject m_target;
	public GameObject m_parent;

	public void Apply() {
		m_target.transform.SetParent(m_parent.transform, false);
	}
}

public enum n_mediaQueryType {
	orientation,
	screenSize,
}

public enum n_mediaOrientation {
	landscape,
	portrait,
}


