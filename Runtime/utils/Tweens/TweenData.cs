//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class TweenData {
	// Dependencies

	// Properties
	public AnimationCurve m_curve;
	public float m_timeSeconds;
	public float m_timeDelaySeconds;

	[Header("Settings")]
	public bool m_deactivatesOnFinish = false;
	public bool m_autoStart = false;
	public bool m_prefillValues = false;
	public bool m_postFillsValues = true;
	public bool m_bounces = false;
	public bool m_repeats = false;
	public int m_repeatCount = -1;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions

	// Private Functions

}
