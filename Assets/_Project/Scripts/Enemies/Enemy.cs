using System.Collections;
using System.Collections.Generic;
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

    public void TakeDamage(int recievingDamage)
    {
        this.Health -= recievingDamage;

        if (this.Health <= 0)
        {
            // Destroy this and give player currency.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
