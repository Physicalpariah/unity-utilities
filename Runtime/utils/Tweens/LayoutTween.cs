//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(LayoutElement))]
public class LayoutTween : BaseTween {
	// Dependencies

	// Properties
	[SerializeField] private LayoutTweenData m_dataStart;
	[SerializeField] private LayoutTweenData m_dataEnd;
	[SerializeField] private LayoutElement m_layoutElement;

	protected override void Apply(float lerp = 0) {
		base.Apply(lerp);

		m_layoutElement.minHeight = LerpStuff(m_layoutElement.minHeight, m_dataStart.height_min, m_dataEnd.height_min, lerp);
		m_layoutElement.minWidth = LerpStuff(m_layoutElement.minWidth, m_dataStart.width_min, m_dataEnd.width_min, lerp);
		m_layoutElement.preferredHeight = LerpStuff(m_layoutElement.preferredHeight, m_dataStart.height_pref, m_dataEnd.height_pref, lerp);
		m_layoutElement.preferredWidth = LerpStuff(m_layoutElement.preferredWidth, m_dataStart.width_pref, m_dataEnd.width_pref, lerp);
		m_layoutElement.flexibleHeight = LerpStuff(m_layoutElement.flexibleHeight, m_dataStart.height_flex, m_dataEnd.height_flex, lerp);
		m_layoutElement.flexibleWidth = LerpStuff(m_layoutElement.flexibleWidth, m_dataStart.width_flex, m_dataEnd.width_flex, lerp);
		m_layoutElement.layoutPriority = (int)LerpStuff(m_layoutElement.layoutPriority, m_dataStart.layout_priority, m_dataEnd.layout_priority, lerp);
	}

	private float LerpStuff(float element, float start, float end, float lerp) {
		return Mathf.Lerp(start, end, lerp);
	}

	[ContextMenu("Tween/Copy/Both")]
	protected void CopyTransformToBothTween() {
		base.CopyTransformToEndTween();
		Copy(m_dataEnd);
		Copy(m_dataStart);
	}


	[ContextMenu("Tween/Copy/End")]
	protected override void CopyTransformToEndTween() {
		base.CopyTransformToEndTween();
		Copy(m_dataEnd);
	}

	[ContextMenu("Tween/Copy/Start")]
	protected override void CopyTransformToStartTween() {
		base.CopyTransformToStartTween();
		LogUtils.LogPriority("HMMM1");
		Copy(m_dataStart);
	}


	private void Copy(LayoutTweenData toCopy) {
		toCopy.width_min = m_layoutElement.minWidth;
		toCopy.height_min = m_layoutElement.minHeight;
		toCopy.width_pref = m_layoutElement.preferredWidth;
		toCopy.height_pref = m_layoutElement.preferredHeight;
		toCopy.height_flex = m_layoutElement.flexibleHeight;
		toCopy.width_flex = m_layoutElement.flexibleWidth;
		toCopy.layout_priority = m_layoutElement.layoutPriority;
	}
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions

	// Private Functions

}

[Serializable]
public class LayoutTweenData {
	public float height_min;
	public float width_min;
	public float height_pref;
	public float width_pref;
	public float height_flex;
	public float width_flex;
	public float layout_priority;
}