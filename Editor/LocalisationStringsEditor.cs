//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor;
using UnityEngine;

public class LocalisationStringsEditor : EditorWindow {
	// Properties
	private static LocalisationStringsEditor _window;
	private static Vector2 m_scrollPos;
	private static string m_searchTerm;
	private static SystemLanguage m_selectedLanguage = SystemLanguage.English;

	public static bool m_editsKeys = false;

	private static LocalisationStringsEditor s_window {
		get {
			if (_window == null) {
				_window = (LocalisationStringsEditor)EditorWindow.GetWindow(typeof(LocalisationStringsEditor));
				_window.minSize = Vector2.zero;
			}
			return _window;
		}
	}

	// Initalisation Functions
	[MenuItem("Anchorite/utils/Localisation Editor")]
	static void Init() {
		_window = (LocalisationStringsEditor)EditorWindow.GetWindow(typeof(LocalisationStringsEditor));
		AssemblyReloadEvents.afterAssemblyReload += _window.OnAfterAssemblyReload;
	}

	public void OnDisable() {
		AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
	}

	// Unity Callbacks
	private void OnAfterAssemblyReload() {
		Localisation.Instance.LoadDataFromCSV();
	}

	private void OnGUI() {
		DrawGUI();
	}

	public static void DrawGUI() {
		GUILayout.Label("Localisation:");

		GUILayout.Space(10);
		// , m_selectedLanguage, Enum.GetNames(typeof(SystemLanguage)));

		int val = EditorGUILayout.Popup((int)m_selectedLanguage, Enum.GetNames(typeof(SystemLanguage)));
		m_selectedLanguage = (SystemLanguage)val;

		if (Localisation.Instance.m_currentLanguage != m_selectedLanguage) {
			Localisation.SetLanguage(m_selectedLanguage);
		}



		ShowEditor();

		if (GUILayout.Button("Write to Disk")) {
			Localisation.Instance.SaveToCSV();
		}

		if (GUILayout.Button("Load from Disk")) {
			Localisation.Instance.LoadDataFromCSV();
		}
	}

	private static void ShowEditor() {


		AnchoriteEditorUtils.DrawUILine(Color.black);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Search:", GUILayout.Width(90));
		m_searchTerm = GUILayout.TextField(m_searchTerm);
		bool canAdd = true;
		if (Localisation.Instance.m_data.Contains(m_searchTerm)) {
			canAdd = false;
		}

		if (m_selectedLanguage != SystemLanguage.English) {
			canAdd = false;
		}

		if (GUILayout.Button("X", GUILayout.Width(30))) {
			Clear();
		}


		GUILayout.EndHorizontal();



		if (m_selectedLanguage == SystemLanguage.English) {
			if (!canAdd) {
				GUILayout.Label("Cannot add, already exists", AnchoriteEditorUtils.m_boldErrorStyle);
			}
			if (GUILayout.Button("Add New")) {
				if (canAdd) {
					Localisation.Add(m_searchTerm);
					Clear();
				}
			}

			DisplayEditButton();
		}

		AnchoriteEditorUtils.DrawUILine(Color.black);




		GUILayout.BeginHorizontal();
		GUILayout.Label("Key", m_width); // AnchoriteEditorUtils.m_boldStyle]
		GUILayout.Label("Current Value", m_width);
		GUILayout.Label("Comment", m_width);
		GUILayout.EndHorizontal();

		m_scrollPos = GUILayout.BeginScrollView(m_scrollPos);
		for (int a = 0; a < Localisation.Instance.m_data.m_strings.Count; a++) {
			LocalisationString data = Localisation.Instance.m_data.m_strings[a];
			if (string.IsNullOrWhiteSpace(m_searchTerm)) {
				DisplayStringsResult(data);
			}
			else {
				if (data.m_default.ToLower().Contains(m_searchTerm.ToLower())) {
					DisplayStringsResult(data);
				}
			}
		}


		GUILayout.EndScrollView();

		AnchoriteEditorUtils.DrawUILine(Color.black);
	}

	private static void DisplayEditButton() {
		string editKey = "Edit Data";
		if (m_editsKeys) {
			editKey = "Stop Editing Data";
		}

		if (GUILayout.Button(editKey)) {
			m_editsKeys = !m_editsKeys;
			if (!m_editsKeys) {
				Localisation.Instance.SaveToCSV();
			}
		}
	}

	private static void DisplayStringsResult(LocalisationString data) {
		GUILayout.BeginHorizontal();

		if (m_selectedLanguage != SystemLanguage.English) {
			m_editsKeys = false;
		}

		if (m_editsKeys) {
			data.m_default = GUILayout.TextField(data.m_default, m_width);
		}
		else {
			GUILayout.Label(data.m_default, m_width);
		}

		if (m_editsKeys) {
			data.m_current = GUILayout.TextField(data.m_current, m_width);
		}
		else {
			GUILayout.Label(data.m_current, m_width);
		}

		data.m_comment = GUILayout.TextField(data.m_comment, m_width);


		if (GUILayout.Button("-", GUILayout.Width(30))) {
			Localisation.Instance.m_data.m_strings.Remove(data);
			Clear();
			Localisation.Instance.SaveToCSV();
		}
		GUILayout.EndHorizontal();
	}

	private static GUILayoutOption m_width {
		get {
			float w = (Screen.width / 5) - 60;
			return GUILayout.Width(w);
		}
	}

	private static void Clear() {
		m_searchTerm = "";
	}

	// Public Functions

	// Private Functions

}