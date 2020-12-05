using UnityEngine;

public class WorkerTable : Table
{
	[SerializeField] private GameObject enabledOnBuy;

	public override void HandleClick()
	{
		
	}

	protected override void OnBuy()
	{
		PlayerStatsManager.Instance.PresentsPM += initialEfficiency;

		enabledOnBuy.SetActive(true);
	}

	protected override void OnUpgrade()
	{
		PlayerStatsManager.Instance.PresentsPM += upgradeEfficiencyIncrease;
	}
}
