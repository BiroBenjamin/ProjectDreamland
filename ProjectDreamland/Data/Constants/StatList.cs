using ProjectDreamland.Data.GameFiles;

namespace ProjectDreamland.Data.Constants
{
  public static class StatList
  {
    /// <summary>
    /// Stat with 3 - 6 bonus attack damage
    /// </summary>
    public static readonly Stats Stat1 = new Stats(attackDamage: (3, 6));
    /// <summary>
    /// Stat with 45 bonus health points
    /// </summary>
    public static readonly Stats Stat2 = new Stats(healtPoints: 45);
    /// <summary>
    /// Stat with 68 bonus mana points and 10 bonus mana per 5 seconds
    /// </summary>
    public static readonly Stats Stat3 = new Stats(manaPoints: 68, manaInterval: 10);
    /// <summary>
    /// Stat with 25 bonus health points and 10 bonus mana points
    /// </summary>
    public static readonly Stats Stat4 = new Stats(healtPoints: 25, manaPoints: 10);
    /// <summary>
    /// Stat with 2 - 3 bonus attack damage, 7 bonus mana per 5 seconds and 25 bonus mana points
    /// </summary>
    public static readonly Stats Stat5 = new Stats(attackDamage: (2, 3), manaInterval: 7, manaPoints: 25);
  }
}
