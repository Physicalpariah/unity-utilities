//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class UI_Cell : Recyclable {
	// Properties
	protected UI_ViewController m_controller;

	// Initalisation Functions

	// Unity Callbacks
	public override void InitRecycleable(object[] data) {
		base.InitRecycleable(data);

		m_controller = (UI_ViewController)m_manager;

		if (m_controller == null) {
			LogUtils.LogError("Nope, for some reason we made a cell without a ViewController: " + m_manager);
		}
	}
	// Public Functions

	// Private Functions

}