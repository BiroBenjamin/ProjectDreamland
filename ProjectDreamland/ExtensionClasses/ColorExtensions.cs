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

    public static Color BlackOrWhite(this Color color)
    {
      color.Deconstruct(out byte R, out byte G, out byte B, out byte A);
      if(R > 125 && G > 125 && B > 125)
      {
        return Color.Black;
      }
      return Color.White;
    }
  }
}
