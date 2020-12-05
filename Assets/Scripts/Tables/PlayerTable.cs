﻿using UnityEngine;

public class PlayerTable : Table
{
	[SerializeField] private float clickCooldown;
	private float cooldown;

	private void Awake()
	{
		Cost = buyCost;
		Efficiency = initialEfficiency;
		IsBought = true;
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
			cooldown = clickCooldown;
		}
	}

	public override void HandleInteract()
	{
		if (PlayerStatsManager.Instance.PresentsTotal < Cost) {
			return;
		}

		PlayerStatsManager.Instance.PresentsTotal -= Cost;
		Cost += upgradeCostIncrease;

		Efficiency += upgradeEfficiencyIncrease;
	}
}
