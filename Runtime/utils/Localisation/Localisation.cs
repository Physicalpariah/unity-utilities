//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;

[CreateAssetMenuAttribute(menuName = "anchorite/utils/Localisation Strings Object")]
public class Localisation : ScriptableObject {
	// Properties
	public LocalisationStrings m_data;
	public SystemLanguage m_currentLanguage;

	[SerializeField] private string m_savePath = "/Resources/localisation_files/";
	[SerializeField] private string m_csvFileName = "localisation";
	private string m_extension = ".csv";

	private string m_fullFilePath {
		get {
			return Application.dataPath + m_savePath + m_csvFileName + "-" + m_currentLanguage + m_extension;
		}
	}

	private static Localisation _instance = null;
	public static Localisation Instance {
		get {
			if (_instance == null) {
				_instance = Resources.Load("data/localisation/strings") as Localisation;
				if (_instance == null) {
					Exception ex = new Exception("Whoa no localisation info, should be located at Resources/data/localisation/strings");
					Debug.LogException(ex);
				}
			}
			return _instance;
		}
	}

	public Action e_languageChanged;
	private void RaiseLanguageChanged() {
		if (e_languageChanged != null) {
			e_languageChanged();
		}
	}
	// Initalisation Functions

	// Unity Callbacks

	public static void SetLanguage(SystemLanguage lang) {
		Instance.m_currentLanguage = lang;
		Instance.LoadDataFromCSV();
		Instance.RaiseLanguageChanged();
	}

	// Public Functions

	public static string UseKey(string key) {
		Add(key);
		return Get(key);
	}

	public static void Add(string title) {
		if (!string.IsNullOrWhiteSpace(title)) {
			if (!Instance.m_data.Contains(title)) {
				Instance.m_data.m_strings.Add(new LocalisationString(title));
			}
		}

		Localisation.Instance.SaveToCSV();
	}

	public static string Search(string key) {
		for (int a = 0; a < Instance.m_data.m_strings.Count; a++) {
			LocalisationString data = Instance.m_data.m_strings[a];
			if (data.m_default.ToLower().Contains(key.ToLower())) {
				return data.m_default;
			}
		}

		return null;
	}

	public static string Get(string key) {
		if (Instance.m_data.Contains(key) == false) {
#if UNITY_EDITOR
			Add(key);
			foreach (LocalisationString str in Instance.m_data.m_strings) {
				if (str.m_default.ToLower() == key.ToLower()) {
					return str.m_current;
				}
			}
#endif
			throw new Exception("No localisable string of default value: " + key);
		}

		foreach (LocalisationString str in Instance.m_data.m_strings) {
			if (str.m_default.ToLower() == key.ToLower()) {
				return str.m_current;
			}
		}

		return null;
	}




	public void SaveToCSV() {
		CreateFile(m_fullFilePath);
		AppendToFile(m_fullFilePath, m_data.m_strings);
	}

	private void AppendToFile(string path, List<LocalisationString> data) {

		List<List<string>> rows = new List<List<string>>();

		foreach (LocalisationString text in data) {
			List<string> cols = new List<string>();
			cols.Add("\"" + text.m_default + "\"");
			cols.Add("\"" + text.m_current + "\"");
			rows.Add(cols);
		}

		// List<string> headers = GetHeaders();

		using (StreamWriter sw = File.AppendText(path)) {
			string dat = "";

			for (int a = 0; a < rows.Count; a++) {
				for (int i = 0; i < rows[a].Count; i++) {
					// if the final string is not a space
					if (dat != "" && dat[dat.Length - 1] != '\n') {
						// append a comma
						dat += ",";
					}
					// else append the list of strings
					dat += rows[a][i];
				}
				dat += "\n";
			}

			// when we're done, add one last comma and.. a timestamp?
			dat += ",";
			// write the line
			sw.WriteLine(dat);
		}
	}

	public void CreateFile(string path) {
		List<string> headers = new();

		using (StreamWriter sw = File.CreateText(path)) {

			string finalString = "";
			for (int i = 0; i < headers.Count; i++) {
				if (finalString != "") {
					finalString += ",";
				}
				finalString += headers[i];
			}
			finalString += ",";
			sw.WriteLine(finalString);
		}
	}


	public void LoadDataFromCSV() {
		List<LocalisationString> loadedText = new List<LocalisationString>();

		if (File.Exists(m_fullFilePath)) {
			using (CsvReader reader = new CsvReader(m_fullFilePath)) {
				// int row = 0;
				foreach (string[] values in reader.RowEnumerator) {
					LocalisationString text = new LocalisationString();

					// read columns
					if (values.Length > 1) {
						text.m_default = values[0];
						text.m_current = values[1];
					}

					// save to LocalisableText
					loadedText.Add(text);
				}
			}
		}
		else {
			Debug.Log("No file found");
			return;
		}

		// iterate through the existing localisation data and update it
		if (loadedText.Count > 0) {
			List<LocalisationString> newAssets = new List<LocalisationString>();
			foreach (LocalisationString text in loadedText) {

				bool contains = false;
				foreach (LocalisationString obj in Localisation.Instance.m_data.m_strings) {
					if (obj.m_default == text.m_default) {
						obj.m_current = text.m_current;
						contains = true;
					}
				}

				if (!contains) {
					if (string.IsNullOrWhiteSpace(text.m_default) == false) {
						newAssets.Add(text);
					}
				}
			}

			foreach (LocalisationString str in newAssets) {
				if (!loadedText.Contains(str)) {
					loadedText.Add(str);
				}
			}

			m_data.m_strings = loadedText;


#if UNITY_EDITOR
			EditorUtility.SetDirty(Instance);
#endif

		}
		else {
			Debug.Log("Couldnt extract assets from file");
		}


	}

	// Private Functions

}