//  Created by Matt Purchase.
//  Copyright (c) 2020 Matt Purchase. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public static class DeviceUtils {

	public static n_deviceType m_debugDevice;
	public static n_deviceInput m_input { get; private set; }

	public static EventHandler e_inputChanged;

	public static void SetDevice(n_deviceInput input) {
		m_input = input;
		if (e_inputChanged != null) {
			e_inputChanged(null, null);
		}
	}

	public static bool ValidateAppleTVGen3() {
		bool isDevice = false;


#if UNITY_IOS
		if (UnityEngine.tvOS.Device.generation == UnityEngine.tvOS.DeviceGeneration.AppleTVHD) {
			isDevice = true;
		}
#endif

		return isDevice;
	}

	public static bool m_isApplePlatform {

		get {
			switch (Application.platform) {
				case (RuntimePlatform.IPhonePlayer): {
						return true;
					}
				case (RuntimePlatform.OSXPlayer): {
						return true;
					}
				case (RuntimePlatform.tvOS): {
						return true;
					}
				default: {
						return false;
					}
			}
		}
	}

	public static n_deviceType m_deviceType {
		get {
			if (isiPhone) {
				return n_deviceType.iPhone;
			}
			if (isIPad) {
				return n_deviceType.iPad;
			}
			if (isMac) {
				return n_deviceType.mac;
			}
			if (isTV) {
				return n_deviceType.appleTV;
			}
			return n_deviceType.iPhone;
		}
	}

	public static bool isScreenPortrait {
		get {
			if (Screen.height > Screen.width) {
				return true;
			}
			return false;
		}
	}

	public static bool isMac {
		get {
			if (Application.platform == RuntimePlatform.OSXPlayer) {
				return true;
			}
			if (Application.platform == RuntimePlatform.OSXEditor && m_debugDevice == n_deviceType.mac) {
				return true;
			}
			return false;
		}
	}

	public static bool isTV {
		get {
			if (Application.platform == RuntimePlatform.tvOS) {
				return true;
			}
			if (Application.platform == RuntimePlatform.OSXEditor && m_debugDevice == n_deviceType.mac) {
				return true;
			}
			return false;
		}
	}

	public static bool isiPhone {
		get {
			if (isPlatformiOS) {
				if (!DeviceUtils.isIPad) {
					return true;
				}
			}
			return false;
		}
	}

	private static bool isPlatformiOS {
		get {
			if (Application.platform == RuntimePlatform.OSXEditor) {
				if (m_debugDevice == n_deviceType.iPhone) {
					return true;
				}
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return true;
			}
			return false;
		}
	}

	public static bool isIphone13OrHigher {
		get {
#if UNITY_IOS
			if (Device.generation == DeviceGeneration.iPhone13Pro) {
				return true;
			}
			if (Device.generation == DeviceGeneration.iPhone13ProMax) {
				return true;
			}

			// if (Device.generation == DeviceGeneration.iPhone14Pro){
			// 	return true;
			// }

			// if (Device.generation == DeviceGeneration.iPhone14ProMax){
			// 	return true;
			// }
#endif
			return false;
		}
	}

	public static bool isIPad {
		get {
			bool isIpad = false;

			if (Application.platform == RuntimePlatform.OSXEditor) {
				if (m_debugDevice == n_deviceType.iPad) {
					return true;
				}
			}

			if (Application.platform != RuntimePlatform.IPhonePlayer) {
				return false;
			}




#if UNITY_IOS
			if (Device.generation == DeviceGeneration.iPad1Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPad2Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPad3Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPad4Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPad5Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPad6Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPad7Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadAir1)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadAir2)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadAir3Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadMini1Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadMini2Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadMini3Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadMini4Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadMini5Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadMini6Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadPro10Inch1Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadPro10Inch2Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadPro11Inch)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadPro1Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadPro2Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadPro3Gen)
				isIpad = true;

			if (Device.generation == DeviceGeneration.iPadUnknown)
				isIpad = true;

			// if (Device.generation == DeviceGeneration.iPadMini6Gen)
			// 	isIpad = true;

#endif
			return isIpad;
		}
	}



}

public enum n_deviceInput {
	touch,
	controller,
	keyboard,
}


public enum n_deviceType {
	iPhone,
	iPad,
	appleTV,
	mac,
	editor,
	android,
}
