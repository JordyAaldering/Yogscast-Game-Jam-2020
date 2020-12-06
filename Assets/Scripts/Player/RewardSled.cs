using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardSled : MonoBehaviour
{
    [SerializeField] private GameObject enabledOnReady;
    [SerializeField] private int happinessRequired;
    [SerializeField] private float happinessMultiplier;

    [SerializeField] private GameObject rewardParticles;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private List<string> rewardStrings;

    private bool rewardReady = false;

	private void Awake()
	{
        enabledOnReady.SetActive(false);
	}

	public void CheckRewardReady()
	{
        if (!rewardReady && PlayerStatsManager.Instance.Happiness > happinessRequired) {
            enabledOnReady.SetActive(true);
            rewardReady = true;
		}
	}

    public void ClaimReward()
	{
        if (rewardReady) {
            happinessRequired = (int)(happinessMultiplier *
                Mathf.Max(PlayerStatsManager.Instance.Happiness, happinessRequired));
            enabledOnReady.SetActive(false);
            rewardReady = false;

            rewardParticles.SetActive(true);
            rewardText.gameObject.SetActive(true);
            rewardText.text = rewardStrings[Random.Range(0, rewardStrings.Count)];
            rewardText.GetComponentInParent<DisableAfter>().Activate();
        }
    }
}
