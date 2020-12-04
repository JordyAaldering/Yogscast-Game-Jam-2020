using UnityEngine;

public class PlayerTable : MonoBehaviour, IClickable
{
	[SerializeField] private int efficiency;
	[SerializeField] private float cooldown;
	private float cooldownLeft;

	private void Update()
	{
		if (cooldownLeft > 0f) {
			cooldownLeft -= Time.deltaTime;
		}
	}

	public void HandleClick()
	{
		if (cooldownLeft <= 0f) {
			cooldownLeft = cooldown;
			PlayerStatsManager.Instance.PresentsTotal += efficiency;
		}

	}
}
