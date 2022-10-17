using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.UI.QuestPanel
{
  internal class QuestPanel
  {
    private Texture2D _texture;
    private SpriteFont _font;
    private Rectangle _bounds;
    private float _alpha;

    public QuestPanel(ContentManager content)
    {
      _font = content.Load<SpriteFont>("Fonts/ArialSmall");
      _alpha = .5f;
    }

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
    {
      int width = (int)(screenWidth * .15f);
      int height = (int)(screenHeight * .4f);
      int x = screenWidth - (width + 10);
      int y = (screenHeight - height) - ((screenHeight - height) / 2);
      _bounds = new Rectangle(x, y, width, height );
      QuestManager.Update(gameTime, _bounds);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      if (_texture == null)
      {
        _texture = new Texture2D(graphicsDevice, 1, 1);
        _texture.SetData(new Color[] { Color.Black });
      }
      spriteBatch.Draw(_texture, _bounds, Color.Black * _alpha);
      QuestManager.Draw(gameTime, spriteBatch, graphicsDevice, _font);
    }

  }
}
