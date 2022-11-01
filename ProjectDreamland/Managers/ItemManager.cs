using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Items;
using System.Collections.Generic;

namespace ProjectDreamland.Managers
{
  public static class ItemManager
  {
    private static ContentManager _contentManager = Game1.Self.Content;

    public static List<Item> Items = new List<Item>()
    {
      new Item("apple_001", "Apple", ItemTypesEnum.Quest, 
        _contentManager.Load<Texture2D>("Sprites/Items/apple_icon"), false),
    };
  }
}
