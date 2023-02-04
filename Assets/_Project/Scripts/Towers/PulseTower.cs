using UnityEngine;

public class PulseTower : MonoBehaviour, ITower
{
    public string TowerName => "PulseTower";
    public int Cost => 10;
    public int UpgradeLevel { get; set; } = 1;
    public int InitialCost => 200;
    public int TotalSpentOnTower { get; set; }

    public void Awake()
    {
        TotalSpentOnTower = InitialCost;
    }

    public void ApplyUpgrade()
    {

    }

    public void FireWeapon()
    {

    }
}
