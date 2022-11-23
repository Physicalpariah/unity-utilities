//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


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
		// if (EventSystem.current.currentSelectedGameObject != null) {
		// 	return true;
		// }

		return false;
	}

}