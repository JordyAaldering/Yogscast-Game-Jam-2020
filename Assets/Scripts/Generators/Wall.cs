public class Wall : Generator
{
	private void Awake()
	{
		Cost = buyCost;
	}

	public override void HandleClick()
	{
		HandleInteract();
	}

	public override void HandleInteract()
	{
		if (PlayerStatsManager.Instance.PresentsTotal >= Cost) {
			PlayerStatsManager.Instance.PresentsTotal -= Cost;
			gameObject.SetActive(false);
			FindObjectOfType<ProgressPanel>().IncreaseCounter(tableName);
			IsBought = true;
		}
	}
}
