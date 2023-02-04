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

    [field: SerializeField] public GameObject[] UpgradeTowers { get; set; }

    public void Awake()
    {
        TotalSpentOnTower = InitialCost;
    }

    private Vector3 ScaleFactor = new Vector3(0.3f, 0.3f, 0.3f);

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

        // Fix rotation issue
        //var go2 = go.transform.GetChild(0).GetChild(0).GetChild(0);
        //go2.Rotate(0, 180, 0);
    }
}
