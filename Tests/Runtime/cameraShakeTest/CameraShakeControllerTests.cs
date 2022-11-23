//Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CameraShakeControllerTests {
	// Properties
	private CameraShakeView m_cameraShakeObject;
	private CameraShakeController m_controller;
	private UtilsManager m_utilsManager;


	// public void Setup() {

	// 	UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/!_charge/scenes/test-scenes/scn_testRunner.unity");
	// }

	// Tests
	[SetUp]
	public void SetupTest() {
		LogUtils.LogPriority("Setting up");
		m_controller = new CameraShakeController();

		GameObject obj = new GameObject();
		obj.name = "Camera Shake Mono Holder";
		m_cameraShakeObject = obj.AddComponent<CameraShakeView>();
		m_controller.Initialise();
	}

	[TearDown]
	public void TearDown() {
		m_controller = null;
		GameObject.Destroy(m_cameraShakeObject);
	}

	[UnityTest]
	public IEnumerator WillShake() {
		m_controller.Shake();
		yield return null;

		if (m_cameraShakeObject.transform.localRotation == Quaternion.identity) {
			Assert.Fail();
		}

		Assert.Pass();
	}
}