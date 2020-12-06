using System.Collections;
using UnityEngine;

public class ElfWorker : Generator
{
	[SerializeField] private GameObject enabledOnBuy;
	[SerializeField] private Animator workerAnimator;
	[SerializeField] private GameObject wakeEffect;
	[SerializeField] private GameObject sleepIcon;

	[SerializeField] private float audioWaitMin;
	[SerializeField] private float audioWaitMax;
	[SerializeField] private AudioClip punchSound;
	[SerializeField] private AudioClip hammerSound;

	[SerializeField] private float minSleepWait;
	[SerializeField] private float maxSleepWait;
	private float timeUntilSleep;
	private bool isSleeping;

	public static bool WakeAllSleeping { get; set; } = false;
	public static int SleepBonus { get; set; } = 0;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();

		Cost = buyCost;
		timeUntilSleep = Random.Range(minSleepWait, maxSleepWait);

		enabledOnBuy.SetActive(false);
		sleepIcon.SetActive(false);
	}

	private void Update()
	{
		if (!IsBought) {
			return;
		}

		if (timeUntilSleep >= 0f) {
			timeUntilSleep -= Time.deltaTime;
		} else if (!isSleeping) {
			PlayerStatsManager.Instance.Efficiency -= Efficiency;
			
			workerAnimator.SetBool("isSleeping", true);
			sleepIcon.SetActive(true);
			isSleeping = true;
		}
	}

	private IEnumerator AudioLoop()
	{
		while (true) {
			if (!isSleeping && !audioSource.isPlaying) {
				audioSource.PlayOneShot(hammerSound);
			}
			yield return new WaitForSeconds(Random.Range(audioWaitMin, audioWaitMax));
		}
	}

	public override void HandleClick()
	{
		if (WakeAllSleeping) {
			foreach (var elf in FindObjectsOfType<ElfWorker>()) {
				elf.WakeUp();
			}
		} else {
			WakeUp();
		}
	}

	public void WakeUp()
	{
		if (isSleeping) {
			PlayerStatsManager.Instance.Efficiency += Efficiency;
			timeUntilSleep = Random.Range(minSleepWait, maxSleepWait) + SleepBonus;

			audioSource.PlayOneShot(punchSound);
			wakeEffect.SetActive(true);
			workerAnimator.SetBool("isSleeping", false);
			sleepIcon.SetActive(false);
			isSleeping = false;
		}
	}

	public override void HandleInteract()
	{
		// wake up the worker
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

			StartCoroutine(AudioLoop());

			enabledOnBuy.SetActive(true);
			IsBought = true;
		} else {
			PlayerStatsManager.Instance.Efficiency += upgradeEfficiencyIncrease;
			minSleepWait += upgradeEfficiencyIncrease * 5;
			maxSleepWait += upgradeEfficiencyIncrease * 6;
			Efficiency += upgradeEfficiencyIncrease;
		}
	}
}
