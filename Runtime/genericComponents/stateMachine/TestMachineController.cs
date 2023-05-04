//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;


public class TestMachineController : MonoBehaviour {
	// Dependencies

	// Properties
	public StateMachineObject m_machine;

	public StateObject m_initialState;
	public string m_stateToChangeTo;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions
	[ContextMenu("Initialise")]
	public void Initialse() {
		m_machine.Initialise(m_initialState);
	}

	[ContextMenu("Shutdown")]
	public void ShutDown() {
		m_machine.ShutDown();
	}

	[ContextMenu("Next state")]
	public void NextState() {
		m_machine.ChangeState(m_stateToChangeTo);
	}
	// Private Functions

}