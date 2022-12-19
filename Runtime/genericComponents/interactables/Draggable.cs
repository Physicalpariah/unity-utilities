//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class Draggable : MonoBehaviour {

	public bool m_pickedUp = false;
	public bool m_rotating = false;
	private bool m_canDrag = true;
	public bool m_willReset = false;
	public bool m_propelsDownward;
	[SerializeField] private float m_rotation;
	[SerializeField] private bool m_forcesPositionOnLetGo = false;
	[SerializeField] private bool m_dropsWithPhysics = true;

	// Initalisation Functions

	// Unity Callbacks
	public virtual void PickUp() {
		m_pickedUp = true;
	}

	public virtual bool VerifyCanPickup() {
		return true;
	}


	public virtual void MoveItem(float height = 0.3f) {
		if (!m_canDrag) { return; }
		if (m_rotating) { return; }

		Vector3 curPosition = DragHandler.Instance.GetMouseWorldPoint() + DragHandler.m_offset;
		curPosition.y = height;
		SetPosition(curPosition);


		curPosition.y = GetHeight();
	}

	protected virtual float GetHeight() {
		return 0;
	}

	protected virtual void SetPosition(Vector3 curPosition) {
		transform.position = curPosition;
	}

	private void OnDisable() {
		LetGo();
	}

	// Public Functions

	public void Enable() {
		// m_body.isKinematic = false;
		m_canDrag = true;
	}

	public void Disable() {
		m_canDrag = false;
	}

	public virtual void Reset() {
		m_rotating = false;
		m_pickedUp = false;
		m_willReset = false;
	}

	public virtual void LetGo() {

		if (m_willReset) {
			m_willReset = false;
			Reset();
			return;
		}

		if (m_rotating) {
			ApplyRotation();
			// return;
		}
		if (m_forcesPositionOnLetGo) {
			Vector3 pos = transform.position;
			pos.y = 0.01f;
			transform.position = pos;
		}

		m_pickedUp = false;
	}


	public virtual void Rotate(float angle) {
		if (angle == 0) { return; }
		m_rotating = true;

		m_rotation += angle;
		transform.rotation = Quaternion.Euler(0, m_rotation, 0);
	}

	public virtual void ApplyRotation() {
		m_rotating = false;
		transform.rotation = Quaternion.Euler(0, m_rotation, 0);
	}


	public void FlagReset() {
		m_willReset = true;
	}



	// Private Functions

}