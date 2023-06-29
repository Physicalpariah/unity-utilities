//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif




public class ObjectRecycler {
	// Properties
	// public List<RecycleablePrefabContainer> m_registeredPrefabs;
	public Dictionary<string, RecycleablePrefabContainer> m_registeredPrefabDict;

	private static ObjectRecycler m_instance;
	public static ObjectRecycler Instance {
		get {
			if (m_instance == null) {
				m_instance = new ObjectRecycler();
				MemoryHandler.e_gcWillClear += m_instance.ClearUnusedItems;
			}

			return m_instance;
		}
	}


	public int m_recycleCount { get; private set; }



	// Initalisation Functions

	public ObjectRecycler() {
		m_registeredPrefabDict = new Dictionary<string, RecycleablePrefabContainer>();

		SceneManager.sceneLoaded -= ClearPrefabContainers;
		SceneManager.sceneUnloaded -= UnloadScene;

		SceneManager.sceneLoaded += ClearPrefabContainers;
		SceneManager.sceneUnloaded += UnloadScene;

#if UNITY_EDITOR
		EditorApplication.playModeStateChanged -= OnPlayModeExit;
		EditorApplication.playModeStateChanged += OnPlayModeExit;
#endif
	}

	// Public Functions
	public void DespawnItem(GameObject prefab) {

		bool success = false;

		if (prefab == null) {
			return;
		}


		RecycleablePrefabContainer recycle = null;

		if (m_registeredPrefabDict.ContainsKey(GetNameWithoutClone(prefab))) {
			recycle = m_registeredPrefabDict[GetNameWithoutClone(prefab)];
		}

		if (recycle != null) {
			recycle.DeactivateRecycleable(prefab);
			success = true;
		}

		if (!success) {
			if (prefab != null) {
				LogUtils.LogIssue($"{prefab} is not a registered recycleable, destroying");
				GameObject.Destroy(prefab);
			}
		}
	}


	public void IncrementRecycleablesCount() {
		m_recycleCount++;
	}

	public GameObject SpawnItem(GameObject prefab) {
		return SpawnItem(prefab, null, null);
	}

	public GameObject SpawnItem(GameObject prefab, object data, object manager, GameObject parent = null) {

		// Debug.Log($"Spawning: {prefab}");

		Recyclable recyclable = GetItem(prefab);

		if (recyclable == null) {
			LogUtils.LogError("item is not recyclable: " + prefab);
			return null;
		}

		if (parent != null) {
			recyclable.transform.SetParent(parent.transform);
			recyclable.transform.SetAsLastSibling();
		}

		object[] dat = { manager, data };
		recyclable.InitRecycleable(dat);

		return recyclable.gameObject;
	}


	public void ClearPrefabContainers(Scene scene, LoadSceneMode mode) {
		Clear();
	}

	private void UnloadScene(Scene scene) {
		Clear();
	}

#if UNITY_EDITOR
	private void OnPlayModeExit(PlayModeStateChange change) {
		Clear();
	}
#endif

	private void Clear() {
		m_registeredPrefabDict.Clear();
	}
	// Private Functions
	private Recyclable GetItem(GameObject prefab) {
		if (m_registeredPrefabDict.ContainsKey(GetNameWithoutClone(prefab))) {
			return m_registeredPrefabDict[GetNameWithoutClone(prefab)].GetRecycleable(prefab);
		}
		// we assume no recycler exists for this?
		return CreateContainer(prefab).GetRecycleable(prefab);
	}

	private RecycleablePrefabContainer CreateContainer(GameObject prefab) {
		RecycleablePrefabContainer container = new RecycleablePrefabContainer(prefab);
		m_registeredPrefabDict.Add(prefab.name, container);
		return container;
	}


	public void ClearUnusedItems() {
		foreach (KeyValuePair<string, RecycleablePrefabContainer> entry in m_registeredPrefabDict) {
			entry.Value.ClearContainer();
		}
	}

	private string GetNameWithoutClone(GameObject gameObject) {
		return gameObject.name.Replace("(Clone)", "");
	}
}