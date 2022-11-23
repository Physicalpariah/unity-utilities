//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class SceneTabs : EditorWindow {
	// Properties
	private static SceneTabObject m_sceneTabs;
	// Add menu named "My Window" to the Window menu
	[MenuItem("Anchorite/SceneTab")]
	static void Init() {
		// Get existing open window or if none, make a new one:

		UpdateSceneTabObject();

		SceneTabs window = (SceneTabs)EditorWindow.GetWindow(typeof(SceneTabs));
		window.minSize = new Vector2(0, 8);
		window.Show();
	}

	private static void UpdateSceneTabObject() {
		Object obj = Resources.Load("sceneTabs");
		if (obj is SceneTabObject) {
			m_sceneTabs = (SceneTabObject)obj;
		}
	}

	void OnGUI() {

		if (m_sceneTabs == null) {
			UpdateSceneTabObject();
		}

		GUILayout.BeginHorizontal();

		if (m_sceneTabs != null) {
			foreach (SceneAsset scene in SceneTabs.m_sceneTabs.m_scenes) {
				if (GUILayout.Button("load scene: " + scene.name)) {
					string path = AssetDatabase.GetAssetPath(scene);
					EditorSceneManager.OpenScene(path);
				}
			}

		}
		else {
			foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
				if (GUILayout.Button("load scene: " + scene.path)) {
					EditorSceneManager.OpenScene(scene.path);
				}
			}

		}
		GUILayout.EndHorizontal();
	}


}