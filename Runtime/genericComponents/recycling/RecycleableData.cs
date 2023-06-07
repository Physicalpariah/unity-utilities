//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;


[Serializable]
public class RecycleableData {
	// Properties
	public int m_uniqueID;
	private int _id = -1;
	public int m_recycleID {
		set {
			if (_id == -1) {
				_id = value;
			}
		}
		get { return _id; }
	}

	public bool m_caresAboutInitBalance = true;
	public int m_initialisations = 0;
	public int m_enables = 0;

	// Initalisation Functions

	// Unity Callbacks

	// Public Functions

	// Private Functions

}
