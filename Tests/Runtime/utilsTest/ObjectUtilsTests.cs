using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectUtilsTests {

	// Properties
	private GameObject m_prefab;


	[SetUp]
	public void Setup() {
		m_prefab = new GameObject();
		m_prefab.name = "prefab";
		m_prefab.AddComponent<Recyclable>();
	}


	[TearDown]
	public void TearDown() {
		ObjectUtils.DespawnItem(m_prefab);
	}


	// Tests
	[UnityTest]
	public IEnumerator WillSpawnAsset() {
		GameObject spawned = ObjectUtils.Spawn(m_prefab, null);
		yield return null;
		LogUtils.Log(spawned);
		if (spawned.name == "prefab(Clone)") {
			Assert.Pass();
		}
		else {
			Assert.Fail("Incorrect prefab");
		}
	}


	[UnityTest]
	public IEnumerator WillSpawnAssetAtPosition() {
		Vector3 spot = new Vector3(5, 0, 5);
		GameObject spawned = ObjectUtils.Spawn(m_prefab, null);
		ObjectUtils.SetPosAndRot(spawned, spot, Quaternion.identity);
		yield return null;
		LogUtils.Log(spawned);
		if (spawned.name == "prefab(Clone)") {
			if (spawned.transform.position == spot) {
				Assert.Pass();
			}
			else {
				Assert.Fail("Incorrect Spot");
			}
		}
		else {
			Assert.Fail("Incorrect prefab");
		}
	}


	[UnityTest]
	public IEnumerator WillSpawnAssetAtPositionAndRotation() {
		Vector3 spot = new Vector3(5, 0, 5);
		Quaternion rot = new Quaternion(1, 0, 0, 0);
		GameObject spawned = ObjectUtils.Spawn(m_prefab, null);
		ObjectUtils.SetPosAndRot(spawned, spot, rot);
		yield return null;
		LogUtils.Log(spawned);
		if (spawned.name == "prefab(Clone)") {
			if (spawned.transform.position == spot) {
				if (spawned.transform.rotation == rot) {
					// spawned.transform.rotation.x = 1 for some reason?!
					// bug in unity?

					// seems to fail on < 1.0f values....
					Assert.Pass();
				}
				else {
					Assert.Fail("Incorrect rotation, rot is: " + spawned.transform.rotation);
				}
			}
			else {
				Assert.Fail("Incorrect Spot, pos is " + spawned.transform.position);
			}
		}
		else {
			Assert.Fail("Incorrect prefab, prefab is: " + spawned);
		}
	}


	[UnityTest]
	public IEnumerator WillSpawnAssetAtParentPosAndRot() {
		Vector3 spot = new Vector3(5, 0, 5);
		Quaternion rot = new Quaternion(0.1f, 0, 0, 0);
		GameObject parent = new GameObject();
		parent.name = "parent";
		parent.transform.position = spot;
		parent.transform.rotation = rot;


		GameObject spawned = ObjectUtils.Spawn(m_prefab, parent);
		ObjectUtils.SetPosAndRot(spawned, spot, rot);
		yield return null;
		LogUtils.Log(spawned);
		if (spawned.name == "prefab(Clone)") {
			if (spawned.transform.position == spot) {
				if (spawned.transform.rotation == parent.transform.rotation) {
					Assert.Pass();
				}
				else {
					Assert.Fail("Incorrect rotation, rot is: " + spawned.transform.rotation);
				}
			}
			else {
				Assert.Fail("Incorrect Spot, pos is " + spawned.transform.position);
			}
		}
		else {
			Assert.Fail("Incorrect prefab, prefab is: " + spawned);
		}
	}



	[UnityTest]
	public IEnumerator WillSpawnAndParent() {

		Vector3 spot = new Vector3(5, 0, 5);
		Quaternion rot = new Quaternion(0.1f, 0, 0, 0);
		GameObject parent = new GameObject();

		GameObject spawned = ObjectUtils.Spawn(m_prefab, parent);


		yield return null;


		if (spawned.transform.parent != parent.transform) {
			Assert.Fail();
		}
		else {
			Assert.Pass();
		}
	}


	[UnityTest]
	public IEnumerator WillSpawnAndParentPositioned() {

		Vector3 spot = new Vector3(5, 0, 5);
		Quaternion rot = new Quaternion(1f, 0, 0, 0);
		GameObject parent = new GameObject();

		GameObject spawned = ObjectUtils.Spawn(m_prefab, parent);
		ObjectUtils.SetPosAndRot(spawned, spot, Quaternion.identity);

		yield return null;


		if (spawned.transform.parent != parent.transform) {
			Assert.Fail("Incorrect Parent");
		}
		else {
			if (spawned.transform.position != spot) {
				Assert.Fail("Incorrect spot");
			}
			Assert.Pass();
		}
	}


	[UnityTest]
	public IEnumerator WillSpawnAndParentPosAndRotaiton() {

		Vector3 spot = new Vector3(5, 0, 5);
		Quaternion rot = new Quaternion(1f, 0, 0, 0);
		GameObject parent = new GameObject();

		GameObject spawned = ObjectUtils.Spawn(m_prefab, parent);
		ObjectUtils.SetPosAndRot(spawned, spot, rot);

		yield return null;


		if (spawned.transform.parent != parent.transform) {
			Assert.Fail();
		}
		else {
			if (spawned.transform.position != spot) {
				Assert.Fail("Incorrect spot");
			}
			if (spawned.transform.rotation != rot) {
				Assert.Fail("Incorrect spot");
			}
			Assert.Pass();
		}
	}



	[UnityTest]
	public IEnumerator WillSpawnAndParentUI() {

		Vector3 spot = new Vector3(5, 0, 5);
		Quaternion rot = new Quaternion(0.1f, 0, 0, 0);
		GameObject parent = new GameObject();

		GameObject spawned = ObjectUtils.SpawnUI(m_prefab, parent);


		yield return null;


		if (spawned.transform.parent != parent.transform) {
			Assert.Fail("Incorrect Parent");
		}
		else {
			if (spawned.transform.localScale != Vector3.one) {
				Assert.Fail("Incorrect rescaling");
			}
			Assert.Pass();
		}
	}



	[UnityTest]
	public IEnumerator WillTrimSpawnName() {
		GameObject spawned = ObjectUtils.Spawn(m_prefab, null);
		yield return null;
		string name = ObjectUtils.TrimSpawnName(spawned.name);
		if (name == m_prefab.name) {
			Assert.Pass();
		}
		Assert.Fail();
	}


	[UnityTest]
	public IEnumerator WillinitComponent() {

		GameObject test = new GameObject();
		BoxCollider collider = ObjectUtils.InitComponent<BoxCollider>(test) as BoxCollider;

		yield return null;

		BoxCollider testCollider = test.GetComponent<BoxCollider>();
		if (testCollider == null) {
			Assert.Fail("blank Init failed");
		}

		BoxCollider testCollider2 = ObjectUtils.InitComponent<BoxCollider>(test) as BoxCollider;
		if (testCollider == null) {
			Assert.Fail("pre-filled Init failed");
		}

		Assert.Pass();
	}
}
