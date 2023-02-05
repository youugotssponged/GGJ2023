using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TowerUpgradeUI : MonoBehaviour, IDisposable
{
    private IPlayer Player;
    private ITower SelectedTower;
    public AudioClip CashRegisterSound;
    public AudioClip UpgradeDenied;
    private AudioSource _Source;
    public GameObject TowerUpgradeUIPanelRef;
    private GameObject TowerToSell;
    private TowerSocket SocketRef;
    public double SellTaxPercent = 0.75d;
    public Text UpgradeCostText;

    public void Awake()
    {
        _Source = GetComponent<AudioSource>();
        _Source.clip = CashRegisterSound;
        Player = GameObject.Find("Player").GetComponent<IPlayer>();
    }

    public void ShowUpgradeMenu(TowerSocket socketToApplyTowerUpgradeTo)
    {
        SocketRef = socketToApplyTowerUpgradeTo;
        TowerToSell = socketToApplyTowerUpgradeTo.SpawnPoint.GetChild(2).gameObject;
        SelectedTower = TowerToSell.GetComponent<ITower>();
        TowerUpgradeUIPanelRef.SetActive(true);

        Debug.Log("SelectedTower not null: " + SelectedTower != null);
        UpgradeCostText.text = "Next Upgrade: " + (SelectedTower.InitialCost * SelectedTower.UpgradeLevel);
    }

    public void SellSelectedTower()
    {
        Player.GainCurrency((int)(SelectedTower.TotalSpentOnTower * SellTaxPercent));
        SocketRef.IsOccupied = false;
        Destroy(TowerToSell);
        _Source.Play();
        CloseUpgradeMenu();
    }

    public void ApplyUpgrade()
    {
        if (SelectedTower.UpgradeLevel != 4)
        {
            int upgradeCost = SelectedTower.InitialCost * SelectedTower.UpgradeLevel;
            if (Player.Currency > 0 && Player.Currency >= upgradeCost)
            {
                SelectedTower.UpgradeLevel += 1;
                Player.GainCurrency(-upgradeCost);
                SelectedTower.TotalSpentOnTower += upgradeCost;

                SelectedTower.ApplyUpgrade();
                _Source.Play();
                CloseUpgradeMenu();
            }
        } 
        else
        {
            _Source.PlayOneShot(UpgradeDenied);
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
