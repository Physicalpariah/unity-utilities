//Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DiceTests {
	// Properties

	// Tests
	[SetUp]
	public void Setup() {

	}

	[TearDown]
	public void TearDown() {

	}

	[Test]
	public void WillRollD6() {

		int diceSize = 6;
		for (int a = 0; a < 100; a++) {
			int roll = Dice.Roll(diceSize);
			if (roll < 0) {
				Assert.Fail("less than zero");
			}

			if (roll >= diceSize) {
				Assert.Fail("Larger than size");
			}

		}
		Assert.Pass();
	}

	[Test]
	public void WillRollFloat6() {
		float diceSize = 6.2f;
		for (int a = 0; a < 100; a++) {
			int roll = Dice.Roll(diceSize);
			if (roll < 0) {
				Assert.Fail("less than zero");
			}

			if (roll > Mathf.CeilToInt(diceSize)) {
				Assert.Fail("Larger than size: " + roll + "/" + diceSize);
			}

		}
		Assert.Pass();
	}

	[Test]
	public void WillRollRange() {

		int diceMin = 0;
		int diceSize = 6;

		for (int a = 0; a < 100; a++) {
			int roll = Dice.Roll(diceMin, diceSize);
			if (roll < 0) {
				Assert.Fail("less than zero");
			}

			if (roll > diceSize) {
				Assert.Fail("Larger than size");
			}

		}
		Assert.Pass();
	}


	[Test]
	public void WillBeTrueIfWithin() {

		float low = 3;
		float high = 6;
		float withinVal = 3;

		bool within = Dice.IsWithin(withinVal, low, high);

		if (within) {
			Assert.Pass();
		}

		Assert.Fail();
	}

	[Test]
	public void WillbeFalseIfNotWithin() {
		float low = 3;
		float high = 6;
		float withinVal = 12;

		bool within = Dice.IsWithin(withinVal, low, high);

		if (!within) {
			Assert.Pass();
		}

		Assert.Fail();
	}

	[Test]
	public void WillBeTrueIfBetween() {
		float low = 3;
		float high = 6;
		float withinVal = 4;

		bool within = Dice.IsBetween(withinVal, low, high);

		if (within) {
			Assert.Pass();
		}

		Assert.Fail();
	}


	[Test]
	public void WillBeFalseIfNotBetween() {
		float low = 3;
		float high = 6;
		float withinVal = 3;

		bool within = Dice.IsBetween(withinVal, low, high);

		if (!within) {
			Assert.Pass();
		}

		Assert.Fail();
	}


}