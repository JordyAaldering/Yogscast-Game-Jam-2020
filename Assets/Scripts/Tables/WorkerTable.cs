using UnityEngine;

public class WorkerTable : Table
{
	[SerializeField] private GameObject enabledOnBuy;

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
			PlayerStatsManager.Instance.PresentsPM += initialEfficiency;

			enabledOnBuy.SetActive(true);
			IsBought = true;
		} else {
			PlayerStatsManager.Instance.PresentsPM += upgradeEfficiencyIncrease;
			Efficiency += upgradeEfficiencyIncrease;
		}
	}
}
