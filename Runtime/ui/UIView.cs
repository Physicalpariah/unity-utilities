//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public interface IView {
	UIViewData _viewData { get; }
	void Close();
	void Open();
	void UpdateView();
	void OnViewUpdate();
	void Unsubscribe();
}

public class UIView : MonoBehaviour, IView {
	// Purpose:
	// The views provide the actual content that users see onscreen, drawing text, images, and other types of custom content.

	// Dependencies
	protected UIViewController m_controller;
	// Properties
	public UIViewData _viewData { get { return m_viewData; } }
	public UIViewData m_viewData;

	// Initalisation Functions

	// Unity Callbacks
	private void Awake() {
		m_controller = GetComponentInParent<UIViewController>();
		if (m_controller == null) { throw new Exception("Nope, no window found for this view."); }
		m_controller.RegisterView(this);
	}

	// Public Functions
	public virtual void Open() {
		gameObject.SetActive(true);
		m_controller.SelectFirst();
	}

	public virtual void Close() {
		gameObject.SetActive(false);
		Unsubscribe();
	}

	public virtual void UpdateView() {
		if (gameObject.activeInHierarchy == false) { return; }
		OnViewUpdate();
	}

	public virtual void OnViewUpdate() {

	}

	private void OnDestroy() { Unsubscribe(); }
	// private void OnDisable() { Unsubscribe(); }
	private void OnApplicationQuit() { Unsubscribe(); }

	public virtual void Unsubscribe() {

	}
	// Private Functions


}

[Serializable]
public struct UIViewData {
	public n_viewType m_viewType;
}


public enum n_viewType {
	open_on_open,
	exclusive,
	close_on_open,
	do_nothing,
}

