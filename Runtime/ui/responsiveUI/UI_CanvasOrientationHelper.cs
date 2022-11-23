//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
using UnityEngine.UI;

#if UNTY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

public class UI_CanvasOrientationHelper : MonoBehaviour {

	// Properties
	[SerializeField] private CanvasScaler m_scaler;

	[SerializeField] private CanvasOrientationData m_mobileData;
	[SerializeField] private CanvasOrientationData m_iPadData;
	[SerializeField] private CanvasOrientationData m_desktopData;


	// Initalisation Functions
	void OnEnable() {
		UpdateScale();
		GetScaler();
		UI_Config.e_uiScaleChanged += UpdateScaleEvent;
	}

	void OnDestroy() {
		UI_Config.e_uiScaleChanged -= UpdateScaleEvent;
	}

	private void UpdateScaleEvent() {
		UpdateScale();
	}

	private void UpdateScale() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			if (DeviceUtils.isIPad) {
				SetResolution(m_iPadData);
				return;
			}
			SetResolution(m_mobileData);
		}
		else {
			SetResolution(m_desktopData);
		}

		ForceEditorRes();
	}

	private void GetScaler() {
		if (m_scaler == null) {
			m_scaler = GetComponent<CanvasScaler>();
		}
	}

	private void ForceEditorRes() {
		if (Application.platform == RuntimePlatform.OSXEditor) {
			switch (DeviceUtils.m_debugDevice) {
				case (n_deviceType.iPhone): {
						SetResolution(m_mobileData);
						break;
					}
				case (n_deviceType.iPad): {
						SetResolution(m_iPadData);
						break;
					}
				case (n_deviceType.mac): {
						SetResolution(m_desktopData);
						break;
					}
			}
		}
	}


	private void SetResolution(CanvasOrientationData data) {
		m_scaler.referenceResolution = data.m_resolution * UI_Config.m_uiScale;
		m_scaler.uiScaleMode = data.m_mode;
		m_scaler.scaleFactor = data.m_pixelScale * UI_Config.m_uiScale;
		m_scaler.defaultSpriteDPI = data.m_dpi;
		m_scaler.fallbackScreenDPI = data.m_dpi;
	}


	// Public Functions


	// Private Functions
	private void LateUpdate() {
		if (m_scaler == null) {
			return;
		}

		if (Screen.height > Screen.width) {
			if (m_scaler.matchWidthOrHeight != 0) {
				m_scaler.matchWidthOrHeight = 0;
			}
		}
		else {
			if (m_scaler.matchWidthOrHeight != 1) {
				m_scaler.matchWidthOrHeight = 1;
			}
		}


		ForceEditorRes();
	}
}

[Serializable]
public struct CanvasOrientationData {
	public CanvasScaler.ScaleMode m_mode;
	public Vector2 m_resolution;
	public float m_pixelScale;
	public float m_dpi;
}

