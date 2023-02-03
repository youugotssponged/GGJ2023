using UnityEngine;

public interface IEnemy : IDamageable
{
    public int CurrencyReward { get; set; }

    public void DestroyEnemy();
    public void CreditPlayer();
}
