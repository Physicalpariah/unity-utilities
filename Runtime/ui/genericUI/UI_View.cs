//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_View {
	// Properties

	[SerializeField] protected GameObject m_viewRoot;
	protected UI_ViewController m_controller;
	public CoreEvent e_viewInitialised;

	// Initalisation Functions

	public virtual void Initialise(UI_ViewController controller) {
		if (controller == null) {
			LogUtils.LogError("No controller detected");
		}
		m_controller = controller;
	}

	public virtual void Initialise(UI_ViewController controller, List<object> dependencies) {
		Initialise(controller);
	}

	public virtual void Initialise(UI_ViewController controller, List<object> dependencies, object data) {
		Initialise(controller);
	}

	public virtual void Initialise(UI_ViewController controller, object data) {
		Initialise(controller);
	}

	public virtual void ShutDown() {

	}

	public virtual void HideView() {
		m_viewRoot.SetActive(false);
	}

	public virtual void ShowView() {
		m_viewRoot.SetActive(true);
	}



	// Public Functions

	// Private Functions

}