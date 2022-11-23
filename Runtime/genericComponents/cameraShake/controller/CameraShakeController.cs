//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraShakeController {
	// Properties
	public delegate void d_shakeFired(ref CameraShakeData data);
	public event d_shakeFired e_shakeFired;

	private CameraShakeView m_view;


	// Initalisation Functions

	public void Initialise() {
		CollectView();
	}

	private void CollectView() {
		m_view = GameObject.FindObjectOfType<CameraShakeView>();
		if (m_view == null) {
			LogUtils.LogError("No Camerashake view found, please add one to the main camera");
		}
		m_view.Initialise(this);
	}

	public void ShutDown() {

	}
	// Unity Callbacks

	// Public Functions

	public void ShakeWithData(CameraShakeData data) {
		if (e_shakeFired != null) {
			e_shakeFired(ref data);
		}
	}

	public void Shake() {
		CameraShakeData data = null;
		if (UtilsManager.Instance != null) {
			data = UtilsManager.Instance.m_references.m_cameraShake.m_defaultShakeData;
		}
		else {
			data = new CameraShakeData();
			data.m_frames = 10;
			data.m_strength = 10;
			
			data.m_xAxisCurve = new AnimationCurve();
			data.m_xAxisCurve.AddKey(0,1);
			data.m_xAxisCurve.AddKey(1,1);

			data.m_yAxisCurve = new AnimationCurve();
			data.m_yAxisCurve.AddKey(0,1);
			data.m_yAxisCurve.AddKey(1,1);
		}

		if (e_shakeFired != null) {
			e_shakeFired(ref data);
		}
	}

	// Private Functions

}