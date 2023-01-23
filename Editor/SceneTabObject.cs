//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CreateAssetMenuAttribute(menuName = "anchorite/utils/sceneTabs")]
public class SceneTabObject : ScriptableObject {
	[Header("This should be located at /Resources/sceneTabs")]
	public List<SceneAsset> m_scenes;

	public SceneTabObject() {
		m_scenes = new();
	}
}