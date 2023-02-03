using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTower : MonoBehaviour, ITower
{
    public int Cost { get; } = 10;
    public string TowerName { get; } = "MachineTower";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ApplyUpgrade()
    {
        throw new System.NotImplementedException();
    }

    public void FireWeapon()
    {
        throw new System.NotImplementedException();
    }

    public void OnTowerRemoved()
    {
        throw new System.NotImplementedException();
    }
}
