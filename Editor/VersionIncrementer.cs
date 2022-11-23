//from http://answers.unity3d.com/questions/45186/can-i-auto-run-a-script-when-editor-launches-or-a.html
//

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


#if UNITY_IPHONE
using UnityEditor.iOS.Xcode;
#endif

[InitializeOnLoad]
public class VersionIncrementor {

	[PostProcessBuild]
	public static void OnPostProcessBuild(BuildTarget target, string path) {
		IncrementBuild();
		LogUtils.Log("err");
	}


	static VersionIncrementor() {
		//If you want the scene to be fully loaded before your startup operation,
		// for example to be able to use Object.FindObjectsOfType, you can defer your
		// logic until the first editor update, like this:
		EditorApplication.update += RunOnce;
	}


	static void RunOnce() {
		LogUtils.LogPriority("Processing build");
		EditorApplication.update -= RunOnce;
		IncrementBuild();
	}


	public static void IncrementVersion() {

		//1.4870
		string versionText = PlayerSettings.bundleVersion;
		if (!string.IsNullOrEmpty(versionText)) {
			string[] lines = versionText.Split('.');
			int majorVersion = int.Parse(lines[0]);
			int minorVersion = int.Parse(lines[1]);

			minorVersion += 10;

			if (minorVersion > 999) {
				majorVersion++;
				minorVersion = 0;
			}

			versionText = majorVersion.ToString("0") + "." + minorVersion.ToString("000");
			LogUtils.Log("Setting version: " + versionText);
			PlayerSettings.bundleVersion = versionText;

			float build = GetBuildNumber();
			SetBuildNumber(build);
		}
	}


	public static void IncrementPatch() {

		string versionText = PlayerSettings.bundleVersion;
		if (!string.IsNullOrEmpty(versionText)) {
			string[] lines = versionText.Split('.');
			int majorVersion = int.Parse(lines[0]);
			int minorVersion = int.Parse(lines[1]);

			minorVersion += 1;

			if (minorVersion > 999) {
				majorVersion++;
				minorVersion = 0;
			}

			versionText = majorVersion.ToString("0") + "." + minorVersion.ToString("000");
			LogUtils.Log("Setting version: " + versionText);
			PlayerSettings.bundleVersion = versionText;

			float build = GetBuildNumber();
			SetBuildNumber(build);
		}
	}


	public static void IncrementBuild() {
		float build = GetBuildNumber();
		build++;
		SetBuildNumber(build);

		if (BuildInfoData.Instance.m_incrementsPackage) {
			TextAsset packageJson = (TextAsset)AssetDatabase.LoadAssetAtPath(BuildInfoData.Instance.m_packageJSONPath, typeof(TextAsset));
			Package pack = JsonUtility.FromJson<Package>(packageJson.text);
			pack.version = Application.version + "." + PlayerSettings.iOS.buildNumber;

			string json = JsonUtility.ToJson(pack);

			File.WriteAllText(AssetDatabase.GetAssetPath(packageJson), json);
			EditorUtility.SetDirty(packageJson);
		}
		BuildInfoData.Instance.SetVersionNumber(BuildInfoData.Instance.ParseVersionNumber(Application.version));
		BuildInfoData.Instance.SetBuildNumber(BuildInfoData.Instance.ParseBuildNumber(PlayerSettings.iOS.buildNumber));
	}

	private static float GetBuildNumber() {
		string buildText = PlayerSettings.iOS.buildNumber;

		if (!string.IsNullOrEmpty(buildText)) {
			float count = float.Parse(buildText);
			return count;
		}

		return 0;
	}

	private static void SetBuildNumber(float count) {
		PlayerSettings.macOS.buildNumber = count.ToString();
		PlayerSettings.tvOS.buildNumber = count.ToString();
		PlayerSettings.iOS.buildNumber = count.ToString();
		PlayerSettings.Android.bundleVersionCode = (int)count;
		BuildInfoData.Instance.SetBuildNumber((int)count);
	}
}

