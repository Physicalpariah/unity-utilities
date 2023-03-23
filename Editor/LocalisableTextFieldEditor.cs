//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using TMPro;


[CustomEditor(typeof(LocalisableTextField))]
public class LocalisableTextFieldEditor : Editor {

	// Properties
	private string m_lastUsed;
	// Public Functions
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		LocalisableTextField handler = (LocalisableTextField)target;
		if (handler.m_label == null) { return; }
		if (string.IsNullOrWhiteSpace(handler.m_text.m_value)) { return; }
		if (m_lastUsed == handler.m_text.m_value) { return; }


		m_lastUsed = handler.m_text.m_value;
		handler.m_label.text = handler.m_text.m_value;
		EditorUtility.SetDirty(handler);
		EditorUtility.SetDirty(handler.m_label);
	}
}