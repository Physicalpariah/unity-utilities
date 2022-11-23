//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using UnityEngine;


public static class MemoryHandler  {
	// Properties
	public static Action e_gcWillClear;
	// Initalisation Functions

	// Public Functions
	public static void ClearGC() {
		if (e_gcWillClear != null) {
			e_gcWillClear();
		}

		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}
	// Private Functions

}