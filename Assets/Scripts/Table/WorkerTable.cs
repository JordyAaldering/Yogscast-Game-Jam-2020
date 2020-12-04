using UnityEngine;

public class WorkerTable : MonoBehaviour, IClickable
{
	[SerializeField] private int cost;
	[SerializeField] private int efficiency;
	[SerializeField] private GameObject enabledOnBuy;

	private bool bought = false;

	public void HandleClick()
	{
		if (bought) {
			return;
		}

		if (PlayerStatsManager.Instance.PresentsTotal >= cost) {
			PlayerStatsManager.Instance.PresentsTotal -= cost;
			PlayerStatsManager.Instance.PresentsPM += efficiency;
			enabledOnBuy.SetActive(true);
			bought = true;
		}
	}
}
