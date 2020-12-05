using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI efficiencyText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetInfo(Table table)
	{
        buyText.text = (table.IsBought ? "Upgrade " : "Buy ") + table.tableName;
        costText.text = $"Cost: {table.Cost}";
        efficiencyText.text = table.IsBought
            ? $"Efficiency: {table.Efficiency}"
            : $"New Efficiency: {table.Efficiency + table.upgradeEfficiencyIncrease}";
        descriptionText.text = table.description;
    }
}
