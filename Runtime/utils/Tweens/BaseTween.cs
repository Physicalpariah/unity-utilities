//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BaseTween : MonoBehaviour {
	// Dependencies

	// Properties


	[SerializeField] protected TweenData m_tweenData;

	private int m_currentRepeat = 0;
	private CanvasGroup m_canvasGroup;
	private Coroutine c_tweenRoutine;

	public UnityEvent m_completionAction;
	// Initalisation Functions
	private void OnEnable() {
		if (m_tweenData.m_autoStart) {
			StartTween();
		}
		if (m_tweenData.m_prefillValues) {
			Apply(0.0f);
		}
	}

	[ContextMenu("Tween/Start Tween")]
	public void StartTween() {
		if (c_tweenRoutine != null) {
			StopCoroutine(c_tweenRoutine);
		}
		m_currentRepeat = 0;
		c_tweenRoutine = StartCoroutine(DoTween());
	}
	// Unity Callbacks

	[ContextMenu("Tween/Set/Start")]
	private void SetToStart() {
		Apply(0);
	}

	[ContextMenu("Tween/Set/End")]
	private void SetToEnd() {
		Apply(1);
	}


	protected virtual void CopyTransformToStartTween() {

	}


	protected virtual void CopyTransformToEndTween() {
		LogUtils.LogPriority("HMMM2");
	}


	// Public Functions
	protected IEnumerator DoTween() {
		yield return null;
		WaitForEndOfFrame frame = new WaitForEndOfFrame();
		float delay = m_tweenData.m_timeDelaySeconds * GetFrameRate();
		for (float a = 0; a < delay; a++) {
			yield return frame;
		}

		float time = m_tweenData.m_timeSeconds * GetFrameRate();
		for (float a = 1; a < time; a++) {
			float curveLerp = m_tweenData.m_curve.Evaluate(a / time);
			Apply(curveLerp);
			yield return frame;
		}


		if (m_tweenData.m_postFillsValues) {
			Apply(1);
		}


		if (m_tweenData.m_bounces) {
			for (float a = 1; a < time; a++) {
				float curveLerp = m_tweenData.m_curve.Evaluate(1 - a / time);
				Apply(curveLerp);
				yield return frame;
			}
		}

		if (m_tweenData.m_repeats) {
			if (m_tweenData.m_repeatCount > 0) {
				m_currentRepeat++;
				if (m_currentRepeat < m_tweenData.m_repeatCount) {
					c_tweenRoutine = StartCoroutine(DoTween());
				}
				else if (m_tweenData.m_deactivatesOnFinish) {
					gameObject.SetActive(false);
				}
			}
			else {
				c_tweenRoutine = StartCoroutine(DoTween());
			}
		}
		else if (m_tweenData.m_deactivatesOnFinish) {
			gameObject.SetActive(false);
		}

		m_completionAction.Invoke();
	}

	// Private Functions
	private float GetFrameRate() {

		if (Application.targetFrameRate == -1) {
			return 60;
		}
		else {
			return Application.targetFrameRate;
		}
	}

	protected virtual void Apply(float lerp = 0) {

	}

}