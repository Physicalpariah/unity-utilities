//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Tween : MonoBehaviour {

	// Properties
	[Header("Options")]
	[SerializeField] private bool m_position = true;
	[SerializeField] private bool m_rotation = true;
	[SerializeField] private bool m_scale = true;
	[SerializeField] private bool m_alpha = false;

	[Header("Values")]
	public TweenValue m_start;
	public TweenValue m_end;
	[SerializeField] private AnimationCurve m_curve;
	public float m_timeSeconds;
	[SerializeField] private float m_timeDelaySeconds;


	[Header("Settings")]
	[SerializeField] private bool m_deactivatesOnFinish = false;
	[SerializeField] private bool m_autoStart = false;
	[SerializeField] private bool m_prefillValues = false;
	[SerializeField] private bool m_postFillsValues = true;
	[SerializeField] private bool m_repeats = false;
	[SerializeField] private int m_repeatCount = -1;

	private int m_currentRepeat = 0;
	private CanvasGroup m_canvasGroup;
	private Coroutine c_tweenRoutine;

	public UnityEvent m_completionAction;


	// Initalisation Functions

	private void OnEnable() {
		if (m_alpha) {
			if (m_canvasGroup == null) {
				m_canvasGroup = GetComponent<CanvasGroup>();
			}
		}
		if (m_autoStart) {
			StartTween();
		}

		if (m_prefillValues) {
			SetTransfrom(0.0f);
		}
	}

	[ContextMenu("Start Tween")]
	public void StartTween() {
		if (c_tweenRoutine != null) {
			StopCoroutine(c_tweenRoutine);
		}
		m_currentRepeat = 0;
		c_tweenRoutine = StartCoroutine(DoTween());
	}

	public void StopTween() {
		if (c_tweenRoutine != null) {
			StopCoroutine(c_tweenRoutine);
		}
	}

	[ContextMenu("Copy to start")]
	private void CopyTransformToStartTween() {
		m_start = CopyTransform();
	}

	[ContextMenu("Copy to end")]
	private void CopyTransformToEndTween() {
		m_end = CopyTransform();
	}

	private TweenValue CopyTransform() {
		TweenValue value = new TweenValue();
		value.pos = transform.localPosition;
		value.rot = transform.localRotation.eulerAngles;
		value.scale = transform.localScale;
		return value;
	}
	[ContextMenu("Set/Start")]
	private void SetToStart() {
		SetTransfrom(0);
	}
	[ContextMenu("Set/End")]
	private void SetToEnd() {
		SetTransfrom(1);
	}

	private void SetTransfrom(float lerp = 0) {
		if (m_position) {
			transform.localPosition = Vector3.Lerp(m_start.pos, m_end.pos, lerp);
		}
		if (m_rotation) {
			transform.localRotation = Quaternion.Euler(Vector3.Lerp(m_start.rot, m_end.rot, lerp));
		}
		if (m_scale) {
			transform.localScale = Vector3.Lerp(m_start.scale, m_end.scale, lerp);
		}
		if (m_alpha) {
			if (m_canvasGroup != null) {
				m_canvasGroup.alpha = Mathf.Lerp(m_start.alpha, m_end.alpha, lerp);
			}
		}
	}


	// Public Functions

	private IEnumerator DoTween() {
		yield return null;
		WaitForEndOfFrame frame = new WaitForEndOfFrame();
		float delay = m_timeDelaySeconds * GetFrameRate();
		for (float a = 0; a < delay; a++) {
			yield return frame;
		}

		float time = m_timeSeconds * GetFrameRate();
		for (float a = 1; a < time; a++) {
			float curveLerp = m_curve.Evaluate(a / time);
			SetTransfrom(curveLerp);
			yield return frame;
		}


		if (m_postFillsValues) {
			SetTransfrom(1);
		}

		if (m_repeats) {
			if (m_repeatCount > 0) {
				m_currentRepeat++;
				if (m_currentRepeat < m_repeatCount) {
					c_tweenRoutine = StartCoroutine(DoTween());
				}
				else if (m_deactivatesOnFinish) {
					gameObject.SetActive(false);
				}
			}
			else {
				c_tweenRoutine = StartCoroutine(DoTween());
			}
		}
		else if (m_deactivatesOnFinish) {
			gameObject.SetActive(false);
		}

		m_completionAction.Invoke();
	}


	private float GetFrameRate() {

		if (Application.targetFrameRate == -1) {
			return 60;
		}
		else {
			return Application.targetFrameRate;
		}
	}

	// Private Functions

}

[Serializable]
public struct TweenValue {
	public Vector3 pos;
	public Vector3 rot;
	public Vector3 scale;
	public float alpha;
}