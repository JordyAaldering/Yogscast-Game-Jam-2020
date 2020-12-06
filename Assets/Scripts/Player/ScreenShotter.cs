using System;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F12)) {
			ScreenCapture.CaptureScreenshot($"Screenshots/{DateTime.Now}");
		}
	}
}
