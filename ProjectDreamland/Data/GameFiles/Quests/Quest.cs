using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;

namespace ProjectDreamland.Data.GameFiles.Quests
{
  public class Quest
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Objective Objective { get; set; }
    public int RewardExp { get; set; }
    public BaseCharacter QuestGiver { get; set; }
    public Rectangle Bounds { get; set; }
    public bool IsDone { get; set; }

    public Quest(string title, string description, string type, int rewardExp, Objective objective)
    {
      Title = title;
      Description = description;
      Type = type;
      RewardExp = rewardExp;
      Objective = objective;
    }

    public void Update(GameTime gameTime, Rectangle panelBounds, int y)
    {
      if (Objective.IsDone) IsDone = true;
      int width = (int)(panelBounds.Width * .8f);
      int height = (int)(panelBounds.Height * .1f);
      int x = (int)(panelBounds.X + panelBounds.Width * .1f);
      Bounds = new Rectangle(x, y, width, height);
      Objective.Update(gameTime);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SpriteFont font)
    {
      spriteBatch.DrawString(font, $"{Title}:", new Vector2(Bounds.X, Bounds.Y), Color.White);
      spriteBatch.DrawString(font, $"{Objective.Description}", new Vector2(Bounds.X, Bounds.Y + 15), Color.White);
    }
  }
}
