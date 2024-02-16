//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIWindow : MonoBehaviour {

	// Purpose:
	// The window serves as an invisible container for the rest of your UI, acts as a top-level container for your views, and routes events to them

	// Dependencies

	// Properties
	[SerializeField] protected UIViewController m_currentView;
	[SerializeField] private List<UIViewController> m_views;
	[SerializeField] private List<UIViewController> m_history;
	[SerializeField] private EventSystem m_eventSystem;
	// Initalisation Functions

	// Unity Callbacks
	public void Awake() {
		m_views = new();

		// hack to turn on all the children so they can register.
		EnableViewControllers();
		StartCoroutine(InitRoutine());
	}

	private void EnableViewControllers() {
		for (int a = 0; a < transform.childCount; a++) {
			Transform trans = transform.GetChild(a);
			if (trans.gameObject != null) {
				trans.gameObject.SetActive(true);
			}
		}
	}

	private IEnumerator InitRoutine() {
		yield return new WaitForEndOfFrame();
		Init();
	}

	private void Init() {
		CloseAllViews();
		OpenFirstView();
	}

	protected virtual void OpenFirstView() {
		m_currentView.Open();
	}

	// Public Functions
	public UIViewController GetCurrentViewController() {
		return m_currentView;
	}

	public void SelectInteractable(GameObject selected) {
		m_eventSystem.SetSelectedGameObject(selected);
	}


	public void RegisterView(UIViewController view) {
		m_views.Add(view);
	}

	public void Segue<T>() where T : UIViewController {
		UIViewController view = GetControllerOfType<T>();
		Segue(view);
	}

	public void Segue(UIViewController view) {
		m_currentView.Close();
		m_history.Add(m_currentView);

		m_currentView = view;
		m_currentView.Open();
	}

	public void Back() {
		if (m_history.Count <= 0) { return; }

		m_currentView.Close();
		UIViewController current = m_history[m_history.Count - 1];
		m_history.RemoveAt(m_history.Count - 1);
		m_currentView = current;
		m_currentView.Open();
	}

	public void CloseAllViews() {
		for (int a = 0; a < m_views.Count; a++) {
			m_views[a].Close();
		}
	}

	public T GetControllerOfType<T>() where T : UIViewController {
		for (int a = 0; a < m_views.Count; a++) {
			if (m_views[a] is T) {
				return m_views[a] as T;
			}
		}
		return null;
	}
	// Private Functions

}
