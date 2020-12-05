using UnityEngine;

public class Factory : Generator
{
	[SerializeField] private GameObject enabledOnBuy;

	private void Awake()
	{
		Cost = buyCost;
		enabledOnBuy.SetActive(false);
	}

	public override void HandleClick()
	{
		
	}

	public override void HandleInteract()
	{
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
			Efficiency += upgradeEfficiencyIncrease;
		}
	}
}
