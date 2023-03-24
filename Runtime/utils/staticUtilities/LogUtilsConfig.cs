//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenuAttribute(menuName = "anchorite/utils/LogUtils Config")]
public class LogUtilsConfig : ScriptableObject {
	// Properties   
	public bool m_doesBasicLog = true;
	public bool m_doesPriorityLog = true;
	public bool m_doesWarningLog = true;
	public bool m_doesIssueLog = true;
	public bool m_doesTODOLog = true;


	private static string m_loadPath = "data/logutils";
	private static LogUtilsConfig m_instance;
	public static LogUtilsConfig Instance {
		get {
			if (m_instance == null) {
				m_instance = Resources.Load(m_loadPath) as LogUtilsConfig;
				if (m_instance == null) {
					Exception ex = new Exception($"Whoa no Logutils Config , should be located at Resources/{m_loadPath}");
					Debug.LogException(ex);
				}
			}
			return m_instance;
		}
	}
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions

	// Private Functions

}
