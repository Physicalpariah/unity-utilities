//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_ScrollView<T> : Recycler<T> {
	// Properties
	public UI_ViewController m_controller;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions

	public void Initialise(List<T> data, UI_ViewController controller) {
		SetController(controller);
		Initialise(data);
	}

	public void SetController(UI_ViewController ctrl) {
		m_controller = ctrl;
	}

	// Private Functions
	protected override GameObject SpawnCell(object element, Transform holder, GameObject prefab) {
		return ObjectUtils.SpawnUIWithData(prefab, holder.gameObject, m_controller, element);
	}
}