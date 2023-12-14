//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(UIMediaQuery))]
public class UIMediaQueryEditor : Editor {

	// Properties
	public delegate void d_queryFired(MediaQueryTrigger trigger);
	public static event d_queryFired e_queryFired;
	// Public Functions
	public override void OnInspectorGUI() {
		UIMediaQuery handler = (UIMediaQuery)target;
		DrawDefaultInspector();
		if (handler.m_queries.Count > 0) {
			GUILayout.Label("Apply Layout:");

			for (int a = 0; a < handler.m_queries.Count; a++) {
				if (GUILayout.Button(handler.m_queries[a].m_trigger.name)) {

					if (e_queryFired != null) {
						e_queryFired(handler.m_queries[a].m_trigger);
					}


					handler.m_queries[a].Activate();
					UIMediaQuery[] subQueries = handler.GetComponentsInChildren<UIMediaQuery>();

					for (int b = 0; b < subQueries.Length; b++) {
						foreach (MediaQuery query in subQueries[b].m_queries) {
							if (query.m_trigger.name == handler.m_queries[a].m_trigger.name) {
								query.Activate();


							}
						}
					}
				}
			}



		}
	}
}