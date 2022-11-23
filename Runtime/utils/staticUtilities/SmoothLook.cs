
//  Created by Mathew Purchase.
//  Copyright (c) 2018 Mathew Purchase. All rights reserved.
using UnityEngine;

public static class SmoothLook {

	// Properties


	// Init


	// Core Functions
	public static void DoSmoothPlanarLook(Transform transform, Vector3 target, float rotationSpeed) {
		if (Time.timeScale > 0.3f) {
			Vector3 targetRot = target - transform.position;
			if (targetRot == Vector3.zero) {
				return;
			}

			Quaternion rotation = Quaternion.LookRotation(targetRot);
			rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

			// Debug.DrawLine(transform.position, target, Color.magenta);
			rotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
			transform.rotation = rotation;
		}
	}


	public static void DoSmoothPlanarLookNoRoll(Transform transform, Vector3 target, float rotationSpeed) {

		if (Time.timeScale > 0.3f) {
			Vector3 targetRot = target - transform.position;
			if (targetRot == Vector3.zero) {
				return;
			}

			Quaternion rotation = Quaternion.LookRotation(targetRot, transform.parent.up);

			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}


	public static void DoSmoothFreeLook(Transform transform, Vector3 target, float rotationSpeed) {
		if (Time.timeScale > 0.3f) {

			Vector3 targetRot = target - transform.position;
			if (targetRot == Vector3.zero) {
				transform.rotation = Quaternion.Euler(Vector3.zero);
				return;
			}
			Quaternion rotation = Quaternion.LookRotation(targetRot);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}


	public static void DoSmoothFreeLookLocal(Transform transform, Vector3 target, float rotationSpeed) {
		if (Time.timeScale > 0.3f) {
			Vector3 targetRot = target - transform.position;
			if (targetRot == Vector3.zero) {
				return;
			}
			Quaternion rotation = Quaternion.LookRotation(targetRot);
			transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
		}
	}

}
