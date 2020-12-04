using TMPro;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
	public static PlayerStatsManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI presentsTotalText;

	private int _presentsTotal;
	public int PresentsTotal {
		get => _presentsTotal;
		set {
			_presentsTotal = value;
			UpdateUI();
		}
	}

	private void Awake()
	{
		Instance = this;
		UpdateUI();
	}

	private void UpdateUI()
	{
		presentsTotalText.text = $"Presents: {PresentsTotal}";
	}
}
