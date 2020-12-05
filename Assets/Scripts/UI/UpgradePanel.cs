using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI efficiencyText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetInfo(Generator table)
	{
        if (!table.IsBought) {
            buyText.text = $"Buy {table.tableName}";
            costText.text = $"Cost: {table.Cost} Presents";
            efficiencyText.text = $"Efficiency: {table.Efficiency}";
            descriptionText.text = table.description;
        } else {
            buyText.text = $"Upgrade {table.tableName}";
            costText.text = $"Cost: {table.Cost} Presents";
            efficiencyText.text = $"New Efficiency: {table.Efficiency + table.upgradeEfficiencyIncrease}";
            descriptionText.text = table.upgradeInfo;
        }
    }
}
