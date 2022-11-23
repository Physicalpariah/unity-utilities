//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;

//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "anchorite/camera shake data")]
public class CameraShakeData : ScriptableObject {
	// Properties
	public int m_frames;
	public float m_strength;
	public UnityEngine.AnimationCurve m_xAxisCurve;
	public UnityEngine.AnimationCurve m_yAxisCurve;
	// Initalisation Functions

	// Public Functions

	// Private Functions
}