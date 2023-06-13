//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIView : MonoBehaviour {
	// Purpose:
	// The views provide the actual content that users see onscreen, drawing text, images, and other types of custom content.

	// Dependencies
	protected UIViewController m_controller;
	// Properties
	public UIViewData m_viewData;

	// Initalisation Functions

	// Unity Callbacks
	private void Start() {
		m_controller = GetComponentInParent<UIViewController>();
		if (m_controller == null) { throw new Exception("Nope, no window found for this view."); }
		m_controller.RegisterView(this);
	}

	// Public Functions
	public virtual void Open() {
		gameObject.SetActive(true);
	}

	public virtual void Close() {
		gameObject.SetActive(false);
	}
	// Private Functions


}

[Serializable]
public struct UIViewData {
	public n_viewType m_viewType;
}


public enum n_viewType {
	subView,
	exclusive,
	modal,
}

