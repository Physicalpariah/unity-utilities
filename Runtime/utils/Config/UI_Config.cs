//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class UI_Config {


	public const float m_iphoneMinLandscapeUIScale = 1.0f;
	public const float m_minUIScale = 0.8f;
	public const float m_maxUIScale = 1.5f;
	public const float m_defaultUIScale = 1.0f;
	private const string m_scalePrefsName = "ui_scale";

	public static CoreEvent e_uiScaleChanged;

	public static float m_uiScale {
		get {
			return GetLastUIScale();
		}
		set {
			SetFloat(value, m_scalePrefsName);

			if (UI_Config.e_uiScaleChanged != null) {
				UI_Config.e_uiScaleChanged();
			}
		}
	}

	public static float GetLastUIScale() {
		if (DeviceUtils.isiPhone) {
			if (!DeviceUtils.isScreenPortrait) {
				return GetFloat(m_scalePrefsName, m_defaultUIScale, m_iphoneMinLandscapeUIScale, m_maxUIScale);
			}
		}

		return GetFloat(m_scalePrefsName, m_defaultUIScale, m_minUIScale, m_maxUIScale);
	}

	public static bool GetBool(string name, bool defaultValue) {
		if (!UnityEngine.PlayerPrefs.HasKey(name)) {
			return defaultValue;
		}

		if (UnityEngine.PlayerPrefs.GetInt(name) == 1) {
			return true;
		}
		else {
			return false;
		}
	}


	public static void SetBool(bool value, string name) {
		if (value) {
			UnityEngine.PlayerPrefs.SetInt(name, 1);
		}
		else {
			UnityEngine.PlayerPrefs.SetInt(name, 0);
		}
	}



	public static void SetInt(int value, string name) {
		UnityEngine.PlayerPrefs.SetInt(name, value);
	}


	public static int GetInt(string name, int defaultValue) {
		if (!UnityEngine.PlayerPrefs.HasKey(name)) {
			return defaultValue;
		}

		return UnityEngine.PlayerPrefs.GetInt(name);
	}



	public static void SetFloat(float value, string name) {
		UnityEngine.PlayerPrefs.SetFloat(name, value);
	}


	public static float GetFloat(string name, float defaultValue, float minValue = 0, float maxValue = 1) {
		if (!UnityEngine.PlayerPrefs.HasKey(name)) {
			return defaultValue;
		}

		float value = UnityEngine.PlayerPrefs.GetFloat(name, defaultValue);

		if (value < minValue) {
			value = minValue;
		}

		if (value > maxValue) {
			value = maxValue;
		}

		return value;
	}



	public static void SetVector2(Vector2 value, string name) {
		UnityEngine.PlayerPrefs.SetFloat(name + "_x", value.x);
		UnityEngine.PlayerPrefs.SetFloat(name + "_y", value.y);
	}


	public static Vector2 GetVector2(string name) {

		bool hasData = true;

		if (!UnityEngine.PlayerPrefs.HasKey(name + "_x")) {
			hasData = false;
		}
		if (!UnityEngine.PlayerPrefs.HasKey(name + "_y")) {
			hasData = false;
		}

		Vector2 vector = new Vector2();
		if (hasData) {
			float x = UnityEngine.PlayerPrefs.GetFloat(name + "_x");
			float y = UnityEngine.PlayerPrefs.GetFloat(name + "_y");
			vector.x = x;
			vector.y = y;
		}

		return vector;
	}



	public static void SetVector3(Vector3 value, string name) {
		UnityEngine.PlayerPrefs.SetFloat(name + "_x", value.x);
		UnityEngine.PlayerPrefs.SetFloat(name + "_y", value.y);
		UnityEngine.PlayerPrefs.SetFloat(name + "_z", value.z);
	}


	public static Vector3 GetVector3(string name) {

		bool hasData = true;

		if (!UnityEngine.PlayerPrefs.HasKey(name + "_x")) {
			hasData = false;
		}
		if (!UnityEngine.PlayerPrefs.HasKey(name + "_y")) {
			hasData = false;
		}
		if (!UnityEngine.PlayerPrefs.HasKey(name + "_z")) {
			hasData = false;
		}


		Vector3 vector = new Vector3();
		if (hasData) {
			float x = UnityEngine.PlayerPrefs.GetFloat(name + "_x");
			float y = UnityEngine.PlayerPrefs.GetFloat(name + "_y");
			float z = UnityEngine.PlayerPrefs.GetFloat(name + "_z");
			vector.x = x;
			vector.y = y;
			vector.z = z;
		}

		return vector;
	}

}