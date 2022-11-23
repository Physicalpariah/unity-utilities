//  Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;


public class SoundEffect : Recyclable {
	// Properties
	[SerializeField] private AudioSource m_source;

	// Initalisation Functions
	public override void InitRecycleable(object[] data) {
		base.InitRecycleable( data);

		AudioClip clip = (AudioClip)m_data;
		if (clip == null) {
			return;
		}

		m_source.clip = clip;
		m_source.Play();
	}
	// Unity Callbacks

	// Public Functions

	// Private Functions

}