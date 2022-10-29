using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;

namespace ProjectDreamland.UI
{
    public class ExperienceBar
  {
    public Rectangle BaseBounds { get; set; }
    public Rectangle LevelBounds { get; set; }

    private SpriteFont _font;
    private Texture2D _baseTexture;
    private Texture2D _expTexture;
    private Texture2D _levelTexture;
    private int _level;
    private int _expBarCurrentWidth;

    public ExperienceBar(Rectangle bounds)
    {
      _font = Fonts.ArialBig;
      BaseBounds = new Rectangle(bounds.X + 50, bounds.Y + 35, bounds.Width - 50, 15);
      LevelBounds = new Rectangle(bounds.X, bounds.Y, 50, bounds.Height);
    }
    public void Update(GameTime gameTime, float maxExp, float currentExp, int level)
    {
      if (maxExp <= 0) return;
      _expBarCurrentWidth = (int)MathHelper.Lerp(0, BaseBounds.Width, currentExp / maxExp);
      _level = level;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float layerDepth)
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
      spriteBatch.Draw(_baseTexture, BaseBounds, null, Color.Black, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
      spriteBatch.Draw(_expTexture, new Rectangle(BaseBounds.X, BaseBounds.Y, _expBarCurrentWidth, BaseBounds.Height), null,
        Color.CadetBlue, 0f, Vector2.Zero, SpriteEffects.None, layerDepth + .01f);
      spriteBatch.Draw(_levelTexture, LevelBounds, null, Color.Gray, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
      Vector2 fontSize = _font.MeasureString(_level.ToString());
      spriteBatch.DrawString(_font, $"{_level}", new Vector2(LevelBounds.X + fontSize.X, LevelBounds.Y + (fontSize.Y / 2)), Color.White, 
        0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + .01f);
    }
  }
}
