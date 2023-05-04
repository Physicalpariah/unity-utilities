//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "utils-shell-project/StateObject")]
public class StateObject : ScriptableObject {
	public string m_name;
	public List<StateObject> m_connections;

	public virtual void OnStateEnter() {
		LogUtils.LogPriority($"{m_name} has been entered");
	}

	public virtual void OnStateExit() {
		LogUtils.LogPriority($"{m_name} is exiting");
	}
}