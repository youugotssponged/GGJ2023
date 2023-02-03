using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeUI : MonoBehaviour
{
    [SerializeField] private IPlayer Player;

    public void Awake()
    {
        Player = new FakePlayer();
    }

    // Start is called before the first frame update
    public GameObject TowerUpgradeUIPanelRef;
    private GameObject TowerToSell;
    private TowerSocket SocketRef;
    public double SellTaxPercent = 0.75d;

    public void SellSelectedTower()
    {
        // Increment Player Tower cost + upgrade spent cost - sell penalty
        var tower = TowerToSell.GetComponent<ITower>();
        Player.Currency += (int)(tower.TotalSpentOnTower * SellTaxPercent);
        SocketRef.IsOccupied = false;
        Destroy(TowerToSell);
        CloseUpgradeMenu();
    }

    public void ShowUpgradeMenu(TowerSocket socketToApplyTowerUpgradeTo)
    {
        SocketRef = socketToApplyTowerUpgradeTo;
        TowerToSell = socketToApplyTowerUpgradeTo.SpawnPoint.GetChild(1).gameObject;
        TowerUpgradeUIPanelRef.SetActive(true);
    }

    public void ApplyUpgrade()
    {
        var tower = TowerToSell.GetComponent<ITower>();
        tower.UpgradeLevel += 1;
        Player.Currency -= tower.InitialCost * tower.UpgradeLevel;
        tower.TotalSpentOnTower += tower.InitialCost * tower.UpgradeLevel;

        tower.ApplyUpgrade();

        SocketRef = null;
        TowerToSell = null;
        CloseUpgradeMenu();
    }

    public void CloseUpgradeMenu()
    {
        TowerToSell = null;
        SocketRef = null;
        TowerUpgradeUIPanelRef.SetActive(false);
    }
}
