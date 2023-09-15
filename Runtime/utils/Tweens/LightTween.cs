//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightTween : BaseTween {
	// Dependencies

	// Properties
	[SerializeField] private float m_intentsityStart;
	[SerializeField] private float m_intentsityEnd;
	[SerializeField] private Light m_light;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions
	protected override void Apply(float lerp = 0) {
		base.Apply(lerp);
		m_light.intensity = LerpStuff(m_light.intensity, m_intentsityStart, m_intentsityEnd, lerp);
	}

	private float LerpStuff(float element, float start, float end, float lerp) {
		return Mathf.Lerp(start, end, lerp);
	}
	// Private Functions
	[ContextMenu("Tween/Copy/Both")]
	protected void CopyTransformToBothTween() {
		base.CopyTransformToEndTween();
		m_intentsityStart = m_light.intensity;
		m_intentsityEnd = m_light.intensity;
	}


	[ContextMenu("Tween/Copy/End")]
	protected override void CopyTransformToEndTween() {
		base.CopyTransformToEndTween();
		m_intentsityEnd = m_light.intensity;
	}

	[ContextMenu("Tween/Copy/Start")]
	protected override void CopyTransformToStartTween() {
		base.CopyTransformToStartTween();
		m_intentsityStart = m_light.intensity;
	}


	private float Copy(float toCopy) {
		return m_light.intensity;
	}
}