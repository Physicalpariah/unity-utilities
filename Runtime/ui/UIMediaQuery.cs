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

	public string m_activeQueryName;
	public List<GameObject> m_targets;
	public List<MediaQueryTrigger> m_triggers;
	public List<MediaQuery> m_queries;
	[HideInInspector] public bool m_modern;

	// Initalisation Functions
	private void OnEnable() {
		UIMediaQueryHandler.e_mediaChanged += Repaint;
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
						m_activeQueryName = query.m_name;
					}
				}
			}
		}

		if (activeOrientationQuery != null) {
			activeOrientationQuery.Activate();
		}
	}


	// Public Functions

	public void ConvertTargets() {
		m_targets = new();
		m_triggers = new();

		for (int a = 0; a < m_queries.Count; a++) {
			if (m_triggers.Contains(m_queries[a].m_trigger) == false) {
				m_triggers.Add(m_queries[a].m_trigger);
			}
		}
		for (int a = 0; a < m_queries.Count; a++) {
			for (int b = 0; b < m_queries[a].m_actions.Count; b++) {
				if (m_targets.Contains(m_queries[a].m_actions[b].m_target) == false) {
					m_targets.Add(m_queries[a].m_actions[b].m_target);
				}
			}
		}

		SetActions();
		m_modern = true;
	}

	public void SetActions() {

		List<MediaQuery> backupQueries = new();
		for (int a = 0; a < m_queries.Count; a++) {
			backupQueries.Add(m_queries[a]);
		}


		m_queries.Clear();


		for (int b = 0; b < m_triggers.Count; b++) {
			MediaQuery query = new();
			query.m_trigger = m_triggers[b];
			query.m_actions = new();
			for (int a = 0; a < m_targets.Count; a++) {
				MediaAction act = new();
				act.SetTarget(this, a);
				query.m_actions.Add(act);
			}
			m_queries.Add(query);
		}

		for (int a = 0; a < m_queries.Count; a++) {
			if (backupQueries.Count > a) {
				m_queries[a].m_container = backupQueries[a].m_container;
				for (int b = 0; b < m_queries[a].m_actions.Count; b++) {
					if (backupQueries[a].m_actions.Count > b) {
						if (m_queries[a].m_actions[b].m_target == backupQueries[a].m_actions[b].m_target) {
							m_queries[a].m_actions[b].m_parent = backupQueries[a].m_actions[b].m_parent;
						}
					}
				}
			}
		}

		for (int a = 0; a < m_queries.Count; a++) {
			m_queries[a].m_name = m_queries[a].m_trigger.name;
		}

#if UNITY_EDITOR
		UnityEditor.EditorUtility.SetDirty(this);
#endif

	}

	public GameObject GetTargetByIndex(int id) {
		return m_targets[id];
	}

	// Private Functions
}




[Serializable]
public class MediaQuery {
	public string m_name;
	public GameObject m_container;
	public MediaQueryTrigger m_trigger;
	public List<MediaAction> m_actions;

	public void Activate() {
		m_container.SetActive(true);
		foreach (MediaAction act in m_actions) {
			act.Apply();
		}

	}

	public void Deactivate() {
		m_container.SetActive(false);
	}
}


[Serializable]
public class MediaAction {
	public string m_name;
	public n_mediaActionType m_type;
	// [Header("Reparenting")]

	[HideInInspector] public GameObject m_target;
	public GameObject m_parent;

	private GameObject GetTarget() {
		m_target = m_query.GetTargetByIndex(m_targetID);
		if (m_target != null) {
			m_group = m_target.GetComponent<LayoutElement>();
		}
		return m_target;
	}
	// [Header("Layout Element")]
	public LayoutElement m_group;
	public Vector2 m_minSize;
	public Vector2 m_prefSize;
	// [Header("Visibility")]
	// public GameObject m_visibilityTarget;
	public bool m_isShown;

	private UIMediaQuery m_query;
	public int m_targetID;
	public bool m_isModernVersion;


	public void SetTarget(UIMediaQuery media, int id) {
		m_query = media;
		m_targetID = id;
		GetTarget();
		m_isModernVersion = true;
	}

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
		if (m_parent == null) { return; }
		if (m_group == null) { GetTarget(); }
		if (m_target == null) { return; }
		m_target.transform.SetParent(m_parent.transform, false);
	}

	private void ApplyLayout() {
		if (m_group == null) { return; }
		if (m_group == null) { GetTarget(); }
		if (m_group == null) { return; }
		m_group.minHeight = m_minSize.y;
		m_group.minWidth = m_minSize.x;
		m_group.preferredHeight = m_prefSize.y;
		m_group.preferredWidth = m_prefSize.x;
	}

	private void ApplyVisibility() {
		if (m_group == null) { GetTarget(); }
		if (m_target == null) { return; }
		m_target.gameObject.SetActive(m_isShown);
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


