//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class RecycleablePrefabContainer {
	// Properties
	public List<Recyclable> m_spawnedItems { get; private set; }
	public List<Recyclable> m_inactiveItems { get; private set; }
	public List<Recyclable> m_activeItems { get; private set; }
	public GameObject m_spawnablePrefab { get; private set; }
	public int m_rawSpawns { get; private set; } = 0;
	private int m_maxSpawns = 1000;

	private GameObject m_deactivatedHolder;

	// Initalisation Functions
	public RecycleablePrefabContainer(GameObject prefab) {
		m_spawnedItems = new List<Recyclable>();
		m_activeItems = new List<Recyclable>();
		m_inactiveItems = new List<Recyclable>();
		m_spawnablePrefab = prefab;

		if (m_deactivatedHolder == null) {
			m_deactivatedHolder = GameObject.Find("deactivated-recycleable-container");

			if (m_deactivatedHolder == null) {
				m_deactivatedHolder = new();
				m_deactivatedHolder.name = "deactivated-recycleable-container";
			}
		}
	}

	// Public Functions
	public Recyclable GetRecycleable(GameObject prefab) {
		// 1)
		// if we've maxxed out our items, and don't have one to recyle, recycle an active one.		
		if (m_rawSpawns >= m_maxSpawns && m_inactiveItems.Count == 0) {
			return GetExistingActiveItem();
		}

		// 2)
		// if we have inactive items, grab the first one that isn't null.
		if (m_inactiveItems.Count > 0) {
			return GetInactiveItem();
		}

		// 2.1) if we've maxed out spawns, and we care about balance, and there's no inactive items, and we haven't yet got an item, we're in trouble.
		if (m_rawSpawns >= m_maxSpawns + 1) {
			if (m_spawnedItems[0].m_recycleData.m_caresAboutInitBalance) {
				Debug.LogError("no inactive items for prefab:" + prefab.name);
			}
		}

		// 3)
		// if all of the above is not correct, create a new prefab.
		return CreateNewItem(prefab); ;
	}

	public void DeactivateRecycleable(GameObject prefab) {
		if (prefab == null) {
			return;
		}

		if (prefab.gameObject.activeSelf == false) {
			return;
		}

		Recyclable recycleable = prefab.GetComponent<Recyclable>();
		if (m_activeItems.Contains(recycleable)) {
			m_activeItems.Remove(recycleable);
		}

		if (recycleable == null) {
			LogUtils.LogError("Uh oh");
		}

		m_inactiveItems.Add(recycleable);

		prefab.gameObject.SetActive(false);
		prefab.transform.SetParent(m_deactivatedHolder.transform);
	}

	public void ClearContainer() {
		for (int a = 0; a < m_inactiveItems.Count; a++) {
			m_spawnedItems.Remove(m_inactiveItems[a]);
			GameObject.Destroy(m_inactiveItems[a].gameObject);
		}
		m_inactiveItems.Clear();
		m_rawSpawns = m_spawnedItems.Count;
	}

	// Private Functions
	private Recyclable GetInactiveItem() {
		Recyclable recyclable = null;
		for (int a = 0; a < m_inactiveItems.Count; a++) {
			recyclable = m_inactiveItems[a];
			if (recyclable != null) {
				if (recyclable.gameObject != null) {
					ActivateItem(recyclable);
					return recyclable;
				}
			}
			else {
				m_inactiveItems.Remove(m_inactiveItems[a]);
			}
		}

		return recyclable;
	}

	private Recyclable CreateNewItem(GameObject prefab) {
		GameObject newItem = ObjectUtils.SpawnRaw(prefab);
		Recyclable recyclable = newItem.GetComponent<Recyclable>();

		newItem.SetActive(true);

		recyclable.SetID(m_rawSpawns);
		ObjectRecycler.Instance.IncrementRecycleablesCount();
		recyclable.SetUniqueID(ObjectRecycler.Instance.m_recycleCount);
		m_rawSpawns++;
		if (recyclable.m_recycleData.m_caresAboutInitBalance) {
			if (m_rawSpawns >= m_maxSpawns + 1) {
				LogUtils.LogError($"Whoa there pardner,{newItem.name} has exceeded max spawns!, this is highly illegal.");
			}
		}
		m_spawnedItems.Add(recyclable);
		m_activeItems.Add(recyclable);
		return recyclable;
	}

	private Recyclable GetExistingActiveItem() {
		// if we've got nothing to return, do so.
		if (m_activeItems.Count == 0) {
			return null;
		}

		Recyclable active = null;
		int index = 0;
		bool foundActive = false;
		while (!foundActive) {

			// if the current item isn't null
			if (m_activeItems[index] != null) {
				// if the current item's gameobject isnt null
				if (m_activeItems[index].gameObject != null) {
					// get the current item
					active = m_activeItems[index];
					foundActive = true;
				}
			}
			else {
				m_activeItems.Remove(m_activeItems[index]);
			}
			index++;

			if (index >= m_activeItems.Count) {
				foundActive = true;
			}
		}

		if (active != null) {
			DeactivateRecycleable(active.gameObject);
			ActivateItem(active);
		}
		return active;
	}

	private void ActivateItem(Recyclable item) {
		m_inactiveItems.Remove(item);
		m_activeItems.Add(item);
		item.gameObject.SetActive(true);
	}
}
