//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class UIViewController : MonoBehaviour {

	// Purpose:
	// A view controller manages a single set of views for your app, and it keeps the information in those views up-to-date

	// Dependencies
	public UIWindow m_window { get; private set; }
	// Properties
	public List<IView> m_views;
	public List<UIConnection> m_connections;
	public Button m_btnBack;

	// Initalisation Functions
	private void Start() {
		m_window = GetComponentInParent<UIWindow>();

		EnableViews();

		if (m_window == null) { throw new Exception("Nope, no window found for this view."); }
		m_window.RegisterView(this);

		foreach (UIConnection con in m_connections) {
			con.Apply(m_window);
		}

		SubBackButton();
	}

	private void Subscribe() {
		UIMediaQueryHandler.e_mediaChanged += OnOrientationChanged;
	}

	private void Unsubscribe() {
		foreach (IView view in m_views) {
			if (view != null) {
				view.Unsubscribe();
			}
		}
		UIMediaQueryHandler.e_mediaChanged -= OnOrientationChanged;
	}


	// Unity Callbacks
	private void OnDestroy() {
		Unsubscribe();
	}
	private void OnApplicationQuit() {
		Unsubscribe();
	}

	private void OnDisable() {
		Unsubscribe();
	}

	// Public Functions
	public UIConnection GetConnectionToScreenByType<T>() {
		foreach (UIConnection conn in m_connections) {
			if (conn is T) {
				return conn;
			}
		}

		return null;
	}


	public void RegisterView(IView view) {
		if (m_views == null) {
			m_views = new();
		}

		m_views.Add(view);
	}

	public virtual void Open() {
		gameObject.SetActive(true);
		IView firstExclusive = null;
		foreach (IView view in m_views) {
			switch (view._viewData.m_viewType) {
				case (n_viewType.open_on_open): {
						view.Open();
						break;
					}
				case (n_viewType.close_on_open): {
						view.Close();
						break;
					}
				case (n_viewType.exclusive): {
						if (firstExclusive == null) {
							firstExclusive = view;
						}
						break;
					}
			}
		}

		if (firstExclusive != null) {
			foreach (IView view in m_views) {
				view.Close();
			}
			firstExclusive.Open();
		}
		Subscribe();
	}

	public virtual void OnOrientationChanged(MediaQueryTrigger trigger) {

	}

	// Private Functions
	private void EnableViews() {
		for (int a = 0; a < transform.childCount; a++) {
			transform.GetChild(a).gameObject.SetActive(true);
		}
	}

	public virtual void Close() {
		foreach (IView view in m_views) {
			view.Close();
		}
		Unsubscribe();

		gameObject.SetActive(false);
	}


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
		if (m_toView == null) {
			m_button.interactable = false;
		}
		else {
			m_button.interactable = true;
			UIUtils.SubscribeButton(m_button, Segue);
		}
	}

	public void Segue() {
		m_window.Segue(m_toView);
	}
}