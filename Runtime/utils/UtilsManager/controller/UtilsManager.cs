//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UtilsManager {
	// Properties
	public UtilsReferences m_references;
	public CameraShakeController m_cameraShake;


	private static UtilsManager m_instance;
	public static UtilsManager Instance {
		get {
			return m_instance;
		}
	}

	// Initalisation Functions

	private void SetInstance() {
		if (m_instance != null) {
			LogUtils.LogError("Nope, instance already set, this should'nt have fired.");
		}
		m_instance = this;
	}

	public void Initialise() {
		SetInstance();
		m_cameraShake = new CameraShakeController();


		m_cameraShake.Initialise();
	}

	private void ShutDown() {
		m_cameraShake.ShutDown();
		m_cameraShake = null;
	}
	// Unity Callbacks

	// Public Functions

	// Private Functions

}