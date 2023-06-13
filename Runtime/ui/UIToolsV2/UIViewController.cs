//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIViewController : MonoBehaviour {

	// Purpose:
	// A view controller manages a single set of views for your app, and it keeps the information in those views up-to-date

	// Dependencies
	protected UIWindow m_window;
	// Properties
	public List<UIView> m_views;
	public List<UIConnection> m_connections;
	public Button m_btnBack;
	// Initalisation Functions
	private void Start() {
		m_window = GetComponentInParent<UIWindow>();
		if (m_window == null) { throw new Exception("Nope, no window found for this view."); }
		m_window.RegisterView(this);


		foreach (UIConnection con in m_connections) {
			con.Apply(m_window);
		}

		SubBackButton();
	}




	// Unity Callbacks

	// Public Functions

	public void RegisterView(UIView view) {
		m_views.Add(view);
	}

	public void Open() {
		foreach (UIView view in m_views) {
			view.Open();
		}
	}

	public void Close() {
		foreach (UIView view in m_views) {
			view.Close();
		}
	}
	// Private Functions
	private void SubBackButton() {
		if (m_btnBack == null) { return; }
		UIUtils.SubscribeButton(m_btnBack, m_window.Back);
	}
}

[Serializable]
public class UIConnection {
	public string m_name;
	public UIViewController m_toView;
	public Button m_button;
	private UIWindow m_window;

	public void Apply(UIWindow window) {
		m_window = window;
		UIUtils.SubscribeButton(m_button, Segue);
	}

	private void Segue() {
		m_window.Segue(m_toView);
	}
}