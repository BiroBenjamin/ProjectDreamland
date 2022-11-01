using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;

namespace ProjectDreamland.Data.GameFiles.Items
{
  public class Armor : Item
  {
    public Stats Stats { get; set; }

    public Armor(string id, string name, ItemTypesEnum itemType, Texture2D texture, Stats stats) : 
      base(id, name, itemType, texture, true)
    {
      Stats = stats;
    }

    public override string ToString()
    {
      return $"{Name}\n\n{Stats}";
    }
  }
}
