using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.Data.Constants
{
    public static class Fonts
    {
        public static SpriteFont ArialBig { get; set; } = Game1.Self.Content.Load<SpriteFont>("Fonts/ArialBig");
        public static SpriteFont ArialSmall { get; set; } = Game1.Self.Content.Load<SpriteFont>("Fonts/ArialSmall");
    }
}
