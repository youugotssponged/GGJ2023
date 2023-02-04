using UnityEngine;

public class SniperTower : MonoBehaviour, ITower
{
    public string TowerName => "SniperTower";
    public int InitialCost => 10;
    public int UpgradeLevel { get; set; } = 1;
    public int TotalSpentOnTower { get; set; }
    public float CoolDownTimeInSeconds => 5;
    public int Damage => 100;
    [field: SerializeField] public GameObject EffectOnEnemy { get; set; }
    [field: SerializeField] public AudioClip ShootSound { get; set; }

    public void Awake()
    {
        TotalSpentOnTower = InitialCost;
    }

    public void ApplyUpgrade()
    {

    }
}
