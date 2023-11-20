//  Created by Matt Purchase.
//  Copyright (c) 2023 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "anchorite/Media Query")]
public class MediaQueryTrigger : ScriptableObject {
	public n_mediaOrientation m_orientation;
	public List<n_deviceType> m_devices;
}