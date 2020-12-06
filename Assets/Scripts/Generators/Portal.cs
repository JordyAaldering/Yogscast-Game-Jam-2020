using System.Collections;
using UnityEngine;

public class Portal : Generator
{
	[SerializeField] private GameObject enabledOnBuy;
	[SerializeField] private float clickCooldown;
	private float cooldown;

	[SerializeField] private float audioWaitMin;
	[SerializeField] private float audioWaitMax;
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
		if (IsBought && cooldown > 0f) {
			cooldown -= Time.deltaTime;
		}
	}

	private IEnumerator AudioLoop()
	{
		while (true) {
			if (!audioSource.isPlaying) {
				audioSource.PlayOneShot(jingleSound);
			}
			yield return new WaitForSeconds(Random.Range(audioWaitMin, audioWaitMax));
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

			StartCoroutine(AudioLoop());

			enabledOnBuy.SetActive(true);
			IsBought = true;
		} else {
			PlayerStatsManager.Instance.Efficiency += upgradeEfficiencyIncrease;
			Efficiency += upgradeEfficiencyIncrease;
		}
	}
}
