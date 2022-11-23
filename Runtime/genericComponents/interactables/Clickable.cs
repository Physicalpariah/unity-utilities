//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class Clickable : MonoBehaviour {
	// Properties

	public CoreEvent e_clicked;

	// Initalisation Functions


	// Unity Callbacks
	private void LateUpdate() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit();
			GameObject clicked = ObjectUtils.GetClickedObject(out hit);
			if (clicked == gameObject) {
				if (e_clicked != null) {
					e_clicked();
				}
			}
		}
	}
	// Public Functions

	// Private Functions

}