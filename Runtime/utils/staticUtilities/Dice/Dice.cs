//  Created by Mathew Purchase.
//  Copyright (c) 2016 Mathew Purchase. All rights reserved.

using System.Collections.Generic;
using UnityEngine;


public static class Dice {

	public static bool IsWithin(float num, float min, float max) {
		if (num >= min && num < max) {
			return true;
		}
		return false;
	}

	public static bool IsBetween(float num, float min, float max) {
		if (num > min && num < max) {
			return true;
		}
		return false;
	}

	public static bool FlipCoin() {
		int x = Roll(100);
		if (x > 50) {
			return true;
		}
		return false;
	}


	public static int Roll(float min, float max) {
		return Mathf.CeilToInt(Random.Range(min, max));
	}

	public static int Roll(int size) {
		int diceRoll = UnityEngine.Random.Range(0, size);
		return diceRoll;
	}

	public static int Roll(float size) {
		float diceRoll = UnityEngine.Random.Range(0, size);
		return Mathf.CeilToInt(diceRoll);
	}


	public static float RollFloat(float min, float max) {
		return Random.Range(min, max);
	}

	public static float RollFloat(int size) {
		int diceRoll = UnityEngine.Random.Range(0, size);
		return diceRoll;
	}

	public static float RollFloat(float size) {
		float diceRoll = UnityEngine.Random.Range(0, size);
		return diceRoll;
	}

}

