//  Created by Matt Purchase.
//  Copyright (c) 2018 Matt Purchase. All rights reserved.
using System;
using UnityEditor;
using UnityEngine;

public class GuidGenerator {
	[MenuItem("Anchorite/utils/Generate Guid %g")]
	public static void CaptureScreen() {
		string guid = Guid.NewGuid().ToString();
		TextEditor te = new TextEditor();
		te.text = guid;
		te.SelectAll();
		te.Copy();

		LogUtils.Log("GUID added to clipboard");
		LogUtils.Log(guid);
	}
}
