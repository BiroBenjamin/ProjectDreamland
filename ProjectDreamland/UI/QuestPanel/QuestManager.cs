using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Quests;
using System.Collections.Generic;

namespace ProjectDreamland.UI.QuestPanel
{
  public static class QuestManager
  {
    public static List<Quest> Quests { get; set; } = new List<Quest>();

    public static void Update(GameTime gameTime, Rectangle panelBounds)
    {
      int y = (int)(panelBounds.Y + panelBounds.Height * .05f);
      foreach(Quest quest in Quests)
      {
        quest.Update(gameTime, panelBounds, y);
        y += quest.Bounds.Height;
      }
    }
    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SpriteFont font)
    {
      foreach(Quest quest in Quests)
      {
        quest.Draw(gameTime, spriteBatch, graphicsDevice, font);
      }
    }
  }
}
