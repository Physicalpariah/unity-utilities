//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using UnityEditor;
using UnityEditor.IMGUI;
using UnityEngine;
using System.Collections.Generic;


public static class AnchoriteEditorUtils {
	// Properties

	// Initalisation Functions

	// Public Functions
	public static GUIStyle GetHeaderStyle() {

		GUIStyle headerStyle = new GUIStyle();
		headerStyle.normal.textColor = Color.white;
		headerStyle.alignment = TextAnchor.MiddleCenter;
		headerStyle.fontStyle = FontStyle.Bold;
		headerStyle.fontSize = 14;
		return headerStyle;
	}

	public static GUIStyle m_subHeaderStyle {
		get {
			GUIStyle headerStyle = new GUIStyle();
			headerStyle.normal.textColor = Color.white;
			headerStyle.alignment = TextAnchor.MiddleCenter;
			headerStyle.fontSize = 12;
			return headerStyle;
		}

	}

	public static GUIStyle m_boldStyle {
		get {
			GUIStyle headerStyle = new GUIStyle();
			headerStyle.normal.textColor = Color.white;
			headerStyle.fontStyle = FontStyle.Bold;
			return headerStyle;
		}

	}

	public static GUIStyle m_boldErrorStyle {
		get {
			GUIStyle headerStyle = new GUIStyle();
			headerStyle.normal.textColor = Color.red;
			headerStyle.fontStyle = FontStyle.Bold;
			return headerStyle;
		}

	}

	public static GUILayoutOption m_currentWidth {
		get {
			return GUILayout.Width(EditorGUIUtility.currentViewWidth);
		}
	}

	public static GUILayoutOption m_tabButtonHeight {
		get {
			return GUILayout.Height(50);
		}
	}

	public static GUILayoutOption m_rolloutButtonHeight {
		get {
			return GUILayout.Height(30);
		}
	}

	public static GUIStyle m_baseStyle {
		get {
			GUIStyle style = new GUIStyle();
			style.padding = new RectOffset(5, 5, 0, 0);
			return style;
		}
	}

	public static void DrawSpace() {
		GUILayout.Space(12);
	}

	public static void DrawUILine(Color color, int thickness = 2, int padding = 10) {
		Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
		r.height = thickness;
		r.y += padding / 2;
		r.x -= 2;
		r.width += 6;
		EditorGUI.DrawRect(r, color);
	}
	// Private Functions

}