//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupTween : BaseTween {
	// Dependencies

	// Properties
	[SerializeField] private float m_dataStart;
	[SerializeField] private float m_dataEnd;
	[SerializeField] private CanvasGroup m_canvasGroup;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions
	protected override void Apply(float lerp = 0) {
		base.Apply(lerp);
		m_canvasGroup.alpha = LerpStuff(m_canvasGroup.alpha, m_dataStart, m_dataEnd, lerp);
	}

	private float LerpStuff(float element, float start, float end, float lerp) {
		return Mathf.Lerp(start, end, lerp);
	}

	[ContextMenu("Tween/Copy/Both")]
	protected void CopyTransformToBothTween() {
		base.CopyTransformToEndTween();
		m_dataEnd = m_canvasGroup.alpha;
		m_dataStart = m_canvasGroup.alpha;
	}


	[ContextMenu("Tween/Copy/End")]
	protected override void CopyTransformToEndTween() {
		base.CopyTransformToEndTween();
		m_dataEnd = m_canvasGroup.alpha;
	}

	[ContextMenu("Tween/Copy/Start")]
	protected override void CopyTransformToStartTween() {
		base.CopyTransformToStartTween();
		m_dataStart = m_canvasGroup.alpha;
	}


	private float Copy(float toCopy) {
		return m_canvasGroup.alpha;
	}
	// Private Functions

}
