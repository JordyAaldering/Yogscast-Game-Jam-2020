using UnityEngine;

public class Portal : Generator
{
	[SerializeField] private GameObject enabledOnBuy;
	[SerializeField] private float clickCooldown;
	private float cooldown;

	private void Awake()
	{
		Cost = buyCost;
		enabledOnBuy.SetActive(false);
	}

	private void Update()
	{
		if (cooldown > 0f) {
			cooldown -= Time.deltaTime;
		}
	}

	public override void HandleClick()
	{
		if (cooldown <= 0f) {
			PlayerStatsManager.Instance.PresentsTotal += Efficiency;
			PlayerStatsManager.Instance.Happiness += Efficiency * PlayerStatsManager.Instance.HappinessModifier;
			cooldown = clickCooldown;
		}
	}
	
	public override void HandleInteract()
	{
		if (PlayerStatsManager.Instance.PresentsTotal < Cost) {
			return;
		}

		PlayerStatsManager.Instance.PresentsTotal -= Cost;
		Cost = (int)(Cost * upgradeCostMultiplier);

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
