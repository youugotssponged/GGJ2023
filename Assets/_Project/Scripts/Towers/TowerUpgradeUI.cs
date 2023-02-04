using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TowerUpgradeUI : MonoBehaviour, IDisposable
{
    private IPlayer Player;
    private ITower SelectedTower;
    public AudioClip CashRegisterSound;
    private AudioSource _Source;
    public GameObject TowerUpgradeUIPanelRef;
    private GameObject TowerToSell;
    private TowerSocket SocketRef;
    public double SellTaxPercent = 0.75d;

    public void Awake()
    {
        _Source = GetComponent<AudioSource>();
        _Source.clip = CashRegisterSound;
        Player = GameObject.Find("Player").GetComponent<IPlayer>();
    }

    public void ShowUpgradeMenu(TowerSocket socketToApplyTowerUpgradeTo)
    {
        SocketRef = socketToApplyTowerUpgradeTo;
        TowerToSell = socketToApplyTowerUpgradeTo.SpawnPoint.GetChild(1).gameObject;
        SelectedTower = TowerToSell.GetComponent<ITower>();
        TowerUpgradeUIPanelRef.SetActive(true);
    }

    public void SellSelectedTower()
    {
        Player.Currency += (int)(SelectedTower.TotalSpentOnTower * SellTaxPercent);
        SocketRef.IsOccupied = false;
        Destroy(TowerToSell);
        _Source.Play();
        CloseUpgradeMenu();
    }

    public void ApplyUpgrade()
    {
        int upgradeCost = SelectedTower.InitialCost * SelectedTower.UpgradeLevel;
        if (Player.Currency > 0 && Player.Currency >= upgradeCost)
        {
            SelectedTower.UpgradeLevel += 1;
            Player.Currency -= upgradeCost;
            SelectedTower.TotalSpentOnTower += upgradeCost;

            SelectedTower.ApplyUpgrade();
            _Source.Play();
            CloseUpgradeMenu();
        }
    }

    public void CloseUpgradeMenu()
    {
        ReleaseReferences();
        TowerUpgradeUIPanelRef.SetActive(false);
    }

    private void ReleaseReferences()
    {
        TowerToSell = null;
        SocketRef = null;
        SelectedTower = null;
    }

    public void Dispose()
    {
        ReleaseReferences();
    }
}
