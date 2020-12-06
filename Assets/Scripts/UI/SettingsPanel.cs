using TMPro;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private GameObject settingsPanel;
	[SerializeField] private TextMeshProUGUI quitText;
	[SerializeField] private AudioSource musicSource;

	private bool confirmedQuit = false;

	public static bool IsOpen { get; private set; } = false;

	private void Awake()
	{
		Continue();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (IsOpen) {
				Continue();
			} else {
				OpenSettings();
			}
		}
	}

	public void MusicVolume(float val)
	{
		musicSource.volume = val;
	}

	public void OpenSettings()
	{
		IsOpen = true;

		settingsPanel.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		quitText.text = "Quit";
		confirmedQuit = false;
	}

	public void Continue()
	{
		IsOpen = false;

		settingsPanel.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void Quit()
	{
		if (confirmedQuit) {
			Debug.Log("Quitting application");
			Application.Quit();
		} else {
			quitText.text = "Are you sure?";
			confirmedQuit = true;
		}
	}
}
