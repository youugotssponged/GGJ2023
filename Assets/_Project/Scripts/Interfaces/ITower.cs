public interface ITower 
{
    public string TowerName { get; }
    public int InitialCost { get; }
    public int TotalSpentOnTower { get; set; }
    public int UpgradeLevel { get; set; }
    public void ApplyUpgrade();
    public void FireWeapon();
}
