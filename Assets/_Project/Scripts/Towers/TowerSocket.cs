using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSocket : MonoBehaviour, IInteractable
{
    public GameObject SpawnMenuPanelUIRef;
    private TowerSpawnUI TowerSpawnUIRef;
    public Transform SpawnPoint;
    public bool IsOccupied;

    private void Awake()
    {
        SpawnPoint = transform.Find("SpawnPoint");
        TowerSpawnUIRef = SpawnMenuPanelUIRef.GetComponent<TowerSpawnUI>();
    }


    public void Update()
    {
    }


    public void Interact()
    {
        TowerSpawnUIRef.ShowSpawnMenu(this);
    }
}
