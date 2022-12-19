//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;



public class DragHandler : MonoBehaviour {
	// Properties

	private static DragHandler m_instance;
	public static DragHandler Instance {
		get {
			return m_instance;
		}
	}

	public static void SetInstance(DragHandler instance) {
		m_instance = instance;
	}

	public static void ClearInstance() {
		m_instance = null;
	}

	private Draggable m_target;
	public float m_scrollRotateSensitivity = 1.0f;
	public bool m_mouseStateDown { get; private set; }
	public static bool m_dragging { get; private set; }
	private bool m_hasBeenTapped = false;
	public static GameObject s_dragMarker { get; private set; }
	private float m_tapTimer;

	public static Vector3 m_screenSpace;
	public static Vector3 m_offset;

	public LayerMask m_layerMask;



	protected GKRotationRecognizer m_rotationRecogniser;
	protected GKLongPressRecognizer m_longPressRecogniser;


	public Action e_letGo;
	public Action e_pickedUp;
	public Action e_tapped;
	public Action e_dragging;
	// Initalisation Functions
	private void OnEnable() {
		SetInstance(this);
		if (s_dragMarker == null) {
			s_dragMarker = GameObject.Find("draggable-placement-marker");
			if (s_dragMarker == null) {
				s_dragMarker = new GameObject();
				s_dragMarker.name = "draggable-placement-marker";
			}

		}

		m_rotationRecogniser = new GKRotationRecognizer();
		m_rotationRecogniser.gestureRecognizedEvent += HandlePinch;
		GestureKit.addGestureRecognizer(m_rotationRecogniser);

		m_longPressRecogniser = new GKLongPressRecognizer();
		m_longPressRecogniser.gestureRecognizedEvent += HandleLongPress;
		GestureKit.addGestureRecognizer(m_longPressRecogniser);

	}


	private void HandleLongPress(GKLongPressRecognizer press) {
		LogUtils.Log("Resetting?");
		if (m_target == null) { return; }

		m_target.FlagReset();
	}
	// Unity Callbacks
	private void HandlePinch(GKRotationRecognizer pinch) {

		if (m_target == null) { return; }

		m_target.Rotate(pinch.deltaRotation);
	}



	// Public Functions
	private void LateUpdate() {

		if (UIUtils.CheckForUI()) {
			return;
		}

		// if (Input.GetMouseButtonDown(1)) {
		// 	Reset();
		// 	return;
		// }

		if (Input.GetMouseButtonDown(0)) {
			Acquire();
		}
		if (Input.GetMouseButtonUp(0)) {
			LetGo();
		}
		if (m_mouseStateDown) {
			Drag();
			m_target.Rotate(Input.mouseScrollDelta.magnitude * m_scrollRotateSensitivity);
		}

		if (transform.position.y < -1) {
			Reset();
		}
	}

	private void Acquire() {
		RaycastHit hitInfo;
		GameObject target = ObjectUtils.GetClickedObject(out hitInfo, LayerMask.NameToLayer("interact"));

		if (hitInfo.collider == null) { return; }

		Draggable drag = hitInfo.collider.gameObject.GetComponentInParent<Draggable>();
		if (drag == null) { return; }

		m_target = drag;

		if (m_target == null) { return; }
		if (m_target.VerifyCanPickup() == false) { m_target = null; return; }


		m_mouseStateDown = true;
		m_offset = m_target.transform.position - GetMouseWorldPoint();


		s_dragMarker.gameObject.SetActive(true);

		m_target.PickUp();
		if (e_pickedUp != null) {
			e_pickedUp();
		}
		// m_yOffset = m_target.transform.position.y;
		m_hasBeenTapped = false;

	}

	public Vector3 GetMouseWorldPoint() {
		m_screenSpace = Camera.main.WorldToScreenPoint(m_target.transform.position);

		//Get the world-space ray from mouse position
		Ray mouseRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenSpace.z));
		//Perform the raycast using the inverted layermask (ignores the selected layers

		if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, m_layerMask)) {
			//Just set this transform to look at the hit point
			s_dragMarker.transform.position = hit.point;
			return hit.point;
		}
		return Vector3.zero;
	}

	private void Drag() {
		if (m_target == null) { return; }
		// m_body.velocity = Vector3.zero;
		m_tapTimer += Time.deltaTime;
		Vector3 raycastStartPoint = transform.position;
		raycastStartPoint.y -= 0.05f;
		Debug.DrawRay(raycastStartPoint, -transform.up * 1000, Color.red, 0.5f);
		m_target.MoveItem();
		m_dragging = true;
		if (e_dragging != null) {
			e_dragging();
		}
	}

	private void Reset() {
		if (m_target == null) { return; }


		m_dragging = false;
		s_dragMarker.gameObject.SetActive(false);
		m_mouseStateDown = false;

		m_target.Reset();

		m_target.LetGo();
		if (e_letGo != null) {
			e_letGo();
		}

		m_target = null;
	}

	private void LetGo() {
		if (m_target == null) { return; }

		m_dragging = false;
		s_dragMarker.gameObject.SetActive(false);
		m_target.MoveItem(0);
		m_mouseStateDown = false;
		m_target.LetGo();
		if (e_letGo != null) {
			e_letGo();
		}

		if (m_target == gameObject) {
			if (!m_hasBeenTapped) {
				if (m_tapTimer < 0.1f) {
					LogUtils.Log("Tapped");
					if (e_tapped != null) {
						e_tapped();
					}
					m_hasBeenTapped = true;
				}
			}
			m_tapTimer = 0;
		}
		m_target = null;
	}
	// Private Functions

}