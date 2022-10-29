using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.ExtensionClasses
{
  public static class Texture2DExtensions
  {
    public static Texture2D GetBaseTexture(this Texture2D thisTexture2D, GraphicsDevice graphicsDevice, Color color)
    {
      Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
      texture.SetData(new Color[] { color });
      return texture;
    }
  }
}
