//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_View : MonoBehaviour {
	// Properties

	[SerializeField] protected GameObject m_viewRoot;
	protected UI_ViewController m_controller;
	public CoreEvent e_viewInitialised;
	public bool m_viewShown;

	// Initalisation Functions

	public virtual void Initialise(UI_ViewController controller) {
		if (controller == null) {
			LogUtils.LogError("No controller detected");
		}
		m_controller = controller;

		if (m_viewRoot == null) {
			m_viewRoot = gameObject;
		}

	}


	public virtual void Initialise(UI_ViewController controller, object data) {
		Initialise(controller);
	}


	public virtual void ShutDown() {

	}

	public virtual void HideView() {
		if (m_viewShown == false) { return; }
		m_viewShown = false;
		m_viewRoot.SetActive(false);
	}

	public virtual void ShowView() {
		if (m_viewShown) { return; }
		m_viewShown = true;
		m_viewRoot.SetActive(true);
	}



	// Public Functions

	// Private Functions

}