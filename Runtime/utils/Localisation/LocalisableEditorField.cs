//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class LocalisableField {
	public string m_value;
}


[CustomPropertyDrawer(typeof(LocalisableField))
]
public class LocalisableEditorField : PropertyDrawer {

	private string m_lastValue;
	private bool m_isSet = false;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Calculate rects
		var baseValueRect = new Rect(position.x, position.y, position.width - 120, position.height);
		var labelRect = new Rect(position.x + 2 + baseValueRect.width, position.y, 90, position.height);
		var buttonRect = new Rect(position.x + 2 + baseValueRect.width + 90, position.y, 30, position.height);


		string value = property.FindPropertyRelative("m_value").stringValue;
		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		// m_isSet = LocalisationStringsObject.Instance.m_data.Contains(value);

		if (!m_isSet) {
			EditorGUI.PropertyField(baseValueRect, property.FindPropertyRelative("m_value"), GUIContent.none);
			if (value != m_lastValue) {
				m_lastValue = value;
			}
		}
		else {
			EditorGUI.LabelField(baseValueRect, property.FindPropertyRelative("m_value").stringValue);
		}


		if (string.IsNullOrWhiteSpace(value) == false) {
			string suggestion = LocalisationStringsObject.Instance.Search(value);
			if (GUI.Button(labelRect, suggestion)) {
				property.FindPropertyRelative("m_value").stringValue = suggestion;
			}
		}


		if (GUI.Button(buttonRect, "D")) {
			m_isSet = !m_isSet;

			if (!m_isSet) {
				property.FindPropertyRelative("m_value").stringValue = "";
			}
			LocalisationStringsObject.Instance.Add(value);
		}



		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}