//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class OrientationDetection : MonoBehaviour {

	// Properties
	public EventHandler e_landscapeDetected;
	public EventHandler e_portraitDetected;
	public CoreEvent e_orientationChanged;
	public EventHandler e_destroying;


	[SerializeField] private int m_framesToCheck = 10;
	[SerializeField] private bool m_forcesLandscape = false;
	[SerializeField] private bool m_forcesPortrait = false;


	public bool m_isPortrait { get; private set; } = true;

	private Coroutine c_orientationCheck;

	private static OrientationDetection m_instance;
	public static OrientationDetection Instance {
		get {
			if (m_instance == null) {
				m_instance = FindObjectOfType<OrientationDetection>();
			}
			return m_instance;
		}
	}

	// Initalisation Functions
	private void OnEnable() {
		c_orientationCheck = StartCoroutine(DoOrientationCheck());
	}

	private void OnDestroy() {
		StopCoroutine(c_orientationCheck);
		m_instance = null;
		if (e_destroying != null) {
			e_destroying(this, null);
		}
	}

	// Public Functions


	// Private Functions
	private IEnumerator DoOrientationCheck() {
		WaitForEndOfFrame update = new WaitForEndOfFrame();
		while (true) {

			if (Screen.width > Screen.height) {
				SetLandscape();
			}
			else {
				SetPortrait();
			}

			for (int a = 0; a < m_framesToCheck; a++) {
				yield return update;
			}
		}
	}


	private void SetPortrait() {
		if (!m_isPortrait) {
			m_isPortrait = true;
			LogUtils.Log("orientation:  Setting Portrait");
			if (e_portraitDetected != null) {
				e_portraitDetected(this, null);
			}

			if (e_orientationChanged != null) {
				e_orientationChanged();
			}
		}

	}


	private void SetLandscape() {
		if (m_isPortrait) {
			m_isPortrait = false;
			LogUtils.Log("orientation: Setting Landscape");
			if (e_landscapeDetected != null) {
				e_landscapeDetected(this, null);
			}
			if (e_orientationChanged != null) {
				e_orientationChanged();
			}
		}
	}
}



