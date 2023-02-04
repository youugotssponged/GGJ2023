using UnityEngine;

public class MachineTower : MonoBehaviour, ITower
{
    public int InitialCost { get; } = 20;
    public string TowerName { get; } = "MachineTower";
    public int UpgradeLevel { get; set; } = 1;
    public int TotalSpentOnTower { get; set; }
    public float CoolDownTimeInSeconds => 3;

    public int Damage => 20;
    [field: SerializeField] public GameObject EffectOnEnemy { get; set; }

    public void Awake()
    {
        TotalSpentOnTower = InitialCost;
    }

    public void ApplyUpgrade()
    {

    }
}
