using UnityEngine;

public class Portal : Generator
{
	[SerializeField] private GameObject enabledOnBuy;
	[SerializeField] private float clickCooldown;
	private float cooldown;

	[SerializeField] private float audioChance;
	[SerializeField] private AudioClip jingleSound;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();

		Cost = buyCost;
		enabledOnBuy.SetActive(false);
	}

	private void Update()
	{
		if (!IsBought) {
			return;
		}

		if (!audioSource.isPlaying && Random.Range(0f, 1f) < audioChance) {
			audioSource.PlayOneShot(jingleSound);
		}

		if (cooldown > 0f) {
			cooldown -= Time.deltaTime;
		}
	}

	public override void HandleClick()
	{
		if (IsBought && cooldown <= 0f) {
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
