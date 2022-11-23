//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "anchorite/Build Info")]
public class BuildInfoData : ScriptableObject {

	// Variables
	// public string m_testParse;
	public int m_currentBuild;
	public float m_currentVersion;

	// Properties
	private static BuildInfoData _instance = null;
	public static BuildInfoData Instance {
		get {
			if (_instance == null) {
				_instance = Resources.Load("data/build/build-info") as BuildInfoData;

				if (_instance == null) {
					Exception ex = new Exception("Whoa no build info, should be located at Resources/Data/build-info");
					Debug.LogException(ex);
				}
			}
			return _instance;
		}
	}

	public string m_currentVersionNumber {
		get {
			return m_currentVersion.ToString("0.000");
		}
	}
	public string m_currentBuildNumber {
		get {
			return m_currentBuild.ToString();
		}
	}

	// Functions
	public void ResetBuild() {
		m_currentBuild = 0;
	}

	public void SetVersionNumber(float version) {
		if (m_currentVersion < version) {
			m_currentVersion = version;
		}
	}

	public void SetBuildNumber(int build) {
		if (m_currentBuild < build) {
			m_currentBuild = build;
		}
	}

	public string GetCurrentFullVersionNumber() {
		string versionNumber = m_currentVersionNumber + "." + m_currentBuildNumber;
		return versionNumber;
	}

	public float ParseVersionNumber(string number) {

		if (!string.IsNullOrEmpty(number)) {
			string[] lines = number.Split('.');
			float majorVersion = 0;
			if (lines.Length >= 1) {
				majorVersion = int.Parse(lines[0]);
			}
			float minorVersion = 0;
			if (lines.Length >= 2) {
				minorVersion = int.Parse(lines[1]);
			}

			minorVersion /= 1000; // to make minor version a decimal.
			float version = majorVersion + minorVersion;
			return version;
		}

		LogUtils.LogError("Couldnt parse version, returning zero");
		return 0;
	}

	public int ParseBuildNumber(string number) {

		if (!string.IsNullOrEmpty(number)) {

			string[] lines = null;
			try {
				lines = number.Split('.');
			}
			catch (System.Exception e) {
				LogUtils.Log(e.Message);
				throw;
			}

			// lines = number.Split('.');
			float majorVersion = 0;
			if (lines.Length >= 1) {
				majorVersion = int.Parse(lines[0]);
			}
			float minorVersion = 0;
			if (lines.Length >= 2) {
				minorVersion = int.Parse(lines[1]);
			}
			int buildVersion = 0;
			if (lines.Length >= 3) {
				buildVersion = int.Parse(lines[2]);
			}


			return buildVersion;
		}

		LogUtils.LogError("Couldnt parse build, returning zero");
		return 0;
	}
}
