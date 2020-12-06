using System;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
	[SerializeField] private GameObject disableCanvas;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F12)) {
			disableCanvas.SetActive(false);
			TakeScreenshot();
			disableCanvas.SetActive(true);

			TakeScreenshot();
		}
	}

	private void TakeScreenshot()
	{
		string path = $"Screenshots/{DateTime.Now:yyyy-MM-dd HH-mm-ss}.png";
		ScreenCapture.CaptureScreenshot(path);
		Debug.Log($"Screenshot saved to {path}");
	}
}
