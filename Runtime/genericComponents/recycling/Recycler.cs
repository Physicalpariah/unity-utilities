//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class Recycler<T> : MonoBehaviour {
	// Properties
	public List<GameObject> m_spawnedItems;
	[SerializeField] protected GameObject m_spawnablePrefab;
	[SerializeField] protected GameObject m_holder;


	protected int m_activeItems {
		get {
			int count = 0;
			for (int a = 0; a < m_spawnedItems.Count; a++) {
				if (m_spawnedItems[a].gameObject.activeInHierarchy) {
					count++;
				}
			}
			return count;
		}
	}

	public CoreEvent e_itemSpawned;
	public CoreEvent e_allItemsSpawned;
	public CoreEvent e_itemDespawned;


	// Initalisation Functions

	public void SetPrefab(GameObject prefab) {
		m_spawnablePrefab = prefab;
	}

	public void Initialise() {
		Clear();
	}

	public void Initialise(List<T> data) {
		Initialise();
		Append(data);
	}

	// Unity Callbacks

	// Public Functions

	// Private Functions

	public virtual void Clear() {
		if (m_spawnedItems == null) {
			m_spawnedItems = new List<GameObject>();
		}

		foreach (GameObject cell in m_spawnedItems) {
			DeactivateCell(cell);
		}

		// cleanup... should only be required if somehow shit is left in the holder.
		for (int a = 0; a < m_holder.transform.childCount; a++) {
			if (m_holder.transform.GetChild(a).gameObject.activeInHierarchy) {
				ObjectUtils.DespawnItem(m_holder.transform.GetChild(a).gameObject);
			}
		}
	}

	public virtual void DeactivateCell(GameObject cell) {
		ObjectUtils.DespawnItem(cell);
	}

	public virtual void Append(List<T> data) {
		if (data == null) {
			LogUtils.LogWarning(this + " No data found");
			return;
		}

		if (m_holder.activeInHierarchy == false) {
			LogUtils.LogWarning("Nah not spawning anything, inactive");
			return;
		}

		for (int a = 0; a < data.Count; a++) {
			GameObject cell = CreateCell(data[a]);
		}

		if (e_allItemsSpawned != null) {
			e_allItemsSpawned();
		}
	}

	public virtual void Append(T data) {
		GameObject cell = CreateCell(data);
	}

	protected virtual GameObject CreateCell(object element) {
		return CreateCell(element, m_holder.transform, m_spawnablePrefab);
	}

	protected virtual GameObject CreateCell(object element, Transform holder, GameObject prefab) {
		GameObject obj = SpawnCell(element, holder, prefab);

		if (obj == null) {
			LogUtils.LogError("No item found");
			return null;
		}

		obj.transform.position = m_holder.transform.position;

		if (!m_spawnedItems.Contains(obj)) {
			m_spawnedItems.Add(obj);
		}

		if (e_itemSpawned != null) {
			e_itemSpawned();
		}

		return obj;
	}

	protected virtual GameObject SpawnCell(object element, Transform holder, GameObject prefab) {
		return ObjectUtils.SpawnWithData(prefab, holder.gameObject, this, element);
	}




}