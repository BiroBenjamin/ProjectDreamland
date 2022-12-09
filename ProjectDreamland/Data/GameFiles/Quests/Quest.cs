using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Items;
using ProjectDreamland.Managers;
using System;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Quests
{
  public class Quest
  {
    public string ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Objective Objective { get; set; }
    public int RewardExp { get; set; }
    public Item RewardItem { get; set; }
    //public BaseCharacter QuestGiver { get; set; }
    public Rectangle Bounds { get; set; }
    public bool IsDone { get; set; }
    public bool IsAccepted { get; set; }

    public Quest(string id, string title, string description, string type, int rewardExp, Objective objective, Item rewardItem = null)
    {
      if (QuestManager.Quests.Any(x => x.ID == id))
      {
        throw new Exception("The given ID already exists!");
      }
      ID = id;
      Title = title;
      Description = description;
      Type = type;
      RewardExp = rewardExp;
      Objective = objective;
      if (rewardItem != null) RewardItem = rewardItem;
    }

    public void Update(GameTime gameTime, Rectangle panelBounds, int y)
    {
      int width = (int)(panelBounds.Width * .8f);
      int height = (int)(panelBounds.Height * .2f);
      int x = (int)(panelBounds.X + panelBounds.Width * .1f);
      Bounds = new Rectangle(x, y, width, height);
      Objective.Update(gameTime, Type);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont font, float layerDepth)
    {
      spriteBatch.DrawString(font, $"{Title}:", new Vector2(Bounds.X, Bounds.Y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None,
        layerDepth);
      spriteBatch.DrawString(font, $"{Objective.Description}", new Vector2(Bounds.X, Bounds.Y + 20), Color.White, 0f, Vector2.Zero, 1f,
        SpriteEffects.None, layerDepth);
    }
  }
}
