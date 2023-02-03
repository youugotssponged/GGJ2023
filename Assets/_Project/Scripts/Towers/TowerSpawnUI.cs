using System.Linq;
using UnityEngine;

public class TowerSpawnUI : MonoBehaviour
{
    [SerializeField] private IPlayer Player;
    [SerializeField] private ITower[] TowersToShowInSpawnMenu;
    [SerializeField] private GameObject[] TowerPrefabsToChooseFrom;
    [SerializeField] private GameObject TowerSpawnUIMenuPanel;

    private static Transform SpawnAt;
    private static TowerSocket LastChosenSocket;
    private ITower SelectedTower;

    private void Awake()
    {
        Player = new FakePlayer();
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
        TowerSpawnUIMenuPanel.SetActive(false);     
    }

    public void SelectTower(string TowerName)
    {
        var towerObj = TowerPrefabsToChooseFrom
                .Where(x => x.name.Contains(TowerName))
                .FirstOrDefault();

        SelectedTower = towerObj.GetComponent<ITower>();
    }

    public void ConfirmTowerSelection()
    {
        if (Player.Currency > 0 && Player.Currency >= SelectedTower.Cost)
        {
            // Spawn tower to Spawn Point that was given
            Player.Currency -= SelectedTower.Cost;

            var towerObj = TowerPrefabsToChooseFrom
                .Where(x => x.name.Contains(SelectedTower.TowerName))
                .FirstOrDefault();

            Debug.Log(SelectedTower.TowerName);
            Debug.Log(towerObj != null);
            
            Instantiate(towerObj, SpawnAt);
            LastChosenSocket.IsOccupied = true;
            LastChosenSocket = null;
            SpawnAt = null;
            CloseSpawnMenu();
        }
    }
}
