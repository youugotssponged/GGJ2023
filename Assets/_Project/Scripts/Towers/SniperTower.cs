using UnityEngine;

public class SniperTower : MonoBehaviour, ITower
{
    public string TowerName => "SniperTower";
    public int InitialCost => 300;
    public int UpgradeLevel { get; set; } = 1;
    public int TotalSpentOnTower { get; set; }
    public float CoolDownTimeInSeconds { get; set; } = 6;
    public int Damage { get; set; } = 200;
    [field: SerializeField] public GameObject EffectOnEnemy { get; set; }
    [field: SerializeField] public AudioClip ShootSound { get; set; }

    [field: SerializeField] public GameObject[] UpgradeTowers { get; set; }

    public void Awake()
    {
        TotalSpentOnTower = InitialCost;
    }

    private Vector3 ScaleFactor = new Vector3(0.3f, 0.3f, 0.3f);

    public void ApplyUpgrade()
    {
        CoolDownTimeInSeconds -= 0.15f;
        Damage += 15;

        var sc = GetComponent<SphereCollider>();
        sc.radius += 2f;

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
