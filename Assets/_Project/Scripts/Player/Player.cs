using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public int Currency { get; set; } = 0;
    public int ExperienceLevel { get; set; } = 1;
    public int Health { get; set; } = 25;
    private int MaxHealth { get; } = 25;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int receivingDamage)
    {
        if (Health > 0 && receivingDamage > 0)
        {
            Health -= receivingDamage;
            UpdateBaseColour();
        }
    }

    private void UpdateBaseColour()
    {
        float healthPercentage = ((float)Health / (float)MaxHealth);
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(1, healthPercentage, healthPercentage);
    }

    public void GainExperienceLevel(int receivedExperienceLevel)
    {
        ExperienceLevel += receivedExperienceLevel;
    }


    public void GainCurrency(int receivedCurrency)
    {
        Currency += receivedCurrency;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.AttackDamage);
            enemy.DestroyEnemy();
        }
    }

}
