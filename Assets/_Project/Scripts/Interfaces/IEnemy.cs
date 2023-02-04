public interface IEnemy : IDamageable
{
    public int CurrencyReward { get; set; }
    public int AttackDamage { get; set; }

    public void DestroyEnemy();
    public void CreditPlayer();
}
