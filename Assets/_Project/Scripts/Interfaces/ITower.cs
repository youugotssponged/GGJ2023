using UnityEngine;

public interface ITower 
{
    public string TowerName { get; }
    public int InitialCost { get; }
    public int TotalSpentOnTower { get; set; }
    public int UpgradeLevel { get; set; }
    public float CoolDownTimeInSeconds { get; }
    public int Damage { get; }
    public GameObject EffectOnEnemy { get; set; }
    public AudioClip ShootSound { get; set; }
    public void ApplyUpgrade();
}
