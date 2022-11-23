//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using UnityEngine;

//------------------------------------------------------------------------------
[Serializable]
public struct stringProp {

	[SerializeField] private string _serialisedValue;

	public event StringEvent e_propChanged;

	public string value {
		get { return _serialisedValue; }
		set {
			_serialisedValue = value;
			if (e_propChanged != null) {
				e_propChanged(_serialisedValue);
			}
		}
	}
}

//------------------------------------------------------------------------------
[Serializable]
public struct intProp {

	[SerializeField] private int _serialisedValue;

	public event IntEvent e_propChanged;

	public int value {
		get { return _serialisedValue; }
		set {
			_serialisedValue = value;
			if (e_propChanged != null) {
				e_propChanged(_serialisedValue);
			}
		}
	}
}

//------------------------------------------------------------------------------
[Serializable]
public struct floatProp {

	[SerializeField] private float _serialisedValue;

	public event FloatEvent e_propChanged;

	public float value {
		get { return _serialisedValue; }
		set {
			_serialisedValue = value;
			if (e_propChanged != null) {
				e_propChanged(_serialisedValue);
			}
		}
	}
}

//------------------------------------------------------------------------------
[Serializable]
public struct boolProp {

	[SerializeField] private bool _serialisedValue;

	public event BoolEvent e_propChanged;

	public bool value {
		get { return _serialisedValue; }
		set {
			_serialisedValue = value;
			if (e_propChanged != null) {
				e_propChanged(_serialisedValue);
			}
		}
	}
}