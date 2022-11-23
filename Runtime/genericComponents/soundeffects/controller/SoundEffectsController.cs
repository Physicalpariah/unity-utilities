//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundEffectsController : Recycler<AudioClip> {
	// Properties

	// Initalisation Functions

	// Unity Callbacks

	// Public Functions
	public void PlaySound(AudioClip clip) {
		Append(clip);
	}

	// Private Functions

}