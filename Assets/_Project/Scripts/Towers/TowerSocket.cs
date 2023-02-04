using UnityEngine;

public class TowerSocket : MonoBehaviour, IInteractable
{
    public GameObject SpawnMenuPanelUIRef;
    public GameObject UpradeUIPanelUIRef;

    private TowerSpawnUI TowerSpawnUIRef;
    private TowerUpgradeUI TowerUpgradeUIRef;

    public Transform SpawnPoint;
    public bool IsOccupied;

    private void Awake()
    {
        SpawnPoint = transform.Find("SpawnPoint").parent;
        TowerSpawnUIRef = SpawnMenuPanelUIRef.GetComponent<TowerSpawnUI>();
        TowerUpgradeUIRef = UpradeUIPanelUIRef.GetComponent<TowerUpgradeUI>();
    }

    public void Interact() 
    {
        if (IsOccupied)
            TowerUpgradeUIRef.ShowUpgradeMenu(this);
        else
            TowerSpawnUIRef.ShowSpawnMenu(this);
    }
}
