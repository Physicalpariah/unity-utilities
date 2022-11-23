//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UtilsReferences : MonoBehaviour {
	// Properties
	public CameraShakeReferences m_cameraShake;
	public UtilsConfig m_config;

	// Initalisation Functions

	// Public Functions

	// Private Functions

}

[Serializable]
public class UtilsConfig {
	public float m_letGoPropulsionAmount = 10.0f;
}