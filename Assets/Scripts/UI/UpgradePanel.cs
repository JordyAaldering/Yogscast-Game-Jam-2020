using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI upgradeText;

    public void SetInfo(Generator table)
	{
        if (!table.IsBought) {
            titleText.text = $"Buy {table.tableName}\n";
            upgradeText.text = $"Cost: {table.Cost} Presents\n";
            upgradeText.text += table.Efficiency > 0 ? $"Efficiency: {table.Efficiency}\n" : "";
            upgradeText.text += table.description;
        } else {
            titleText.text = $"Upgrade {table.tableName}\n";
            upgradeText.text = $"Cost: {table.Cost} Presents\n";
            upgradeText.text += $"New Efficiency: {table.Efficiency + table.upgradeEfficiencyIncrease}\n";
            upgradeText.text += table.upgradeInfo;
        }
    }
}
