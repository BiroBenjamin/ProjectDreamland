namespace ProjectDreamland.Data.GameFiles
{
  public class Stats
  {
    public (int, int) AttackDamage { get; set; }
    public int HealthPoints { get; set; }
    public int ManaPoints { get; set; }
    public int ManaInterval { get; set; }

    public Stats((int, int)? attackDamage = null, int healtPoints = 0, int manaPoints = 0, int manaInterval = 0)
    {
      AttackDamage = attackDamage == null ? (0, 0) : attackDamage.Value;
      HealthPoints = healtPoints;
      ManaPoints = manaPoints;
      ManaInterval = manaInterval;
    }

    public Stats AddStats(params Stats[] stats)
    {
      Stats result = this;
      foreach(Stats stat in stats)
      {
        result = new Stats((result.AttackDamage.Item1 + stat.AttackDamage.Item1, result.AttackDamage.Item2 + stat.AttackDamage.Item2),
        result.HealthPoints + stat.HealthPoints, result.ManaPoints + stat.ManaPoints, result.ManaInterval + stat.ManaInterval);
      }
      return result;
    }

    public override string ToString()
    {
      return $"{(AttackDamage.Item1 == 0 && AttackDamage.Item2 == 0 ? "" : $"Attack Damage: {AttackDamage.Item1} - {AttackDamage.Item2}")}\n" +
        $"{(HealthPoints == 0 ? "" : $"Bonus Health: {HealthPoints}")}\n" +
        $"{(ManaPoints == 0 ? "" : $"Bonus Mana: {ManaPoints}")}\n" +
        $"{(ManaInterval == 0 ? "" : $"Bonus Mana Regen: {ManaInterval}")}\n";
    }
  }
}
