using UnityEngine;

public class FakePlayer : MonoBehaviour, IPlayer
{
    public int Currency { get; set; } = 1000;
    public int ExperienceLevel { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Health { get; set; } = 100;

    public void GainCurrency(int receivedCurrency)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int recievingDamage)
    {
        throw new System.NotImplementedException();
    }
}
