using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : MonoBehaviour, ITower
{
    public string TowerName => "SniperTower";
    public int InitialCost => 10;
    public int UpgradeLevel { get; set; } = 1;
    public int TotalSpentOnTower { get; set; }
    
    public void ApplyUpgrade()
    {

    }

    public void FireWeapon()
    {

    }
}
