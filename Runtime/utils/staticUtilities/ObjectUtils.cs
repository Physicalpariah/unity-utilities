//  Created by Mathew Purchase.
//  Copyright (c) 2016 Mathew Purchase. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectUtils {


	public static string TrimSpawnName(string objectName) {
		string trimmedName = "";
		if (objectName.Contains("(Clone)")) {
			string[] newname = objectName.Split('(');
			trimmedName = newname[0];
		}
		else {
			trimmedName = objectName;
		}
		return trimmedName;
	}

	public static Component InitComponent<T>(GameObject inputObject) where T : Component {
		Component obj = inputObject.GetComponent<T>();
		if (obj == null) {
			obj = inputObject.AddComponent<T>();
		}
		return obj;
	}

	public static GameObject GetClickedObject(out RaycastHit hit) {
		return GetClickedObject(out hit, LayerMask.NameToLayer("Default"));
	}

	public static GameObject GetClickedObject(out RaycastHit hit, LayerMask mask) {
		GameObject target = null;

		Vector3 pos = Input.mousePosition;
		if (Input.touchCount > 0)
		{
			pos = Input.touches[0].position;
		}
		Ray ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, 99999, mask)) {
			target = hit.collider.gameObject;
		}

		return target;
	}

	public static void DespawnItem(GameObject prefab) {
		ObjectRecycler.Instance.DespawnItem(prefab);
	}

	public static GameObject SpawnRaw(GameObject prefab) {
		return GameObject.Instantiate(prefab);
	}

	public static GameObject Spawn(GameObject prefab) {
		GameObject obj = ObjectRecycler.Instance.SpawnItem(prefab);
		return obj;
	}

	public static GameObject Spawn(GameObject prefab, GameObject parent) {
		GameObject obj = ObjectRecycler.Instance.SpawnItem(prefab);
		obj.transform.SetParent(parent.transform);
		return obj;
	}

	public static GameObject SpawnUI(GameObject prefab, GameObject parent) {
		GameObject obj = ObjectRecycler.Instance.SpawnItem(prefab);
		obj.transform.SetParent(parent.transform, false);
		obj.transform.position = Vector3.zero;
		return obj;
	}

	public static GameObject SpawnWithData(GameObject prefab, GameObject parent, object manager, object data) {
		GameObject obj = ObjectRecycler.Instance.SpawnItem(prefab, data, manager, parent);

		return obj;
	}

	public static GameObject SpawnUIWithData(GameObject prefab, GameObject parent, object manager, object data) {
		GameObject obj = ObjectRecycler.Instance.SpawnItem(prefab, data, manager);
		obj.transform.SetParent(parent.transform, false);
		return obj;
	}

	public static void SetPosAndRot(GameObject spawnedItem, Vector3 pos, Quaternion rotation) {
		spawnedItem.transform.position = pos;
		spawnedItem.transform.rotation = rotation;
	}

}
