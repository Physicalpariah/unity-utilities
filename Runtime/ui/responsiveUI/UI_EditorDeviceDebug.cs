//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class UI_EditorDeviceDebug : MonoBehaviour {
	// Properties 
	[SerializeField] private n_deviceType m_deviceType;
	[SerializeField] private bool m_usesDebugDevice = false;
	// Initalisation Functions

	// Unity Callbacks
	private void Update() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			return;
		}

		if (!m_usesDebugDevice) {
			return;
		}

		DeviceUtils.m_debugDevice = m_deviceType;
	}

	// Public Functions

	// Private Functions

}