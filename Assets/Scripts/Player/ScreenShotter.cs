using System;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F12)) {
			string path = $"Screenshots/{DateTime.Now:s}";
			ScreenCapture.CaptureScreenshot(path);
			Debug.Log($"Screenshot saved to {path}");
		}
	}
}
