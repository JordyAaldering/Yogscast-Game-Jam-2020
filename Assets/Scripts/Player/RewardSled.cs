using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RewardSled : MonoBehaviour
{
    [SerializeField] private GameObject enabledOnReady;
    [SerializeField] private int happinessRequired;
    [SerializeField] private float happinessMultiplier;
    public int HappinessRequired => happinessRequired;

    [SerializeField] private AudioClip pingSound;
    [SerializeField] private GameObject rewardParticles;

    [SerializeField] private TextMeshProUGUI rewardText;
	[SerializeField] private List<UnityEvent> rewards;

    public bool RewardReady { get; private set; }

    private AudioSource audioSource;
    private ProgressPanel progress;

    private void Awake()
    {
        progress = FindObjectOfType<ProgressPanel>();
        audioSource = GetComponent<AudioSource>();
        enabledOnReady.SetActive(false);
	}

	public void CheckRewardReady()
	{
        if (!RewardReady && PlayerStatsManager.Instance.Happiness >= happinessRequired) {
            enabledOnReady.SetActive(true);
            RewardReady = true;
		}
	}

    public void ClaimReward()
	{
        if (RewardReady) {
            happinessRequired = (int)(happinessMultiplier *
                Mathf.Max(PlayerStatsManager.Instance.Happiness, happinessRequired));
            enabledOnReady.SetActive(false);
            RewardReady = false;

            audioSource.PlayOneShot(pingSound);
            rewardParticles.SetActive(true);

            int index = UnityEngine.Random.Range(0, rewards.Count);
			rewards[index].Invoke();
		}
    }

	/*
     * Rewards
     */

	public void ElfEfficiencyBonus()
	{
		SetText("Your elves are now slightly more efficient!");
		PlayerStatsManager.Instance.Efficiency += progress.progress["Elf Worker"].Item1;
	}

	public void FactoryEfficiencyBonus()
	{
		SetText("Your factories are now slightly more efficient!");
		PlayerStatsManager.Instance.Efficiency += progress.progress["Factory"].Item1 * 10;
	}

	public void PortalEfficiencyBonus()
	{
		SetText("The portal is now slightly more efficient!");
		PlayerStatsManager.Instance.Efficiency += progress.progress["Portal"].Item1 * 100;
	}

	public void ElfBreakBonus()
	{
		SetText("Elves now require less sleep!");
		ElfWorker.SleepBonus += 10;
	}

	public void FactoryBreakBonus()
	{
		SetText("Your factories will now break down less!");
		Factory.BreakBonus += 10;
	}

	public void WalkSpeed()
	{
		SetText("You can now walk faster!");
		FindObjectOfType<PlayerController>().speed += 0.5f;
	}

	public void SprintSpeed()
	{
		SetText("You can now sprint faster!");
		FindObjectOfType<PlayerController>().sprintSpeed += 0.5f;
	}

	public void InteractDistance()
	{
		SetText("You can now reach further!");
		FindObjectOfType<PlayerController>().interactRange += 0.5f;
	}

	public void FixAllElves()
	{
		SetText("You can now wake all elves at once!");
		ElfWorker.WakeAllSleeping = true;
	}

	public void FixAllFactories()
	{
		SetText("You can now repair all factories at once!");
		Factory.RepairAllBroken = true;
	}

	private void SetText(string msg)
	{
		rewardText.gameObject.SetActive(true);
		rewardText.text = msg;
		StartCoroutine(rewardText.GetComponentInParent<DisableAfter>().Activate());
		
	}
}
