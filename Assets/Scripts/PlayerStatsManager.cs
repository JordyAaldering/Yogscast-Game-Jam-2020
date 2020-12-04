using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
	public static PlayerStatsManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI presentsTotalText;
	[SerializeField] private TextMeshProUGUI presentsPMText;

	private int _presentsTotal = 0;
	public int PresentsTotal {
		get => _presentsTotal;
		set {
			_presentsTotal = value;
			UpdateUI();
		}
	}

	private int _presentsPM = 0;
	public int PresentsPM {
		get => _presentsPM;
		set {
			_presentsPM = value;
			UpdateUI();
		}
	}

	private void Awake()
	{
		Instance = this;

		UpdateUI();
		StartCoroutine(PresentPMLoop());
	}

	private IEnumerator PresentPMLoop()
	{
		while (true) {
			yield return new WaitForSeconds(1f);
			PresentsTotal += PresentsPM;
		}
	}

	private void UpdateUI()
	{
		presentsTotalText.text = $"Presents: {PresentsTotal}";
		presentsPMText.text = $"Presents: {PresentsPM}";
	}
}
