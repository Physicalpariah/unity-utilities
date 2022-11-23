//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// #if UNITY_EDITOR
// using UnityEditor;
// [ExecuteInEditMode]
// #endif
public class UI_ResponsiveElement : MonoBehaviour {

	// Properties
	[SerializeField] private Transform m_portratParent;
	[SerializeField] private Transform m_landscapeParent;

	[SerializeField] private List<ResponsiveBreakPointParents> m_landscapeBreakPointParents;
	[SerializeField] private GameObject m_layoutElement;
	[SerializeField] private LayoutElement m_element;
	[SerializeField] private RectTransform m_transform;

	[Header("Resolution")]
	[SerializeField] private Vector2 m_widths;
	[Header("Layout")]
	[SerializeField] private bool m_forcesLandscapeOniPad = true;
	[SerializeField] private bool m_forcesOrientationForPlatforms = false;
	public bool m_forcesLandscape = false;
	[SerializeField] private List<n_deviceType> m_forcePlatforms;
	[Header("Testing")]
	[SerializeField] private int m_index;

	private bool m_subscribed = false;

	// Initalisation Functions

	[ContextMenu("Get References")]
	public void GetReferences() {
		m_element = GetComponent<LayoutElement>();
		m_transform = GetComponent<RectTransform>();
		m_layoutElement = gameObject;
	}

	void OnEnable() {
		if (DeviceUtils.isScreenPortrait) {
			SetPortrait(null, null);
		}
		else {
			SetLandscape(null, null);
		}
		OrientationDetection.Instance.e_landscapeDetected += SetLandscape;
		OrientationDetection.Instance.e_portraitDetected += SetPortrait;
		OrientationDetection.Instance.e_destroying += Unsubscribe;

		m_subscribed = true;
	}

	private void OnDisable() {
		Unsubscribe(null, null);
	}

	private void Unsubscribe(object sender, EventArgs ea) {
		if (!m_subscribed) {
			return;
		}

		OrientationDetection.Instance.e_destroying -= Unsubscribe;
		OrientationDetection.Instance.e_landscapeDetected -= SetLandscape;
		OrientationDetection.Instance.e_portraitDetected -= SetPortrait;
		m_subscribed = false;
	}

	public void AutoGetReferences() {
		m_transform = GetComponent<RectTransform>();
		m_layoutElement = gameObject;
		m_element = ObjectUtils.InitComponent<LayoutElement>(gameObject) as LayoutElement;
	}


	public void SetForceSettingsToChildren() {
		UI_ResponsiveElement[] list = GetComponentsInChildren<UI_ResponsiveElement>();
		foreach (UI_ResponsiveElement element in list) {
			element.m_forcePlatforms = m_forcePlatforms;
			element.m_forcesLandscape = m_forcesLandscape;
			element.m_forcesOrientationForPlatforms = m_forcesOrientationForPlatforms;
		}
	}

	// Portrait

	public void SetPortraitTest() {
		UI_ResponsiveElement[] list = GetComponentsInChildren<UI_ResponsiveElement>();
		foreach (UI_ResponsiveElement element in list) {
			element.SetPortrait();
		}
	}

	public void SetPortrait() {
		SetPortrait(null, null);
	}

	public void SetLandscapeTest() {
		UI_ResponsiveElement[] list = GetComponentsInChildren<UI_ResponsiveElement>();
		foreach (UI_ResponsiveElement element in list) {
			element.SetLandscape();
		}
	}

	public void SetLandscape() {
		SetLandscape(null, null);
	}


	private void SetPortrait(object sender, EventArgs ea) {


		// TODO: Remove Legacy Code;
		bool isIpad = false;
		isIpad = DeviceUtils.isIPad;
		if (isIpad) {
			if (m_forcesLandscape || m_forcesLandscapeOniPad) {
				SetLandscape(null, null);
				return;
			}
		}
		// end

		if (ForcesOrientation()) {
			if (m_forcesLandscape) {
				SetLandscape(null, null);
				return;
			}
		}

		SetResolution(m_widths.y);
		SetWidth(m_widths.y);
		SetLayout(m_portratParent);
	}


	// Landscape
	private void SetLandscape(object sender, EventArgs ea) {

		if (ForcesOrientation()) {
			if (!m_forcesLandscape) {
				SetPortrait(null, null);
				return;
			}
		}

		SetResolution(m_widths.x);
		SetWidth(m_widths.x);

		if (m_landscapeBreakPointParents.Count == 0) {
			SetLayout(m_landscapeParent);
		}
		else {
			ResponsiveBreakPointParents chosen = null;
			foreach (ResponsiveBreakPointParents parent in m_landscapeBreakPointParents) {
				if (UI_ViewManager.Instance.m_mainCanvas.referenceResolution.y < parent.m_heightBreak) {
					if (chosen == null) {
						chosen = parent;
					}
					else {
						if (parent.m_heightBreak < chosen.m_heightBreak) {
							chosen = parent;
						}
					}
				}
			}
			if (chosen != null) {
				SetLayout(chosen.m_parent);
			}
			else {
				SetLayout(m_landscapeParent);
			}
		}
	}

	public bool ForcesOrientation() {
		bool forces = false;
		if (m_forcesOrientationForPlatforms) {
			foreach (n_deviceType type in m_forcePlatforms) {
				if (type == DeviceUtils.m_deviceType) {
					forces = true;
				}
			}
		}

		return forces;
	}


	// Functions

	private void SetResolution(float targetWidth) {

		if (targetWidth == 0) {
			return;
		}
		if (m_element == null) {
			return;
		}

		if (m_element.preferredWidth != targetWidth) {
			m_element.preferredWidth = targetWidth;
		}
	}


	private void SetWidth(float targetWidth) {
		if (m_transform == null) {
			return;
		}
		if (m_transform.sizeDelta.x != targetWidth) {
			Vector2 rect = m_transform.sizeDelta;
			rect.x = targetWidth;
			m_transform.sizeDelta = rect;
		}
	}


	private void SetLayout(Transform parent) {
		if (m_layoutElement == null) {
			return;
		}

		if (parent == null) {
			return;
		}

		if (m_layoutElement.transform.parent != parent) {
			m_layoutElement.transform.SetParent(parent, false);
			transform.SetSiblingIndex(m_index);
		}
	}
}

[Serializable]
public class ResponsiveBreakPointParents {
	public int m_heightBreak = -1;
	public int m_widthBreak = -1;
	public RectTransform m_parent;
}


