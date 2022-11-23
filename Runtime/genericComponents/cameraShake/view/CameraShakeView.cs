//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShakeView : MonoBehaviour {
	// Properties
	[SerializeField] private CameraShakeController m_controller;
	private Coroutine c_shakeRoutine;

	private WaitForEndOfFrame m_wait;

	// Initalisation Functions

	// Unity Callbacks

	public void Initialise(CameraShakeController controller) {
		m_controller = controller;
		m_controller.e_shakeFired += ShakeCamera;
		m_wait = new WaitForEndOfFrame();
	}

	private void ShutDown() {
		Unsubscribe();
	}

	private void Unsubscribe() {
		m_controller.e_shakeFired -= ShakeCamera;
	}

	// Public Functions

	// Private Functions

	private void ShakeCamera(ref CameraShakeData data) {
		if (c_shakeRoutine != null) {
			StopCoroutine(c_shakeRoutine);
		}

		c_shakeRoutine = StartCoroutine(DoShake(data));
	}

	private IEnumerator DoShake(CameraShakeData data) {
		for (float a = 0; a < data.m_frames; a++) {
			float x = data.m_xAxisCurve.Evaluate(a / (float)data.m_frames) * data.m_strength;
			float y = data.m_yAxisCurve.Evaluate(a / (float)data.m_frames) * data.m_strength;

			Vector3 rot = new Vector3(x, y, 0);

			transform.localRotation = Quaternion.Euler(rot);

			yield return m_wait;
		}

		transform.localRotation = Quaternion.identity;
	}
}