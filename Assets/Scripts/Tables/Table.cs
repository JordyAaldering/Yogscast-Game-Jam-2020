using UnityEngine;

public abstract class Table : MonoBehaviour
{
	public int buyCost;
	public int initialEfficiency;
	public int upgradeCostIncrease;
	public int upgradeEfficiencyIncrease;

	public string tableName;
	public string description;
	public string upgradeInfo;

	public int Cost { get; protected set; }
	public int Efficiency { get; protected set; }
	public bool IsBought { get; protected set; } = false;

	public abstract void HandleClick();
	public abstract void HandleInteract();
}
