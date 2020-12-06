using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardSled : MonoBehaviour
{
    [SerializeField] private GameObject enabledOnReady;
    [SerializeField] private int happinessRequired;
    [SerializeField] private float happinessMultiplier;
    public int HappinessRequired => happinessRequired;

    [SerializeField] private GameObject rewardParticles;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private List<string> rewardStrings;

    public bool RewardReady { get; private set; }

	private void Awake()
	{
        enabledOnReady.SetActive(false);
	}

	public void CheckRewardReady()
	{
        if (!RewardReady && PlayerStatsManager.Instance.Happiness > happinessRequired) {
            enabledOnReady.SetActive(true);
            RewardReady = true;
		}
	}

    public void ClaimReward()
	{
        if (RewardReady) {
            happinessRequired = (int)(happinessMultiplier *
                Mathf.Max(PlayerStatsManager.Instance.Happiness, happinessRequired));
            enabledOnReady.SetActive(false);
            RewardReady = false;

            rewardParticles.SetActive(true);
            rewardText.gameObject.SetActive(true);
            rewardText.text = rewardStrings[Random.Range(0, rewardStrings.Count)];
            rewardText.GetComponentInParent<DisableAfter>().Activate();
        }
    }
}
