using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Items;
using System.Collections.Generic;

namespace ProjectDreamland.Managers
{
  public static class InventoryManager
  {
    public static List<Item> Items { get; } = new List<Item>();

    public static void AddItem(Item item)
    {
      int index = Items.IndexOf(null);
      if (index != -1) Items[index] = item;
      else Items.Add(item);
    }

    public static void Update(GameTime gameTime)
    {

    }
  }
}
