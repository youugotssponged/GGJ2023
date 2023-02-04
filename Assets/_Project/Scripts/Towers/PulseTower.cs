using UnityEngine;

public class PulseTower : MonoBehaviour, ITower
{
    public string TowerName => "PulseTower";
    public int Cost => 10;
    public int UpgradeLevel { get; set; } = 1;
    public int InitialCost => 50;
    public int TotalSpentOnTower { get; set; }
    public int Damage { get; set; } = 10;
    public float CoolDownTimeInSeconds => 1;
    [field: SerializeField] public GameObject EffectOnEnemy { get; set; }
    [field: SerializeField] public AudioClip ShootSound { get; set; }

    [field: SerializeField] public GameObject[] UpgradeTowers { get; set; }

    private Vector3 ScaleFactor = new Vector3(0.3f, 0.3f, 0.3f);


    public void Awake()
    {
        TotalSpentOnTower = InitialCost;
    }

    public void ApplyUpgrade()
    {
        switch (UpgradeLevel)
        {
            case 1:
                break;
            case 2:
                SwapTowerModel(1);
                break;
            case 3:
                SwapTowerModel(2);
                break;
            case 4:
                SwapTowerModel(3);
                break;
        }
    }

    private void SwapTowerModel(int zeroIndex)
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        var go = Instantiate(UpgradeTowers[zeroIndex], gameObject.transform);
        go.transform.localScale = ScaleFactor;
    }
}
