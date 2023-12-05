//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
// using Sirenix.OdinInspector;


public class UIMediaQuery : MonoBehaviour {
	// Dependencies

	// Properties


	[SerializeField] private List<MediaQuery> m_queries;

	// Initalisation Functions
	private void OnEnable() {
		UIMediaQueryHandler.e_mediaChanged += Repaint;
		LogUtils.LogPriority(UIMediaQueryHandler.m_currentTrigger);
		Repaint(UIMediaQueryHandler.m_currentTrigger);
	}

	private void OnDestroy() {
		Unsubscribe();
	}

	private void OnApplicationQuit() {
		Unsubscribe();
	}

	private void OnDisable() {
		Unsubscribe();
	}

	private void Unsubscribe() {
		UIMediaQueryHandler.e_mediaChanged -= Repaint;
	}

	// Unity Callbacks
	private void Repaint(MediaQueryTrigger trigger) {
		MediaQuery activeOrientationQuery = null;
		foreach (MediaQuery query in m_queries) {
			query.Deactivate();
			if (trigger.m_orientation == query.m_trigger.m_orientation || query.m_trigger.m_orientation == n_mediaOrientation.none) {
				foreach (n_deviceType typ in query.m_trigger.m_devices) {
					if (trigger.m_devices.Contains(typ)) {
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
}


[Serializable]
public class MediaQuery {
	public MediaQueryTrigger m_trigger;
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
public struct MediaAction {
	public string m_name;
	public n_mediaActionType m_type;
	// [Header("Reparenting")]
	public GameObject m_target;
	public GameObject m_parent;
	// [Header("Layout Element")]
	public LayoutElement m_group;
	public Vector2 m_minSize;
	public Vector2 m_prefSize;
	// [Header("Visibility")]
	public GameObject m_visibilityTarget;
	public bool m_isShown;

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
			case (n_mediaActionType.toggleVisibility): {
					ApplyVisibility();
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

	private void ApplyVisibility() {
		m_visibilityTarget.gameObject.SetActive(m_isShown);
	}
}



public enum n_mediaActionType {
	reparent,
	layoutElement,
	toggleVisibility,
}

public enum n_mediaOrientation {
	none,
	landscape,
	portrait,
}


