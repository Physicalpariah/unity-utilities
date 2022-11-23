//Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ClickableTests {
	// Properties

	// Tests
	[SetUp]
	public void Setup() {

	}

	[TearDown]
	public void TearDown() {

	}

	[Test]
	public void WillRunTest() {
		Assert.Fail();
	}

	[UnityTest]
	public IEnumerator WillRunCoroutine() {
		yield return null;
		Assert.Fail();
	}
}