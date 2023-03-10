using UnityEngine;

public class MachineTower : MonoBehaviour, ITower
{
    public int InitialCost { get; } = 100;
    public string TowerName { get; } = "MachineTower";
    public int UpgradeLevel { get; set; } = 1;
    public int TotalSpentOnTower { get; set; }
    public float CoolDownTimeInSeconds { get; set; } = 0.65f;

    public int Damage { get; set; } = 100;
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
        CoolDownTimeInSeconds -= 0.05f;
        Damage += 10;

        var sc = GetComponent<SphereCollider>();
        sc.radius += 1;

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
