using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Characters;

namespace ProjectDreamland.Core
{
  public class Camera
  {
    public static Matrix Transform { get; private set; }

    public void Follow(Player target)
    {
      Rectangle targetRect = new Rectangle(target.Position.X, target.Position.Y, target.Size.Width, target.Size.Height);
      Transform = Matrix.CreateTranslation(
        -targetRect.X - (targetRect.Width / 2),
        -targetRect.Y - (targetRect.Height / 2),
        0) * Matrix.CreateTranslation(
          Game1.ScreenWidth / 2,
          Game1.ScreenHeight / 2,
          0);
    }
  }
}
