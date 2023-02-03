using UnityEngine;

public class PulseTower : MonoBehaviour, ITower
{
    public string TowerName => "PulseTower";

    public int Cost => 10;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
