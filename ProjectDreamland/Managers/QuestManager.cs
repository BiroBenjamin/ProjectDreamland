using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Quests;
using System;
using System.Collections.Generic;
using ProjectDreamland.Data.GameFiles.Characters;
using Microsoft.Xna.Framework.Content;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Items;
using System.Linq;

namespace ProjectDreamland.Managers
{
  public static class QuestManager
  {
    private static ContentManager _contentManager = Game1.Self.Content;
    public static List<Quest> Quests { get; set; } = new List<Quest>();

    public static void Initialize()
    {
      Quests.AddRange(
        new List<Quest>() {
          new Quest("testQuest001", "My apple collection", "You need to bring me some apples!", "collect", 350,
            new Objective("apple_001", 5, "collect"),
            new Weapon("weapon_001", "Iron Sword", ItemTypesEnum.Weapon,
            _contentManager.Load<Texture2D>("Sprites/Items/sword_icon"), StatList.Stat1)),
          new Quest("testQuest002", "The gorilla problem", "These gorillas here pose a big problem. I would like to ask you to eliminate some of them.", "kill", 550,
            new Objective("gorilla_front", 2, "kill"),
            new Weapon("weapon_002", "Enchanted Iron Sword", ItemTypesEnum.Weapon,
            _contentManager.Load<Texture2D>("Sprites/Items/sword_icon"), StatList.Stat5)),
          new Quest("testQuest003", "Undead Cleansing", "The skeletons swarmed the place in the valley north of here. People are affraid to collect their apples. Please aid them.", "kill", 625,
            new Objective("skeleton_001", 3, "kill"),
            new Armor("armor_001", "Iron Helmet", ItemTypesEnum.Head,
            _contentManager.Load<Texture2D>("Sprites/Items/helmet_icon"), StatList.Stat2)),
        }
      );
    }

    public static void Update(GameTime gameTime, Rectangle panelBounds)
    {
      int y = (int)(panelBounds.Y + panelBounds.Height * .05f);
      List<Quest> questsToRemove = new List<Quest>();
      foreach (Quest quest in Player.Quests)
      {
        quest.Update(gameTime, panelBounds, y);
        y += quest.Bounds.Height;
        if (quest.IsDone)
        {
          Player.CurrentExperience += quest.RewardExp;
          if (quest.Type == "collect")
          {
            string id = quest.Objective.TargetID;
            Item itemToRemove = ItemManager.Items.Where(x => x.ID == id).FirstOrDefault() as Item;
            for (int i = quest.Objective.Amount - 1; i >= 0; i--)
            {
              InventoryManager.RemoveItem(itemToRemove);
            }
          }
          if (quest.RewardItem != null) InventoryManager.AddItem(quest.RewardItem);
          questsToRemove.Add(quest);
        }
      }
      foreach (Quest quest in questsToRemove)
      {
        quest.Objective.Remaining = quest.Objective.Amount;
        Player.Quests.Remove(quest);
      }
    }
    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont font, float layerDepth)
    {
      foreach (Quest quest in Player.Quests)
      {
        quest.Draw(gameTime, spriteBatch, font, layerDepth);
      }
    }
  }
}
