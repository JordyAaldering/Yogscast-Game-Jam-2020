using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
	public static PlayerStatsManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI statsText;
	[SerializeField] private float happinessModifier;
	public float HappinessModifier => Random.Range(0.75f * happinessModifier, 1.1f * happinessModifier);

	private int _presentsTotal = 0;
	public int PresentsTotal {
		get => _presentsTotal;
		set {
			_presentsTotal = value;
			UpdateUI();
		}
	}

	private int _presentsPM = 0;
	public int Efficiency {
		get => _presentsPM;
		set {
			_presentsPM = value;
			UpdateUI();
		}
	}

	private float _happiness = 0f;
	public float Happiness {
		get => _happiness;
		set {
			_happiness = value;
			rewardSled.CheckRewardReady();
		}
	}

	private RewardSled rewardSled;

	private void Awake()
	{
		Instance = this;
		rewardSled = FindObjectOfType<RewardSled>();

		UpdateUI();
		StartCoroutine(PresentPMLoop());
	}

	private IEnumerator PresentPMLoop()
	{
		while (true) {
			yield return new WaitForSeconds(1f);
			PresentsTotal += Efficiency;
			Happiness += HappinessModifier * Efficiency;
		}
	}

	private void UpdateUI()
	{
		statsText.text = $"Presents: {PresentsTotal}\n";
		statsText.text += $"Efficiency: {Efficiency}\n";
		statsText.text += $"Happiness: {(int)Happiness}";
	}
}
