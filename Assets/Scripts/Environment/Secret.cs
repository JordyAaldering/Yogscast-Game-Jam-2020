using UnityEngine;

public class Secret : MonoBehaviour
{
    [SerializeField] private int reward;

    public void Claim()
	{
		PlayerStatsManager.Instance.PresentsTotal += reward;
		gameObject.SetActive(false);
	}
}
