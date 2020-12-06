using System.Collections;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
	[SerializeField] private GameObject toDisable;
	[SerializeField] private float waitTime;

	public IEnumerator Activate()
	{
		yield return new WaitForSeconds(waitTime);
		toDisable.SetActive(false);
	}
}
