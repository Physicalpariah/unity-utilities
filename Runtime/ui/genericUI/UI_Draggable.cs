//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	// Properties

	[SerializeField] protected Graphic m_raycastTarget;
	protected bool m_active;
	protected Vector3 m_move;
	protected Vector3 m_initialpos;
	protected Vector3 m_distance;
	[SerializeField] protected float m_speed = 0.2f;
	public bool m_returnsToZeroOnDragEnd = true;
	public bool m_canDrag;

	public CoreEvent e_dragStarted;
	public ObjectEvent e_dragEnded;
	public CoreEvent e_onDrag;

	// Initalisation Functions
	public void Enable() {
		m_canDrag = true;
	}

	public void Disable() {
		m_canDrag = false;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		if (!m_canDrag) {
			return;
		}

		m_raycastTarget.raycastTarget = false;

		m_initialpos = transform.position;
		m_move = Vector3.zero;
		m_active = true;
		if (e_dragStarted != null) {
			e_dragStarted();
		}
	}


	public void OnDrag(PointerEventData eventData) {
		if (!m_canDrag) {
			return;
		}

		m_distance = Input.mousePosition - m_initialpos;
		transform.position = m_initialpos + m_distance;
		Vector3 move1 = m_distance.normalized;
		m_move.x = move1.x * m_speed;
		m_move.z = move1.y * m_speed;
		if (e_onDrag != null) {
			e_onDrag();
		}
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (!m_canDrag) {
			return;
		}
		m_raycastTarget.raycastTarget = true;

		if (e_dragEnded != null) {
			if (eventData != null) {
				e_dragEnded(eventData.hovered);
			}
			else {
				e_dragEnded(null);
			}
		}
		m_active = false;
		if (m_returnsToZeroOnDragEnd) {
			ReturnToZero();
		}
	}


	public void ReturnToZero() {
		if (!m_canDrag) {
			return;
		}
		m_move = Vector3.zero;
		transform.position = m_initialpos;
	}
	// Unity Callbacks

	// Public Functions

	// Private Functions

}