using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI upgradeText;

    public void SetInfo(Generator table)
	{
        if (!table.IsBought) {
            titleText.text = $"[E] Buy {table.tableName}";
            upgradeText.text = $"Cost: {table.Cost} Presents\n";
            upgradeText.text += table.Efficiency > 0 ? $"Efficiency: {table.Efficiency}\n" : "";
            upgradeText.text += table.description;
        } else {
            titleText.text = $"[E] Upgrade {table.tableName}";
            upgradeText.text = $"Cost: {table.Cost} Presents\n";
            upgradeText.text += $"Efficiency: {table.Efficiency} -> {table.Efficiency + table.upgradeEfficiencyIncrease}\n";
            upgradeText.text += table.upgradeInfo;
        }
    }

    public void SetReward(RewardSled sled)
	{
        titleText.text = "[E] Get a reward";
        if (sled.RewardReady)
            upgradeText.text = "A new reward is ready for you";
		else {
            upgradeText.text = "Your reward is not ready yet\n";
            upgradeText.text += $"You need {sled.HappinessRequired} happiness";
		}
    }
}
