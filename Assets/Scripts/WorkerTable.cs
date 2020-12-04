using UnityEngine;

public class WorkerTable : MonoBehaviour, IClickable
{
	[SerializeField] private int cost;

	private bool bought;

	public void HandleClick()
	{
		if (bought) {
			return;
		}

		if (PlayerStatsManager.Instance.PresentsTotal >= cost) {
			PlayerStatsManager.Instance.PresentsTotal -= cost;
		}
	}
}
