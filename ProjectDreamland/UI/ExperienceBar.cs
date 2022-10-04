using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.UI
{
  public class ExperienceBar
  {
    public int ExpBarHeight { get; set; }

    private SpriteFont _font;
    private Texture2D _baseTexture;
    private Texture2D _expTexture;
    private Texture2D _levelTexture;
    private int _level;
    private int _expBarMaxWidth;
    private int _expBarCurrentWidth;

    public ExperienceBar(ContentManager content)
    {
      _font = content.Load<SpriteFont>("Fonts/ArialBig");
    }
    public void Update(GameTime gameTime, float maxExp, float currentExp, int maxWidth, int level)
    {
      if (maxExp <= 0) return;
      _expBarMaxWidth = maxWidth - 50;
      _expBarCurrentWidth = (int)MathHelper.Lerp(0, _expBarMaxWidth, currentExp / maxExp);
      _level = level;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y)
    {
      if (_baseTexture == null || _expTexture == null || _levelTexture == null)
      {
        _baseTexture = new Texture2D(graphicsDevice, 1, 1);
        _baseTexture.SetData(new Color[] { Color.Black });
        _expTexture = new Texture2D(graphicsDevice, 1, 1);
        _expTexture.SetData(new Color[] { Color.CadetBlue });
        _levelTexture = new Texture2D(graphicsDevice, 1, 1);
        _levelTexture.SetData(new Color[] { Color.Gray });
      }
      spriteBatch.Draw(_baseTexture, new Rectangle(x + 50, y - 15, _expBarMaxWidth, 15), Color.Black);
      spriteBatch.Draw(_expTexture, new Rectangle(x + 50, y - 15, _expBarCurrentWidth, 15), Color.CadetBlue);
      spriteBatch.Draw(_levelTexture, new Rectangle(x, y - 50, 50, 50), Color.Gray);
      Vector2 fontSize = _font.MeasureString(_level.ToString());
      spriteBatch.DrawString(_font, $"{_level}", new Vector2(x + fontSize.X, y - 50 + (fontSize.Y / 2)), Color.White);
    }
  }
}
