using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTower : MonoBehaviour, ITower
{
    public int InitialCost { get; } = 100;
    public string TowerName { get; } = "MachineTower";
    public int UpgradeLevel { get; set; } = 1;
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
