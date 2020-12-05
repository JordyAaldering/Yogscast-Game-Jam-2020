using UnityEngine;

public class WorkerTable : Table
{
	[SerializeField] private GameObject enabledOnBuy;
	[SerializeField] private Animator workerAnimator;
	[SerializeField] private GameObject sleepIcon;

	[SerializeField] private float minSleepWait;
	[SerializeField] private float maxSleepWait;
	private float timeUntilSleep;
	private bool isSleeping;

	private void Awake()
	{
		Cost = buyCost;
		timeUntilSleep = Random.Range(minSleepWait, maxSleepWait);

		enabledOnBuy.SetActive(false);
		sleepIcon.SetActive(false);
	}

	private void Update()
	{
		if (!IsBought) {
			return;
		}

		if (timeUntilSleep >= 0f) {
			timeUntilSleep -= Time.deltaTime;
		} else if (!isSleeping) {
			PlayerStatsManager.Instance.Efficiency -= Efficiency;

			workerAnimator.SetBool("isSleeping", true);
			sleepIcon.SetActive(true);
			isSleeping = true;
		}
	}

	public override void HandleClick()
	{
		if (isSleeping) {
			PlayerStatsManager.Instance.Efficiency += Efficiency;
			timeUntilSleep = Random.Range(minSleepWait, maxSleepWait);
			
			workerAnimator.SetBool("isSleeping", false);
			sleepIcon.SetActive(false);
			isSleeping = false;
		}
	}

	public override void HandleInteract()
	{
		// wake up the worker
		HandleClick();

		if (PlayerStatsManager.Instance.PresentsTotal < Cost) {
			return;
		}

		PlayerStatsManager.Instance.PresentsTotal -= Cost;
		Cost += upgradeCostIncrease;

		if (!IsBought) {
			PlayerStatsManager.Instance.Efficiency += initialEfficiency;
			FindObjectOfType<ProgressPanel>().IncreaseCounter(tableName);
			Efficiency = initialEfficiency;

			enabledOnBuy.SetActive(true);
			IsBought = true;
		} else {
			PlayerStatsManager.Instance.Efficiency += upgradeEfficiencyIncrease;
			minSleepWait += upgradeEfficiencyIncrease;
			maxSleepWait += upgradeEfficiencyIncrease;
			Efficiency += upgradeEfficiencyIncrease;
		}
	}
}
