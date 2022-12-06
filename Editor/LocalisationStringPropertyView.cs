//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalisationString))
]
public class LocalisationStringPropertyView : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		// Draw label

		// var prop = property.FindPropertyRelative("m_current");
		// label.text = prop.stringValue;

		// position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Calculate rects
		var baseValueRect = new Rect(position.x, position.y, position.width, position.height);

		var defaultValueRect = new Rect(position.x, position.y, position.width / 2, position.height);
		var currentValueRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);
		// var toleranceValueRect = new Rect(position.x + 2 + baseValueRect.width, position.y,
		// 30, position.height);

		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField(defaultValueRect, property.FindPropertyRelative("m_default"), GUIContent.none);
		EditorGUI.PropertyField(currentValueRect, property.FindPropertyRelative("m_current"), GUIContent.none);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}