//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;

[Serializable]
public class LocalisationStrings {
	// Properties
	public List<LocalisationString> m_strings;

	public bool Contains(string value) {
		foreach (LocalisationString str in m_strings) {
			if (str.m_default.ToLower() == value.ToLower()) {
				return true;
			}
		}

		return false;
	}
}

[Serializable]
public class LocalisationString {
	public string m_default;
	public string m_current;


	public LocalisationString() { }
	public LocalisationString(string line) {
		m_default = line;
		m_current = line;
	}
}