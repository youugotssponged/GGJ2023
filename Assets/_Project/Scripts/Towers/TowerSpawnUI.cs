using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerSpawnUI : MonoBehaviour
{

    [SerializeField] private ITower[] TowersToShowInSpawnMenu;
    [SerializeField] private GameObject[] TowerPrefabsToChooseFrom;
    [SerializeField] private GameObject TowerSpawnUIMenuPanel;

    public AudioClip CashRegisterSound;
    private AudioSource _Source;
    private IPlayer Player;
    private static Transform SpawnAt;
    private static TowerSocket LastChosenSocket;
    private GameObject SelectedTowerObj;

    private void Awake()
    {
        _Source = GetComponent<AudioSource>();
        _Source.clip = CashRegisterSound;
        Player = GameObject.Find("Player").GetComponent<IPlayer>();
    }

    public void ShowSpawnMenu(TowerSocket socketToSpawnAt)
    {
        // Later we will Plug Data from TowersToShowInSpawnMenu array into UI elems
        // i.e name, price etc
        if (!socketToSpawnAt.IsOccupied)
        {
            SpawnAt = socketToSpawnAt.SpawnPoint;
            ShowSpawnMenuUI();
            LastChosenSocket = socketToSpawnAt; // hold ref
        }
    }

    public void ShowSpawnMenuUI()
    {
        TowerSpawnUIMenuPanel.SetActive(true);
    }

    public void CloseSpawnMenu()
    {
        LastChosenSocket = null;
        SpawnAt = null;
        SelectedTowerObj = null;
        TowerSpawnUIMenuPanel.SetActive(false);
    }

    public void SelectTower(string TowerName)
    {
        SelectedTowerObj = TowerPrefabsToChooseFrom
                .Where(x => x.name.Contains(TowerName))
                .FirstOrDefault();
    }

    public void ConfirmTowerSelection()
    {
        var tower = SelectedTowerObj.GetComponent<ITower>();
        if (Player.Currency > 0 && Player.Currency >= tower.InitialCost)
        {
            // Spawn tower to Spawn Point that was given
            Player.GainCurrency(-tower.InitialCost);
            Instantiate(SelectedTowerObj, SpawnAt);

            LastChosenSocket.IsOccupied = true;
            LastChosenSocket = null;
            SpawnAt = null;
            _Source.Play();
            CloseSpawnMenu();
        }
    }
}
