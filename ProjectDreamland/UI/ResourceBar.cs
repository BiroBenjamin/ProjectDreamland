using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.ExtensionClasses;

namespace ProjectDreamland.UI
{
  public class ResourceBar
  {
    private Texture2D _baseTexture;
    private Texture2D resourceTexture;
    private int _resourceBarMaxWidth;
    private int _resourceBarCurrentWidth;
    private Color _color;

    public ResourceBar(string resourceType)
    {
      switch (resourceType)
      {
        case "None":
          _color = Color.Gray;
          break;
        case "Mana":
          _color = Color.CornflowerBlue;
          break;
        case "Rage":
          _color = Color.DarkOrange;
          break;
        case "Energy":
          _color = Color.Yellow;
          break;
      }
    }

    public void Update(GameTime gameTime, float max, float current)
    {
      if (max <= 0) return;
      _resourceBarMaxWidth = 100;
      _resourceBarCurrentWidth = (int)MathHelper.Lerp(0, _resourceBarMaxWidth, current / max);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 position)
    {
      if (_baseTexture == null || resourceTexture == null)
      {
        _baseTexture = new Texture2D(graphicsDevice, 1, 1);
        _baseTexture.SetData(new Color[] { Color.Black });

        resourceTexture = new Texture2D(graphicsDevice, 1, 1);
        resourceTexture.SetData(new Color[] { _color });
      }
      int x = (int)(position.X - _resourceBarMaxWidth / 2);
      int y = (int)(position.Y - 5);
      spriteBatch.Draw(_baseTexture, new Rectangle(x, y, _resourceBarMaxWidth, 5), Color.Black);
      spriteBatch.Draw(resourceTexture, new Rectangle(x, y, _resourceBarCurrentWidth, 5), _color);
    }
  }
}
