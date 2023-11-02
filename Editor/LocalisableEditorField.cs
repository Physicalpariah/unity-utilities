//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;



[CustomPropertyDrawer(typeof(LocalisableField))
]
public class LocalisableEditorField : PropertyDrawer {

	public string m_lastValue;
	public bool m_isSet = false;
	private bool m_initialCheck = false;
	private bool m_bigMode = false;

	private string m_textAreaValue;

	const float baseValueIndent = 150;
	const float minHeight = 18;

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		// return base.GetPropertyHeight(property, label);
		if (m_bigMode) {
			return 120;
		}
		else {
			return minHeight;
		}
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		float baseValueIndent = 150;
		if (m_isSet == true) {
			baseValueIndent = 60;
		}

		// Calculate rects
		var baseValueRect = new Rect(position.x, position.y, position.width - baseValueIndent, position.height);
		var labelRect = new Rect(position.x + 2 + baseValueRect.width, position.y, 90, position.height);
		var buttonRect = new Rect(position.x + 2 + baseValueRect.width + labelRect.width, position.y, 30, position.height);
		var fieldButtonRect = new Rect(position.x + 2 + baseValueRect.width + labelRect.width + buttonRect.width, position.y, 30, position.height);
		var charCountRect = new Rect(position.x, position.height + 5, position.width, minHeight);

		if (m_isSet) {
			buttonRect = new Rect(position.x + 2 + baseValueRect.width, position.y, 30, position.height);
			fieldButtonRect = new Rect(position.x + 2 + baseValueRect.width + buttonRect.width, position.y, 30, position.height);
		}

		if (m_bigMode) {
			baseValueRect = new Rect(position.x, position.y + minHeight, position.width, position.height - minHeight - minHeight);
			labelRect = new Rect(position.x, position.y, position.width - 60, minHeight);
			buttonRect = new Rect(position.x + labelRect.width, position.y, 30, minHeight);
			fieldButtonRect = new Rect(position.x + labelRect.width + buttonRect.width, position.y, 30, minHeight);
		}


		string value = property.FindPropertyRelative("m_value").stringValue;
		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		if (!m_initialCheck) {
			m_isSet = Localisation.Instance.m_data.Contains(value);
			m_initialCheck = true;
		}



		EditorStyles.textArea.wordWrap = true;
		EditorStyles.textField.wordWrap = true;
		if (!m_isSet) {
			if (m_bigMode) {
				property.FindPropertyRelative("m_value").stringValue = EditorGUI.TextArea(baseValueRect, property.FindPropertyRelative("m_value").stringValue);
				EditorGUI.LabelField(charCountRect, value.Length.ToString());
			}
			else {
				EditorGUI.PropertyField(baseValueRect, property.FindPropertyRelative("m_value"), GUIContent.none);
			}

			if (value != m_lastValue) {
				m_lastValue = value;
			}
		}
		else {
			EditorGUI.LabelField(baseValueRect, property.FindPropertyRelative("m_value").stringValue);
		}





		if (!m_isSet) {

			if (string.IsNullOrWhiteSpace(value) == false) {
				List<string> suggestions = Localisation.SearchList(value);


				int selected = 0;

				EditorGUI.BeginChangeCheck();
				selected = EditorGUI.Popup(labelRect, selected, suggestions.ToArray());
				if (EditorGUI.EndChangeCheck()) {
					property.FindPropertyRelative("m_value").stringValue = suggestions[selected];
					m_isSet = true;
				}
				// if (GUI.Button(labelRect, suggestion)) {
				// 	property.FindPropertyRelative("m_value").stringValue = suggestion;
				// }
			}
		}

		string buttonTitle = "+";
		if (m_isSet) {
			buttonTitle = "-";
		}


		if (GUI.Button(buttonRect, buttonTitle)) {
			ConfirmValue(property, value);
		}

		string fieldTitle = "T";
		if (m_bigMode) {
			fieldTitle = "t";
		}

		if (GUI.Button(fieldButtonRect, fieldTitle)) {
			m_bigMode = !m_bigMode;
		}



		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}

	private void ConfirmValue(SerializedProperty property, string value) {
		m_isSet = !m_isSet;

		if (!m_isSet) {
			property.FindPropertyRelative("m_value").stringValue = "";
		}
		else {
			Localisation.Add(value);
		}
	}
}