using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;

namespace ProjectDreamland.Data.GameFiles.Items
{
  public class Weapon : Item
  {
    public Stats Stats { get; set; }

    public Weapon(string id, string name, ItemTypesEnum weaponType, Texture2D texture, Stats stats) : 
      base(id, name, weaponType, texture, true)
    {
      Stats = stats;
    }

    public override string ToString()
    {
      return $"{Name}\n\n{Stats}";
    }
  }
}
