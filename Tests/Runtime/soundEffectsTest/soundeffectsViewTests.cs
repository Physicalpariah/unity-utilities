using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class soundeffectsViewTests {
	// Properties

	// private __TEST_COMPONENT_NAME__ m_ctrl;
	// private Vector3 m_testSpot;

	// Tests
	[UnitySetUp]
	public IEnumerator Setup() {

		// Load a testing scene
		// TODO: Define testing scene or always have it as scene 0?
		AsyncOperation op = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);


		// Wait for scene to load
		int counter = 200000;
		while (!op.isDone) {
			yield return null;
			counter--;
			if (counter < 0) {
				break;
			}
		}

		// TODO: Fill out initial setup
		// m_testSpot = new Vector3(10, 0, 10);
		// GameObject obj = new GameObject();
		// m_ctrl = obj.AddComponent<UnitController>();
		// m_ctrl.InitialiseComponents();
	}


	[TearDown]
	public void TearDown() {
		// TODO: Destroy anything that's been created.
		// GameObject.DestroyImmediate(m_ctrl.gameObject);
	}


	// Single frame function test.
	[Test]
	public void WillSetup() {
		Assert.Fail();
	}


	// multi frame function test.
	[UnityTest]
	public IEnumerator PlayModeTest() {
		// Test before unit is initialised


		// m_ctrl.m_mover.MoveToSpot(m_testSpot);
		// if (m_ctrl.m_mover.m_agent.hasPath) {
		// 	Assert.Fail();
		// }
		// else {
		// 	Assert.Pass();
		// }

		// InitialiseController();

		// m_ctrl.m_mover.MoveToSpot(m_testSpot);
		yield return null;

		// // we assume that Unity's Navigation works.
		// if (m_ctrl.m_mover.m_agent.hasPath) {
		// 	Assert.Pass();
		// }
		// else {
		Assert.Fail();
		// }
	}

	// Testing events fire properly.
	// [UnityTest]
	// public IEnumerator WillRaiseUnitBeganMovingEvent() {
	// 	bool eventRaised = false;

	// 	m_ctrl.m_mover.e_unitBeganMoving += () => { eventRaised = true; };
	// 	InitialiseController();
	// 	m_ctrl.m_mover.MoveToSpot(m_testSpot);
	// 	yield return null;


	// 	Assert.IsTrue(eventRaised);
	// }
}
