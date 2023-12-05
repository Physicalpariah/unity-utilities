//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMediaQueryHandler : MonoBehaviour {
	// Dependencies

	// Properties
	[SerializeField] private List<MediaQueryTrigger> m_queries;
	private n_mediaOrientation m_orientation;
	private Vector2 m_prevScreenSize = Vector2.zero;

	public static MediaQueryTrigger m_currentTrigger { get; private set; }

	public delegate void d_mediaChanged(MediaQueryTrigger trigger);
	public static event d_mediaChanged e_mediaChanged;

	// Initalisation Functions

	private void OnEnable() {
		StartCoroutine(WaitThenFire());
	}

	private IEnumerator WaitThenFire() {
		yield return new WaitForSeconds(1);
		Repaint();
	}

	private void OnDestroy() {
		m_currentTrigger = null;
	}

	private void OnApplicationQuit() {
		m_currentTrigger = null;
	}

	// Unity Callbacks
	private void Update() {
		RepaintOnScreenSizeChange();
	}

	private void RepaintOnScreenSizeChange() {
		bool repaints = false;
		Vector2 res = GetResolution();
		if (m_prevScreenSize != res) { repaints = true; }
		if (repaints == false) { return; }
		m_prevScreenSize = res;
		Repaint();
	}

	// Public Functions

	// Private Functions
	private Vector2 GetResolution() {
		return new Vector2(Screen.width, Screen.height);
	}

	private void Repaint() {
		SetOrientation();
		MediaQueryTrigger trigger = null;
		foreach (MediaQueryTrigger query in m_queries) {
			if (m_orientation == query.m_orientation || query.m_orientation == n_mediaOrientation.none) {
				foreach (n_deviceType typ in query.m_devices) {
					if (DeviceUtils.m_deviceType == typ) {
						trigger = query;
					}
				}
			}
		}


		m_currentTrigger = trigger;
		if (e_mediaChanged != null) {
			e_mediaChanged(trigger);
		}
	}

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