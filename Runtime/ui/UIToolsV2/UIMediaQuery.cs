//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Sirenix.OdinInspector;


public class UIMediaQuery : MonoBehaviour {
	// Dependencies

	// Properties
	[SerializeField]
	private List<MediaQuery> m_queries;
	private n_mediaOrientation m_orientation;
	public Dictionary<string, string> oh_damn;

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

		foreach (MediaAction act in m_actions) {
			act.Apply();
		}
	}

	public void Deactivate() {

	}
}

[Serializable]
public struct MediaTrigger {
	public n_mediaOrientation m_orientation;
	public List<n_deviceType> m_devices;
}

[Serializable]
public struct MediaAction {
	public string m_name;
	public n_mediaActionType m_type;
	[Header("Reparenting")]
	[HideIf("m_type", n_mediaActionType.layoutElement)]
	public GameObject m_target;
	[HideIf("m_type", n_mediaActionType.layoutElement)]
	public GameObject m_parent;
	[Header("Layout Element")]
	[HideIf("m_type", n_mediaActionType.reparent)]
	public LayoutElement m_group;
	[HideIf("m_type", n_mediaActionType.reparent)]
	public Vector2 m_minSize;
	[HideIf("m_type", n_mediaActionType.reparent)]
	public Vector2 m_prefSize;

	public void Apply() {
		switch (m_type) {
			case (n_mediaActionType.reparent): {
					ApplyParenting();
					break;
				}
			case (n_mediaActionType.layoutElement): {
					ApplyLayout();
					break;
				}
		}
	}

	private void ApplyParenting() {
		if (m_target == null) { return; }
		if (m_parent == null) { return; }
		m_target.transform.SetParent(m_parent.transform, false);
	}

	private void ApplyLayout() {
		if (m_group == null) { return; }
		m_group.minHeight = m_minSize.y;
		m_group.minWidth = m_minSize.x;
		m_group.preferredHeight = m_prefSize.y;
		m_group.preferredWidth = m_prefSize.x;
	}
}



public enum n_mediaActionType {
	reparent,
	layoutElement,
}

public enum n_mediaOrientation {
	none,
	landscape,
	portrait,
}

