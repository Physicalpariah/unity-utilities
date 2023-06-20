//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void CoreEvent();
public delegate void ObjectEvent(object obj);
public delegate void IntEvent(int obj);
public delegate void FloatEvent(float obj);
public delegate void BoolEvent(bool obj);
public delegate void StringEvent(string obj);
public delegate void Vector2Event(Vector2 obj);
public delegate void Vector3Event(Vector3 obj);


public static class AnchoriteEvents {
	public static void CheckEvent(ref Delegate e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	private static void LogEvent(Delegate e) {

		if (e == null) {
			return;
		}

		LogUtils.LogError($"event {e} not unsubscribed on destroy");
		foreach (Delegate subscriber in e.GetInvocationList()) {


		}
	}

	public static void CheckEvent(ref EventHandler e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref CoreEvent e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref ObjectEvent e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref IntEvent e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref FloatEvent e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref StringEvent e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref BoolEvent e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref Vector2Event e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

	public static void CheckEvent(ref Vector3Event e) {
		if (e != null) {
			e = null;
			LogEvent(e);
		}
	}

}


