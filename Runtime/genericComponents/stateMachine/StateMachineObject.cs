//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "utils-shell-project/StateMachineObject")]
public class StateMachineObject : ScriptableObject {
	// Properties
	public StateObject m_currentState;
	[SerializeField] private List<StateObject> m_statesList;
	public Dictionary<string, StateObject> m_states;
	public bool m_initialised = false;

	public void Initialise(StateObject startingState) {
		m_states = new();
		for (int a = 0; a < m_statesList.Count; a++) {
			m_states.Add(m_statesList[a].m_name, m_statesList[a]);
		}
		m_currentState = startingState;
		m_initialised = true;
	}

	public void ShutDown() {
		m_currentState = null;
		m_states = null;
		m_initialised = false;
	}

	public void ChangeState(StateObject state) {
		m_currentState.OnStateExit();
		m_currentState = state;
		m_currentState.OnStateEnter();
	}

	public void ChangeState(string stateID) {
		if (m_states.ContainsKey(stateID) == false) { LogUtils.LogError($"No state by ID: {stateID}"); return; }

		StateObject state = m_states[stateID];
		ChangeState(state);
	}
}