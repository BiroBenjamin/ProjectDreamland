using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Quests;
using System;
using System.Collections.Generic;
using ProjectDreamland.Data.GameFiles.Characters;

namespace ProjectDreamland.Managers
{
  public static class QuestManager
  {
    public static List<Quest> Quests { get; set; } = new List<Quest>();

    public static void Initialize()
    {
      Quests.AddRange(
        new List<Quest>() {
          new Quest("testQuest001", "Monke dead", "You have to kill these monkes!!", "kill", 520, new Objective("gorilla_front", 1))
        }
      );
    }

    public static Quest GetRandomQuest()
    {
      if (Quests.Count <= 0) return null;
      Random random = new Random();
      return Quests[random.Next(0, Quests.Count)];
    }

    public static void Update(GameTime gameTime, Player player, Rectangle panelBounds)
    {
      int y = (int)(panelBounds.Y + panelBounds.Height * .05f);
      foreach (Quest quest in player.Quests)
      {
        quest.Update(gameTime, panelBounds, y);
        y += quest.Bounds.Height;
      }
    }
    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SpriteFont font, Player player)
    {
      foreach (Quest quest in Quests)
      {
        quest.Draw(gameTime, spriteBatch, graphicsDevice, font);
      }
    }
  }
}
