using UnityEngine;

public class RewardSled : MonoBehaviour
{
    [SerializeField] private GameObject enabledOnReady;
    [SerializeField] private int happinessRequired;
    [SerializeField] private float happinessMultiplier;

    private bool rewardReady = false;

	private void Awake()
	{
        enabledOnReady.SetActive(false);
	}

	public void CheckRewardReady()
	{
        if (!rewardReady && PlayerStatsManager.Instance.Happiness > happinessRequired) {
            happinessRequired = (int)(happinessRequired * happinessMultiplier);
            enabledOnReady.SetActive(true);
            rewardReady = true;
		}
	}

    public void ClaimReward()
	{
        enabledOnReady.SetActive(false);
        rewardReady = false;
	}
}
