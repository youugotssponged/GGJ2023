public interface ITower 
{
    public string TowerName { get; }
    public int Cost { get; }
    public void ApplyUpgrade();
    public void OnTowerRemoved();
    public void FireWeapon();
}
