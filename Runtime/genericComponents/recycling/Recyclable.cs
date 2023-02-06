//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class Recyclable : MonoBehaviour {
	// Properties
	protected object m_data;
	protected object m_manager;

	public RecycleableData m_recycleData;

	// Initalisation Functions

	// Unity Callbacks
	protected virtual void OnEnable() {
		m_recycleData.m_enables++;
	}
	// Public Functions
	public virtual void InitRecycleable(object[] data) {
		m_manager = data[0];
		m_data = data[1];
		m_recycleData.m_initialisations++;
		CheckInitBalance();
	}

	private void CheckInitBalance() {
		if (!m_recycleData.m_caresAboutInitBalance) {
			return;
		}
		if (m_recycleData.m_initialisations > m_recycleData.m_enables) {
			LogUtils.LogError($"Recycleable {gameObject.name} + {m_recycleData.m_recycleID}  pushed more inits than enables [{m_recycleData.m_initialisations}] / [{m_recycleData.m_enables}]");
		}
	}

	public void SetID(int id) {
		m_recycleData.m_recycleID = id;
		// RenameObject();
	}

	public void SetUniqueID(int id) {
		m_recycleData.m_uniqueID = id;
	}

	public virtual void Deactivate() {
		CheckInitBalance();
		gameObject.SetActive(false);
	}
	// Private Functions

}
