//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class LocalisationStrings {
	// Properties
	public List<LocalisationString> m_strings;

	public bool Contains(string value) {
		if (string.IsNullOrWhiteSpace(value)) {
			return false;
		}

		foreach (LocalisationString str in m_strings) {
			if (str != null) {
				if (string.IsNullOrWhiteSpace(str.m_default) == false) {
					if (str.m_default.ToLower() == value.ToLower()) {
						return true;
					}
				}
			}
		}

		return false;
	}
}

[Serializable]
public class LocalisationString {
	public string m_default = "";
	public string m_current = "";
	public string m_comment = "";
	public LocalisationString() { }
	public LocalisationString(string line) {
		m_default = line.ToLower(); // THIS MUST BE LOWERCASE
		m_current = line;
	}
}