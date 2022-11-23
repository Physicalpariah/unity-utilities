//  Created by Matt Purchase.
//  Copyright (c) 2018 Matt Purchase. All rights reserved.
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class UI_ViewManager : MonoBehaviour {

	// Properties
	public EventSystem m_eventSystem;
	public List<UI_ViewController> m_viewControllers;
	public UI_ViewController m_firstOpened;
	public UI_ViewController m_currentController;
	public CanvasScaler m_mainCanvas;
	[SerializeField] private bool m_closesAllOnOpen;
	[SerializeField] private bool m_opensOnEnable = true;
	[SerializeField] private bool m_setsInstanceOnEnable = false;

	private static UI_ViewManager m_instance;
	public static UI_ViewManager Instance {
		get {
			return m_instance;
		}
	}

	public static void SetInstance(UI_ViewManager manager) {
		m_instance = manager;
	}

	public static void ClearInstance() {
		m_instance = null;
	}

	// Initalisation Functions
	private void OnEnable() {
		if (m_setsInstanceOnEnable) {
			SetInstance(this);
		}
		if (m_opensOnEnable) {
			Initialise();
		}

	}

	public void Initialise() {
		CollectEventSystem();

		if (m_closesAllOnOpen) {
			foreach (UI_ViewController ctrl in m_viewControllers) {
				if (ctrl != m_firstOpened) {
					ctrl.ForceClose();
				}
			}
		}

		if (m_firstOpened != null) {
			m_firstOpened.Open();
		}
	}

	private void CollectEventSystem() {
		if (m_eventSystem == null) {
			m_eventSystem = GameObject.FindObjectOfType<EventSystem>();
		}
	}

	// Public Functions
	public void RegisterAsCurrentController(UI_ViewController view) {
		m_currentController = view;
	}

	public T GetControllerOfType<T>() where T : UI_ViewController {
		for (int a = 0; a < m_viewControllers.Count; a++) {
			if (m_viewControllers[a] is T) {
				return m_viewControllers[a] as T;
			}
		}
		return null;
	}


	// Private Function
}

