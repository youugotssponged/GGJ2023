using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : IPlayer
{
    public int Currency { get; set; } = 100;
    public int ExperienceLevel { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Health { get; set; } = 100;

    public void TakeDamage(int recievingDamage)
    {
        throw new System.NotImplementedException();
    }
}
