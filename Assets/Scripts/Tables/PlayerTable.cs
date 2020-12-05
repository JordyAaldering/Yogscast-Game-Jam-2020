using UnityEngine;

public class PlayerTable : Table
{
	[SerializeField] private float clickCooldown;
	private float cooldown;

	private void Update()
	{
		if (cooldown > 0f) {
			cooldown -= Time.deltaTime;
		}
	}

	public override void HandleClick()
	{
		if (cooldown <= 0f) {
			PlayerStatsManager.Instance.PresentsTotal += efficiency;
			cooldown = clickCooldown;
		}
	}

	protected override void OnBuy()
	{
		Debug.LogError("player table is bought by default");
	}

	protected override void OnUpgrade()
	{
		
	}
}
