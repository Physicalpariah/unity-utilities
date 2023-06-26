//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

#if UNITY_IOS
using UnityEngine.iOS;
#endif



public static class UIUtils {
	// Public Functions
	public static void SubscribeButton(Button butt, UnityEngine.Events.UnityAction function) {
		butt.onClick.RemoveAllListeners();
		butt.onClick.AddListener(function);
	}

	public static void SubscribeInput(InputField field, UnityEngine.Events.UnityAction<string> function) {
		field.onEndEdit.RemoveAllListeners();
		field.onEndEdit.AddListener(function);
	}

	public static void SubscribeInput(TMP_InputField field, UnityEngine.Events.UnityAction<string> function) {
		field.onEndEdit.RemoveAllListeners();
		field.onEndEdit.AddListener(function);
	}

	public static void SubscribeDropdown(Dropdown dropdown, List<string> names, UnityEngine.Events.UnityAction<int> action) {
		dropdown.ClearOptions();
		List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();

		for (int a = 0; a < names.Count; a++) {
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = names[a];
			dropdownOptions.Add(option);
		}

		dropdown.AddOptions(dropdownOptions);
		dropdown.onValueChanged.RemoveAllListeners();

		dropdown.RefreshShownValue();
		dropdown.onValueChanged.AddListener(action);
	}


	public static void SubscribeDropdown(TMP_Dropdown dropdown, List<string> names, UnityEngine.Events.UnityAction<int> action) {
		dropdown.ClearOptions();
		List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

		for (int a = 0; a < names.Count; a++) {
			TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
			option.text = names[a];
			dropdownOptions.Add(option);
		}

		dropdown.AddOptions(dropdownOptions);
		dropdown.RefreshShownValue();
		dropdown.onValueChanged.RemoveAllListeners();
		dropdown.onValueChanged.AddListener(action);
	}



	public static bool CheckForUI() {
		if (EventSystem.current.IsPointerOverGameObject(0)) {
			return true;
		}
		if (EventSystem.current.IsPointerOverGameObject(-1)) {
			return true;
		}

		if (IsPointerOverUIObject()) {
			return true;
		}
		// if (EventSystem.current.currentSelectedGameObject != null) {
		// 	return true;
		// }

		return false;
	}


	public static string ToRGBHex(Color c) {
		return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
	}

	private static byte ToByte(float f) {
		f = Mathf.Clamp01(f);
		return (byte)(f * 255);
	}

	private static bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}


	private static float GetKeyboardHeight() {
#if UNITY_IOS
		switch (Device.generation) {
			case DeviceGeneration.iPhone6:
			case DeviceGeneration.iPhone6S:
			case DeviceGeneration.iPhone7:
			case DeviceGeneration.iPhone8:
				return 243f;
			case DeviceGeneration.iPhone6Plus:
			case DeviceGeneration.iPhone6SPlus:
			case DeviceGeneration.iPhone7Plus:
			case DeviceGeneration.iPhone8Plus:
				return 230f;
			case DeviceGeneration.iPhone5S:
			case DeviceGeneration.iPhoneSE1Gen:
				return 285f;
			case DeviceGeneration.iPhoneX:
				return 282.5f;
			case DeviceGeneration.iPadAir1:
			case DeviceGeneration.iPadAir2:
			case DeviceGeneration.iPadPro10Inch1Gen:
				return 213f;
			case DeviceGeneration.iPadPro10Inch2Gen:
				return 196f;
			case DeviceGeneration.iPadPro1Gen:
			case DeviceGeneration.iPadPro2Gen:
				return 193f;
			default:
				return 243f;
		}
#endif
	}

}