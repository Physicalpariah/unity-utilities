//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

public static class LogUtils {
	// Properties


	// Initalisation Functions

	// Public Functions
	public static void ForceLog(object log) {
		Debug.Log(log);
	}

	public static void Log(object log, bool check = true) {
		if (!LogUtilsConfig.Instance.m_doesBasicLog) { return; }
		if (!check) { return; }
		Debug.Log(AssembleLog("", "", log));
	}

	public static void LogPriority(object log, bool check = true) {
		if (!LogUtilsConfig.Instance.m_doesPriorityLog) { return; }
		if (!check) { return; }

		Debug.Log(AssembleLog("Priority:", "#00BDF7", log));
	}


	public static void LogIssue(object log, bool check = true) {
		if (!LogUtilsConfig.Instance.m_doesIssueLog) { return; }
		if (!check) { return; }
		Debug.Log(AssembleLog("Issue:", "#C13F3F", log));

	}

	public static void LogTodo(object log, bool check = true) {
		if (!LogUtilsConfig.Instance.m_doesTODOLog) { return; }
		if (!check) { return; }
		Debug.Log(AssembleLog("TODO:", "#FF00B1", log));

	}

	public static void LogImportant(object log, bool check = true) {
		if (!LogUtilsConfig.Instance.m_doesTODOLog) { return; }
		if (!check) { return; }
		Debug.Log(AssembleLog("IMPORTANT:", "#B5FFFE", log));
		Debug.Break();
	}

	public static void LogWarning(object log, bool check = true) {
		if (!LogUtilsConfig.Instance.m_doesWarningLog) { return; }
		if (!check) { return; }
		Debug.LogWarning(AssembleLog("Warning", "", log));

	}

	public static void LogError(object log, bool check = true) {
		if (!check) { return; }
		Debug.LogError(AssembleLog("Error", "", log));

	}

	public static void LogNotImplemented(object log = null, bool check = true) {
		if (!check) { return; }
		Debug.LogError(AssembleLog("Not Implemented", "", log));
	}

	public static string AssembleLog(string logtype, string color, object log) {
		string logPrefix = "-LOG:" + logtype;
		string logSuffix = "";

		if (!DeviceUtils.m_isApplePlatform) {
			logPrefix = "";
			if (!String.IsNullOrWhiteSpace(color)) {
				logPrefix += "<color=" + color + ">";
				logSuffix += "</color>";
			}
		}


		return logPrefix + log + logSuffix;
	}

	// Private Functions

}