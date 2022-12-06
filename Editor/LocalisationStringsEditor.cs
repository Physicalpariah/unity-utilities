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
	private Vector2 m_scrollPos;
	private string m_searchTerm;
	private SystemLanguage m_selectedLanguage = SystemLanguage.English;

	public bool m_editsKeys = false;

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
	[MenuItem("Anchorite/anchoriteutils/Localisation Editor")]
	static void Init() {
		_window = (LocalisationStringsEditor)EditorWindow.GetWindow(typeof(LocalisationStringsEditor));
	}

	// Unity Callbacks
	private void OnGUI() {
		GUILayout.Label("Localisation:");





		GUILayout.Space(10);
		// , m_selectedLanguage, Enum.GetNames(typeof(SystemLanguage)));

		int val = EditorGUILayout.Popup((int)m_selectedLanguage, Enum.GetNames(typeof(SystemLanguage)));
		m_selectedLanguage = (SystemLanguage)val;

		if (LocalisationStringsObject.Instance.m_currentLanguage != m_selectedLanguage) {
			LocalisationStringsObject.Instance.SetLanguage(m_selectedLanguage);
		}



		ShowEditor();

		if (GUILayout.Button("Write to Disk")) {
			LocalisationStringsObject.Instance.SaveToCSV();
		}

		if (GUILayout.Button("Load from Disk")) {
			LocalisationStringsObject.Instance.LoadDataFromCSV();
		}
	}

	private void ShowEditor() {


		AnchoriteEditorUtils.DrawUILine(Color.black);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Search:", GUILayout.Width(90));
		m_searchTerm = GUILayout.TextField(m_searchTerm);
		bool canAdd = true;
		if (LocalisationStringsObject.Instance.m_data.Contains(m_searchTerm)) {
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
					LocalisationStringsObject.Instance.Add(m_searchTerm);
					Clear();
				}
			}

			DisplayEditButton();
		}

		AnchoriteEditorUtils.DrawUILine(Color.black);




		GUILayout.BeginHorizontal();
		GUILayout.Label("Key", GUILayout.Width((Screen.width / 4) - 30)); // AnchoriteEditorUtils.m_boldStyle]
		GUILayout.Label("Current Value", GUILayout.Width((Screen.width / 4) - 30));
		GUILayout.EndHorizontal();

		m_scrollPos = GUILayout.BeginScrollView(m_scrollPos);
		for (int a = 0; a < LocalisationStringsObject.Instance.m_data.m_strings.Count; a++) {
			LocalisationString data = LocalisationStringsObject.Instance.m_data.m_strings[a];
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

	private void DisplayEditButton() {
		string editKey = "Edit Data";
		if (m_editsKeys) {
			editKey = "Stop Editing Data";
		}

		if (GUILayout.Button(editKey)) {
			m_editsKeys = !m_editsKeys;
		}
	}

	private void DisplayStringsResult(LocalisationString data) {
		GUILayout.BeginHorizontal();


		if (m_selectedLanguage != SystemLanguage.English) {
			m_editsKeys = false;
		}



		if (m_editsKeys) {
			data.m_default = GUILayout.TextField(data.m_default, GUILayout.Width((Screen.width / 4) - 30));
		}
		else {
			GUILayout.Label(data.m_default, GUILayout.Width((Screen.width / 4) - 30));
		}

		if (m_editsKeys) {
			data.m_current = GUILayout.TextField(data.m_current, GUILayout.Width((Screen.width / 4) - 30));
		}
		else {
			GUILayout.Label(data.m_current, GUILayout.Width((Screen.width / 4) - 30));
		}


		if (GUILayout.Button("X", GUILayout.Width(30))) {
			LocalisationStringsObject.Instance.m_data.m_strings.Remove(data);
			Clear();
		}
		GUILayout.EndHorizontal();
	}

	private void Clear() {
		m_searchTerm = "";
	}

	// Public Functions

	// Private Functions

}