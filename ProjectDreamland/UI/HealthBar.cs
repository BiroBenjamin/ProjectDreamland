using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.UI
{
  public class HealthBar
  {
    private Texture2D _baseTexture;
    private Texture2D _healthTexture;
    private int _healthBarMaxWidth;
    private int _healthBarCurrentWidth;

    public HealthBar()
    {
    }

    public void Update(GameTime gameTime, float max, float current)
    {
      if (max <= 0) return;
      _healthBarMaxWidth = 100;
      _healthBarCurrentWidth = (int)MathHelper.Lerp(0, _healthBarMaxWidth, current / max);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int x, int y, Color color)
    {
      if (_baseTexture == null || _healthTexture == null)
      {
        _baseTexture = new Texture2D(graphicsDevice, 1, 1);
        _baseTexture.SetData(new Color[] { Color.Black });

        _healthTexture = new Texture2D(graphicsDevice, 1, 1);
        _healthTexture.SetData(new Color[] { color });
      }
      spriteBatch.Draw(_baseTexture, new Rectangle(x, y, _healthBarMaxWidth, 5), Color.Black);
      spriteBatch.Draw(_healthTexture, new Rectangle(x, y, _healthBarCurrentWidth, 5), color);
    }
  }
}
