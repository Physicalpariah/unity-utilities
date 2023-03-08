//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;

public class SceneTabs : EditorWindow {
	// Properties
	private static SceneTabObject m_sceneTabs;
	private static string m_filePath = "data/sceneTabs/";
	private static string m_fileName = "sceneTabs";
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
		Object obj = Resources.Load(m_filePath + m_fileName);
		if (obj is SceneTabObject) {
			m_sceneTabs = (SceneTabObject)obj;
		}

		if (obj == null) {
			SceneTabObject sceneTab = ScriptableObject.CreateInstance<SceneTabObject>();
			string filePath = "Assets/Resources/" + m_filePath + m_fileName + ".asset";

			string dirPath = Application.dataPath + "/Resources/" + m_filePath;
			// LogUtils.Log("Checking dir at path:" + dirPath);
			if (!Directory.Exists(dirPath)) {
				// LogUtils.Log("Creating directory at:" + dirPath);
				Directory.CreateDirectory(dirPath);
				AssetDatabase.Refresh();
			}

			AssetDatabase.CreateAsset(sceneTab, filePath);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			m_sceneTabs = sceneTab;

			foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
				SceneAsset bleh = AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
				m_sceneTabs.m_scenes.Add(bleh);
			}
		}
	}

	private void OnGUI() {
		SceneTabs.DrawGUI();
	}

	public static void DrawGUI() {

		if (m_sceneTabs == null) {
			UpdateSceneTabObject();
		}

		GUILayout.BeginVertical();

		if (m_sceneTabs != null) {

			GUILayout.BeginHorizontal();
			float width = 0;
			foreach (SceneAsset scene in m_sceneTabs.m_scenes) {

				if (GUILayout.Button(scene.name)) {
					string path = AssetDatabase.GetAssetPath(scene);
					EditorSceneManager.OpenScene(path);
				}
				var rect = GUILayoutUtility.GetLastRect();
				// LogUtils.Log(rect.GetType());

				width += scene.name.Length * 24; // TODO: Magic number yo.
												 // LogUtils.Log("Screen width: " + Screen.width + " rect Width: " + rect.width + " total width: " + width);

				if (width > Screen.width) {
					width = 0;
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}

			}
			GUILayout.EndHorizontal();

		}

		GUILayout.EndVertical();
	}


}