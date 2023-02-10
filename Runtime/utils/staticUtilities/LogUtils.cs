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
		// if (Application.platform == RuntimePlatform.OSXEditor) {
		if (check) {
			Debug.Log(log);
		}
		// }
	}

	public static void LogPriority(object log, bool check = true) {
		// if (Application.platform == RuntimePlatform.OSXEditor) {
		if (check) {
			Debug.Log("Priority: <color=#00BDF7>" + log + "</color>");
		}
		// }
	}

	public static void LogIssue(object log, bool check = true) {
		// if (Application.platform == RuntimePlatform.OSXEditor) {
		if (check) {
			Debug.Log("Issue: <color=#C13F3F>" + log + "</color>");
		}
		// }
	}

	public static void LogTodo(object log, bool check = true) {
		// if (Application.platform == RuntimePlatform.OSXEditor) {
		if (check) {
			Debug.Log("TODO: <color=#FF00B1>" + log + "</color>");
		}
		// }
	}

	public static void LogWarning(object log, bool check = true) {
		if (check) {
			Debug.LogWarning(log);
		}
	}

	public static void LogError(object log, bool check = true) {
		if (check) {
			Debug.LogError(log);
		}
	}

	public static void LogNotImplemented(object log = null, bool check = true) {
		if (check) {
			Debug.LogError("Function not yet Implemented: " + log);
		}
	}
	// Private Functions

}