using UnityEngine;

public abstract class Table : MonoBehaviour
{
	[SerializeField] protected int buyCost;
	[SerializeField] protected int upgradeCostIncrease;
	[SerializeField] protected int initialEfficiency;
	[SerializeField] protected int upgradeEfficiencyIncrease;

	protected int cost;
	protected int efficiency;

	private bool bought = false;

	private void Awake()
	{
		cost = buyCost;
		efficiency = initialEfficiency;
	}

	public abstract void HandleClick();

	public void HandleInteract()
	{
		if (PlayerStatsManager.Instance.PresentsTotal < cost) {
			return;
		}

		PlayerStatsManager.Instance.PresentsTotal -= cost;
		cost += upgradeCostIncrease;

		if (!bought) {
			bought = true;

			Debug.Log("table buy");
			OnBuy();
		} else {
			efficiency += upgradeEfficiencyIncrease;

			Debug.Log("table upgrade");
			OnUpgrade();
		}
	}

	protected abstract void OnBuy();

	protected abstract void OnUpgrade();
}
