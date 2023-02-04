using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    private int _currencyReward;
    public int CurrencyReward
    {
        get { return _currencyReward; }
        set { _currencyReward = value; }
    }
    [SerializeField]
    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    [SerializeField]
    private int _attackDamage = 1;
    public int AttackDamage
    {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    public void TakeDamage(int recievingDamage)
    {
        this.Health -= recievingDamage;

        if (this.Health <= 0)
        {
            DestroyEnemy();
            CreditPlayer();
        }
    }
    public void DestroyEnemy()
    {
        // Destroy this and give player currency.
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        if (waveManager != null )
            waveManager.GetComponent<WaveManager>().UpdateEnemiesRemaining();
    }

    public void CreditPlayer()
    {
        // ToDo Increase currency value of player.
    }
}
