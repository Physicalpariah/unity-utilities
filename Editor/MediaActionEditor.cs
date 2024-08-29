//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(MediaAction))]
public class MediaActionEditor : PropertyDrawer {

	// Properties
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return m_lineHeight * m_propertyCount + (m_spacing * m_propertyCount);
	}

	public int m_lineHeight = 18;
	public int m_propertyCount = 3;
	public int m_spacing = 3;
	// Public Functions
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		// Find the SerializedProperties by nameF
		var name = property.FindPropertyRelative("m_name");
		var action = property.FindPropertyRelative("m_type");
		var modern = property.FindPropertyRelative("m_isModernVersion");
		// reparent
		var target = property.FindPropertyRelative("m_target");
		var parent = property.FindPropertyRelative("m_parent");
		// layout
		var layout = property.FindPropertyRelative("m_group");
		var min = property.FindPropertyRelative("m_minSize");
		var pref = property.FindPropertyRelative("m_prefSize");
		// visibility
		var visTarget = property.FindPropertyRelative("m_visibilityTarget");
		var visShown = property.FindPropertyRelative("m_isShown");

		var enumRect = new Rect(position.x, position.y, position.width, m_lineHeight);

		EditorGUI.PropertyField(enumRect, action);
		n_mediaActionType actionType = (n_mediaActionType)action.enumValueIndex;

		float y = position.y + m_lineHeight + m_spacing;
		var firstRect = new Rect(position.x, y, position.width, m_lineHeight);
		y += m_lineHeight + m_spacing;
		var secondRect = new Rect(position.x, y, position.width, m_lineHeight);
		y += m_lineHeight + m_spacing;
		var thirdRect = new Rect(position.x, y, position.width, m_lineHeight);

		switch (actionType) {
			case (n_mediaActionType.layoutElement): {
					if (modern.boolValue) {
						ShowTarget(target, firstRect);
						if (layout.objectReferenceValue == null) {
							EditorGUI.LabelField(secondRect, "no group");
							m_propertyCount = 3;
							break;
						}
						else {
							EditorGUI.PropertyField(secondRect, min);
							EditorGUI.PropertyField(thirdRect, pref);
							m_propertyCount = 4;
						}
					}
					else {
						EditorGUI.PropertyField(firstRect, layout);
						EditorGUI.PropertyField(secondRect, min);
						EditorGUI.PropertyField(thirdRect, pref);
						m_propertyCount = 4;
					}
					break;
				}
			case (n_mediaActionType.reparent): {
					// SerializedProperty commandsProperty = serializedObject.FindProperty("commands");
					// property.
					if (modern.boolValue) {
						ShowTarget(target, firstRect);
					}
					else {
						EditorGUI.PropertyField(firstRect, target);
					}
					EditorGUI.PropertyField(secondRect, parent);
					m_propertyCount = 3;
					break;
				}
			case (n_mediaActionType.toggleVisibility): {
					ShowTarget(target, firstRect);
					EditorGUI.PropertyField(secondRect, visShown);
					m_propertyCount = 3;
					break;
				}
		}
	}

	private static void ShowTarget(SerializedProperty target, Rect firstRect) {
		if (target.objectReferenceValue == null) {
			EditorGUI.LabelField(firstRect, "no target");
		}
		else {
			EditorGUI.LabelField(firstRect, target.objectReferenceValue.name.ToString(), AnchoriteEditorUtils.m_boldStyle);
		}
	}
}
