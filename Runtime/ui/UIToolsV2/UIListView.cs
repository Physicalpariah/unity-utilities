//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIListView<T> : Recycler<T>, IView {
	// Dependencies
	protected UIViewController m_controller;
	// Properties
	public UIViewData _viewData { get { return m_viewData; } }
	public UIViewData m_viewData;


	// Properties

	// Initalisation Functions

	// Unity Callbacks
	private void Start() {
		m_controller = GetComponentInParent<UIViewController>();
		if (m_controller == null) { throw new Exception("Nope, no window found for this view."); }
		m_controller.RegisterView(this);
	}
	// Public Functions

	// Private Functions
	public virtual void Open() {
		gameObject.SetActive(true);
	}

	public virtual void Close() {
		gameObject.SetActive(false);
	}

	protected override GameObject SpawnCell(object element, Transform holder, GameObject prefab) {
		return ObjectUtils.SpawnUIWithData(prefab, holder.gameObject, m_controller, element);
	}

	public virtual void UpdateView() {
		if (gameObject.activeInHierarchy == false) { return; }
		OnViewUpdate();
	}

	public virtual void OnViewUpdate() {

	}
}
