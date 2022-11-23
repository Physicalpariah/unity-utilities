//Created by Matt Purchase.
//  Copyright (c) 2022 Matt Purchase. All rights reserved.
using System.Collections.Generic;
using System;

public class Package {
	// Properties
	public string name;
	public string version;
	public string displayName;
	public string description;
	public string unity;
	public string unityRelease;
	public string documentationUrl;
	public string changelogUrl;
	public string licensesUrl;
	public Dependencies dependencies;
	public List<string> keywords;
	public Author author;
	// Initalisation Functions

	// Unity Callbacks

	// Public Functions

	// Private Functions

}

[Serializable]
public class Dependencies {

	
}

[Serializable]
public class Author {
	public string name;
	public string email;
	public string url;
}