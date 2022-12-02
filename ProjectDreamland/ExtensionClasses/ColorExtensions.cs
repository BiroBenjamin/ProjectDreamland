using Microsoft.Xna.Framework;
using System;

namespace ProjectDreamland.ExtensionClasses
{
  public static class ColorExtensions
  {
    public static Color Invert(this Color color)
    {
      return new Color(255 - color.R, 255 - color.G, 255 - color.B, color.A);
    }
  }
}
