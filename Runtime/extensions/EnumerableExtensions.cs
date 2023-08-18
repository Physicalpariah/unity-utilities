//  Created by Matt Purchase.
//  Copyright (c) 2021 Matt Purchase. All rights reserved.
// 	copypasta'd from: https://stackoverflow.com/questions/2019417/how-to-access-random-item-in-list

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public static class EnumerableExtension {


	public static void AddUnique<T>(this List<T> source, T obj) {
		if (source.Contains(obj)) {
			return;
		}
		source.Add(obj);
	}

	public static T PickRandom<T>(this IEnumerable<T> source) {
		if (source.Count<T>() == 0) { return default(T); }
		return source.PickRandom(1).Single();
	}

	public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) {
		return source.Shuffle().Take(count);
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
		return source.OrderBy(x => Guid.NewGuid());
	}

	public static GameObject FindChildWithTag(this GameObject source, string tag) {
		var tags = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject obj in tags) {
			if (obj.transform.IsChildOf(source.transform)) {
				return obj;
			}
		}
		return null;
	}
}