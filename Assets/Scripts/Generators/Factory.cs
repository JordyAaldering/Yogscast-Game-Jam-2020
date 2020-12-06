﻿using UnityEngine;

public class Factory : Generator
{
	[SerializeField] private GameObject enabledOnBreak;

	[SerializeField] private float audioChance;
	[SerializeField] private AudioClip punchSound;
	[SerializeField] private AudioClip machineSound;

	[SerializeField] private float minBreakWait;
	[SerializeField] private float maxBreakWait;
	private float timeUntilBreak;
	private bool isBroken;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();

		Cost = buyCost;
		timeUntilBreak = Random.Range(minBreakWait, maxBreakWait);

		enabledOnBreak.SetActive(false);
	}

	private void Update()
	{
		if (!IsBought) {
			return;
		}

		if (timeUntilBreak >= 0f) {
			timeUntilBreak -= Time.deltaTime;
		} else if (!isBroken) {
			PlayerStatsManager.Instance.Efficiency -= Efficiency;

			if (!audioSource.isPlaying && Random.Range(0f, 1f) < audioChance) {
				audioSource.PlayOneShot(machineSound);
			}

			enabledOnBreak.SetActive(true);
			isBroken = true;
		}
	}

	public override void HandleClick()
	{
		if (isBroken) {
			PlayerStatsManager.Instance.Efficiency += Efficiency;
			timeUntilBreak = Random.Range(minBreakWait, maxBreakWait);

			audioSource.PlayOneShot(punchSound);
			enabledOnBreak.SetActive(false);
			isBroken = false;
		}
	}

	public override void HandleInteract()
	{
		// repair factory
		HandleClick();

		if (PlayerStatsManager.Instance.PresentsTotal < Cost) {
			return;
		}

		PlayerStatsManager.Instance.PresentsTotal -= Cost;
		Cost = (int)(Cost * upgradeCostMultiplier);

		if (!IsBought) {
			PlayerStatsManager.Instance.Efficiency += initialEfficiency;
			FindObjectOfType<ProgressPanel>().IncreaseCounter(tableName);
			Efficiency = initialEfficiency;

			IsBought = true;
		} else {
			PlayerStatsManager.Instance.Efficiency += upgradeEfficiencyIncrease;
			minBreakWait += upgradeEfficiencyIncrease;
			maxBreakWait += upgradeEfficiencyIncrease;
			Efficiency += upgradeEfficiencyIncrease;
		}
	}
}
