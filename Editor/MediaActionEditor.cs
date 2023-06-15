// //  Created by Matt Purchase.
// //  Copyright (c) 2023 Matt Purchase. All rights reserved.
// using System.Collections.Generic;
// using System;
// using UnityEditor;
// using UnityEngine;


// [CustomPropertyDrawer(typeof(MediaAction))]
// public class MediaActionEditor : PropertyDrawer {

// 	// Properties
// 	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
// 		return EditorGUIUtility.singleLineHeight * m_propertyCount;
// 	}

// 	public int m_propertyCount = 3;
// 	// Public Functions
// 	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {


// 		// Find the SerializedProperties by nameF
// 		var name = property.FindPropertyRelative("m_name");
// 		var action = property.FindPropertyRelative("m_type");
// 		// reparent
// 		var target = property.FindPropertyRelative("m_target");
// 		var parent = property.FindPropertyRelative("m_parent");
// 		// layout
// 		var layout = property.FindPropertyRelative("m_group");
// 		var min = property.FindPropertyRelative("m_minSize");
// 		var pref = property.FindPropertyRelative("m_prefSize");




// 		switch (action.enumValueIndex) {
// 			case ((int)n_mediaActionType.reparent): {

// 					break;
// 				}
// 			case ((int)n_mediaActionType.layoutElement): {

// 					break;
// 				}
// 		}


// 		// EditorGUI.BeginProperty(position, label, property);
// 		// 

// 		EditorGUI.EnumPopup(position, (n_mediaActionType)action.enumValueFlag);

// 		float height = EditorGUIUtility.singleLineHeight;
// 		float y = position.y + height;

// 		var targetRect = new Rect(position.x, y, position.width, height);

// 		EditorGUI.ObjectField(targetRect, target);

// 		y += height;
// 		var minRect = new Rect(position.x, y, position.width, height);
// 		EditorGUI.Vector2Field(minRect, "min", min.vector2Value);

// 		// EditorGUI.EndProperty();

// 	}


// }
