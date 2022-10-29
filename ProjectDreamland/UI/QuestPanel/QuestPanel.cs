using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Components;

namespace ProjectDreamland.UI.QuestPanel
{
  internal class QuestPanel : BaseUI
  {
    private SpriteFont _font;

    public QuestPanel(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) : base(graphicsDevice, bounds, color)
    {
      _font = Fonts.ArialSmall;
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      QuestManager.Update(gameTime, Bounds);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      base.Draw(gameTime, spriteBatch, content, layerDepth - .02f);
      QuestManager.Draw(gameTime, spriteBatch, _font, layerDepth - .01f);
    }
  }
}
