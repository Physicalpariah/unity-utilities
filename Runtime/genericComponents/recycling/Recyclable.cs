//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class Recyclable : MonoBehaviour {
	// Properties
	protected object m_data;
	protected object m_manager;
	public int m_uniqueID;
	private int _id = -1;
	public int m_recycleID {
		set {
			if (_id == -1) {
				_id = value;
			}
		}
		get { return _id; }
	}

	public bool m_caresAboutInitBalance = true;
	private int m_initialisations = 0;
	private int m_enables = 0;

	// Initalisation Functions

	// Unity Callbacks
	protected virtual void OnEnable() {
		// LogUtils.Log("Enabling: " + gameObject.name);
		m_enables++;
	}
	// Public Functions
	public virtual void InitRecycleable(object[] data) {
		m_manager = data[0];
		m_data = data[1];
		// LogUtils.Log("initialising: " + gameObject.name);
		m_initialisations++;
		CheckInitBalance();
	}

	private void CheckInitBalance() {
		if (!m_caresAboutInitBalance) {
			return;
		}
		if (m_initialisations > m_enables) {
			LogUtils.LogError($"Recycleable {gameObject.name} + {m_recycleID}  pushed more inits than enables [{m_initialisations}] / [{m_enables}]");
		}
	}

	public void SetID(int id) {
		m_recycleID = id;
		// RenameObject();
	}

	public void SetUniqueID(int id) {
		m_uniqueID = id;
	}

	// private void RenameObject() {
	// 	string name = gameObject.name;
	// 	name = name.Replace("(Clone)", "_(" + m_recycleID + ")");
	// 	gameObject.name = name;
	// }

	public virtual void Deactivate() {
		CheckInitBalance();
		gameObject.SetActive(false);
	}
	// Private Functions

}



// m_manager = data[0];
// 	m_data = data[1];