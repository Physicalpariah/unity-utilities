//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using UnityEngine;

// attached to canvas icon
public class UI_ScreenSpaceIcon : Recyclable {

	// Properties
	private Camera m_mainCamera;
	private Canvas m_mainCanvas;
	protected ScreenSpaceObjectTracker m_tracker;
	protected bool m_isOnScreen;

	public CoreEvent e_onScreen;
	public CoreEvent e_offScreen;


	// Initalisation Functions

	public override void InitRecycleable(object[] data) {
		base.InitRecycleable(data);
		ScreenSpaceObjectTracker tracker = m_data as ScreenSpaceObjectTracker;

		m_tracker = tracker;
		m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		m_mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
	}


	// Public Functions

	private void OnDestroy() {
		CheckEventsForUnsubscription();
	}

	private void OnDisable() {
		CheckEventsForUnsubscription();
	}


	private void CheckEventsForUnsubscription() {

		e_onScreen = null;
		e_offScreen = null;

		AnchoriteEvents.CheckEvent(ref e_onScreen);
		AnchoriteEvents.CheckEvent(ref e_offScreen);
	}



	protected virtual void SetOffscreen(Vector3 pos) {
		if (m_isOnScreen) {
			m_isOnScreen = false;
			if (e_offScreen != null) {
				e_offScreen();
			}
		}
	}


	protected virtual void SetOnscreen() {
		if (!m_isOnScreen) {
			m_isOnScreen = true;
			if (e_onScreen != null) {
				e_onScreen();
			}
		}
	}


	// Private Functions
	protected virtual void LateUpdate() {
		UpdateTargetIconPosition();
	}

	private void UpdateTargetIconPosition() {
		if (m_tracker == null) {
			Debug.LogWarning("Nah no tracker assigned");
			Destroy(gameObject);
			return;
		}

		Vector3 newPos = m_tracker.transform.position;
		newPos = m_mainCamera.WorldToViewportPoint(newPos);
		if (newPos.z < 0) {
			newPos.x = 1f - newPos.x;
			newPos.y = 1f - newPos.y;
			newPos.z = 0;
			newPos = Vector3Maxamize(newPos);
		}
		if (newPos.x > 1 || newPos.y > 1 || newPos.x < 0 || newPos.y < 0) {
			SetOffscreen(newPos);
		}
		else {
			SetOnscreen();
		}

		newPos = m_mainCamera.ViewportToScreenPoint(newPos);
		newPos.x = Mathf.Clamp(newPos.x, ScreenSpaceIconHandler.Instance.m_xEdgeBuffer, Screen.width - ScreenSpaceIconHandler.Instance.m_xEdgeBuffer);
		newPos.y = Mathf.Clamp(newPos.y, ScreenSpaceIconHandler.Instance.m_bottomEdgeBuffer, Screen.height - ScreenSpaceIconHandler.Instance.m_topEdgeBuffer);
		newPos.z = 0;
		transform.position = newPos;
	}


	public Vector3 Vector3Maxamize(Vector3 vector) {
		Vector3 returnVector = vector;
		float max = 0;
		max = vector.x > max ? vector.x : max;
		max = vector.y > max ? vector.y : max;
		max = vector.z > max ? vector.z : max;
		returnVector /= max;
		return returnVector;
	}
}
