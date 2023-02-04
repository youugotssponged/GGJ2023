public interface IPlayer : IDamageable
{
    public int Currency { get; set; }
    public int ExperienceLevel { get; set; }

    public void GainCurrency(int receivedCurrency);
}
