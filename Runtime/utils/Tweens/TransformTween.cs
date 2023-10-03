//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using System;


public class TransformTween : BaseTween {
	// Dependencies

	// Properties

	// Initalisation Functions
	[SerializeField] private TransformTweenData m_dataStart;
	[SerializeField] private TransformTweenData m_dataEnd;
	[SerializeField] private Transform m_transform;

	[SerializeField] private bool m_position = true;
	[SerializeField] private bool m_rotation = true;
	[SerializeField] private bool m_scale = true;
	// Unity Callbacks

	// Public Functions
	protected override void Apply(float lerp = 0) {
		base.Apply(lerp);

		if (m_position) {
			m_transform.localPosition = LerpStuff(m_transform.position, m_dataStart.pos, m_dataEnd.pos, lerp);
		}
		if (m_rotation) {
			m_transform.localRotation = Quaternion.Euler(LerpStuff(m_transform.rotation.eulerAngles, m_dataStart.rot, m_dataEnd.rot, lerp));
		}
		if (m_scale) {
			m_transform.localScale = LerpStuff(m_transform.localScale, m_dataStart.scale, m_dataEnd.scale, lerp);
		}
	}

	private Vector3 LerpStuff(Vector3 element, Vector3 start, Vector3 end, float lerp) {
		return Vector3.Lerp(start, end, lerp);
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

		Copy(m_dataStart);
	}


	private void Copy(TransformTweenData toCopy) {
		toCopy.pos = m_transform.position;
		toCopy.rot = m_transform.rotation.eulerAngles;
		toCopy.scale = m_transform.localScale;
	}
	// Private Functions

}


[Serializable]
public class TransformTweenData {
	public Vector3 pos;
	public Vector3 rot;
	public Vector3 scale;
}