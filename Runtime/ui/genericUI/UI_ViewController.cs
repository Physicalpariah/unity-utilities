//  Created by Mathew Purchase.
//  Copyright (c) 2018 Mathew Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_ViewController : MonoBehaviour {

	// Properties
	[Header("Details:")]
	[SerializeField] private string m_name;
	[Header("Sub Panels")]
	[SerializeField] protected List<UI_ViewController> m_subPanelsToOpen;
	[SerializeField] protected List<UI_ViewController> m_subpanelsToClose;

	private List<object> m_dependencies;

	[Header("Navigation")]
	[SerializeField] protected GameObject m_firstSelected;

	[Header("Status")]
	public bool m_isActive = false;
	public bool m_autoClose;
	[SerializeField] protected bool m_canGoBack = true;
	public bool m_canReturnTo = true;
	public bool m_darkUI = true;

	//	stuff
	protected Animator m_anim;
	protected UI_ViewController m_previousController;
	protected bool m_pointerOnUIElement = false;


	// Events
	public CoreEvent e_viewClosed;
	public CoreEvent e_viewOpened;

	// static events
	public static Vector2Event e_panning;
	public static CoreEvent e_swipedLeft;
	public static CoreEvent e_swipedRight;
	public static CoreEvent e_swipedUp;
	public static CoreEvent e_swipedDown;
	public static ObjectEvent se_viewOpened;



	// Init
	protected virtual void OnEnable() {
		// m_isActive = enabled;
		if (m_anim == null) {
			m_anim = GetComponent<Animator>();
		}
	}

	protected virtual void OnDestroy() {
		UnsubscribeEvents();
	}

	protected virtual void OnDisable() {
		UnsubscribeEvents();
	}

	public void InjectDependencies(List<object> dependencies) {
		m_dependencies = dependencies;
	}

	// Core Functions
	public void ToggleView(bool sure) {
		if (m_isActive) {
			Close();
		}
		else {
			Open();
		}
	}


	public string GetName() {
		if (string.IsNullOrWhiteSpace(m_name)) {
			return gameObject.name;
		}
		else {
			return m_name;
		}
	}


	public virtual void Close() {
		if (!m_isActive) {
			return;
		}

		m_isActive = false;
		if (m_anim != null) {
			m_anim.SetTrigger("Close");
		}
		else {
			gameObject.SetActive(false);
		}
		UnsubscribeEvents();
		CloseSubPanels();
		FireCloseEvent();
	}

	protected virtual void UnsubscribeEvents() {

	}

	public void FireCloseEvent() {
		if (e_viewClosed != null) {
			e_viewClosed();
		}
	}


	public virtual void CloseSubPanels() {
		if (m_subPanelsToOpen.Count > 0) {
			for (int a = 0; a < m_subPanelsToOpen.Count; a++) {
				m_subPanelsToOpen[a].Close();
			}
		}
	}


	[ContextMenu("Close")]
	public virtual void ForceClose() {
		ForceClose(false);
	}


	public virtual void ForceClose(bool animates) {
		m_isActive = false;
		if (animates) {
			if (m_anim != null) {
				m_anim.SetTrigger("Close");
			}
			else {
				gameObject.SetActive(false);
			}
		}
		else {
			gameObject.SetActive(false);
		}

		FireCloseEvent();
	}


	[ContextMenu("Open")]
	public virtual void Open() {
		if (m_isActive) {
			return;
		}

		if (m_anim == null) {
			m_anim = GetComponent<Animator>();
		}

		m_isActive = true;

		AutoGetFirstSelected();
		SetFirstSelected();

		if (m_anim != null) {
			gameObject.SetActive(true);
			m_anim.SetTrigger("Open");
		}
		else {

			gameObject.SetActive(true);
		}

		OpenPanels();
		ClosePanels();

		string screenName = m_name;
		if (string.IsNullOrWhiteSpace(screenName)) {
			screenName = gameObject.name;
		}

		UI_ViewManager.Instance.RegisterAsCurrentController(this);

		if (e_viewOpened != null) {
			e_viewOpened();
		}
		if (se_viewOpened != null) {
			se_viewOpened(this);
		}
	}

	public void SetFirstSelected(object sender, EventArgs ea) {
		SetFirstSelected();
	}

	protected void SetFirstSelected() {

		if (UI_ViewManager.Instance == null) {
			return;
		}

		if (m_firstSelected != null) {
			UI_ViewManager.Instance.m_eventSystem.SetSelectedGameObject(m_firstSelected);
		}
	}

	public void SetSelected(GameObject selected) {
		UI_ViewManager.Instance.m_eventSystem.SetSelectedGameObject(selected);
	}

	protected void AutoGetFirstSelected() {
		if (m_firstSelected != null) {
			return;
		}

		// TODO: make this recursive. Check for gameobject active in heirarchy
		foreach (Transform child in transform) {
			Selectable select = child.GetComponent<Selectable>();
			if (select != null) {
				m_firstSelected = select.gameObject;
				return;
			}
		}
	}

	protected virtual void Update() {
		SetSelectedOnNull();
		AndroidBackButtonHandler();
		// DebugSwipes();

		if (Input.GetMouseButtonDown(0)) {
			CheckForUI();
		}
		if (Input.GetMouseButtonUp(0)) {
			m_pointerOnUIElement = false;
		}
	}


	protected virtual void CheckForUI() {
		if (UIUtils.CheckForUI()) {
			m_pointerOnUIElement = true;
		}
		// else {
		// 	m_pointerOnUIElement = false;
		// }
	}


	private void SetSelectedOnNull() {

		if (UI_ViewManager.Instance == null) {
			return;
		}

		if (UI_ViewManager.Instance.m_eventSystem.currentSelectedGameObject == null) {
			SetFirstSelected();
			return;
		}

		if (!UI_ViewManager.Instance.m_eventSystem.currentSelectedGameObject.activeInHierarchy) {
			SetFirstSelected();
		}
	}


	protected virtual void AndroidBackButtonHandler() {
		if (!m_isActive) { return; }
		if (m_previousController == null) { return; }
		if (m_previousController == this) { return; }
		if (!m_previousController.m_canReturnTo) { return; }
		if (!m_canGoBack) { return; }

		if (Input.GetButtonDown("Cancel")) {
			Back();
		}
	}


	public virtual void Back() {
		if (m_previousController != null && m_previousController != this) {
			Segue(m_previousController);
		}
	}


	private void ClosePanels() {
		if (m_subpanelsToClose.Count > 0) {
			for (int a = 0; a < m_subpanelsToClose.Count; a++) {
				if (m_subpanelsToClose[a] != null)
					m_subpanelsToClose[a].ForceClose(false);
			}
		}
	}


	private void OpenPanels() {
		if (m_subPanelsToOpen.Count > 0) {
			for (int a = 0; a < m_subPanelsToOpen.Count; a++) {
				if (m_subPanelsToOpen[a] != null)
					m_subPanelsToOpen[a].Open();
			}
		}
	}


	public virtual void Segue<T>() where T : UI_ViewController {
		UI_ViewController controller = UI_ViewManager.Instance.GetControllerOfType<T>();
		Segue(controller);
	}

	public virtual void Segue(UI_ViewController to) {
		if (m_isActive) {
			Close();
			to.SetPrevious(this);
			to.Open();
		}
		else {
			LogUtils.LogError("can't segue, " + gameObject + " is not active");
		}
	}


	public void SetPrevious(UI_ViewController ctrl) {
		m_previousController = ctrl;
	}


	public virtual void LoadData(object data) {

	}


}
