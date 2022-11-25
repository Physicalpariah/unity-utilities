//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CreateAssetMenuAttribute(menuName = "anchorite/utils/sceneTabs")]
public class SceneTabObject : ScriptableObject {
	public List<SceneAsset> m_scenes;
}