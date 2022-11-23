//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_FillBar : UI_View {
	// Properties
	public Image m_fill;
	private Coroutine c_fillRoutine;

	// Initalisation Functions
	public override void Initialise(UI_ViewController controller, object data) {
		base.Initialise(controller, data);

		float fill = (float)data;

		if (c_fillRoutine != null) {
			m_controller.StopCoroutine(c_fillRoutine);
		}
		c_fillRoutine = m_controller.StartCoroutine(DoFillChange(fill));
	}
	// Unity Callbacks

	// Public Functions

	// Private Functions

	private IEnumerator DoFillChange(float fill) {

		float current = m_fill.fillAmount;
		float target = fill;
		float time = 10.0f;
		for (float a = 0; a < time; a++) {
			m_fill.fillAmount = Mathf.Lerp(current, target, a / time);
			yield return new WaitForFixedUpdate();
		}

		m_fill.fillAmount = target;
	}


}