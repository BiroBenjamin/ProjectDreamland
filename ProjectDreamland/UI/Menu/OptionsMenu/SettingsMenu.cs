using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data;

namespace ProjectDreamland.UI.Menu.OptionsMenu
{
  public class SettingsMenu : MenuBase
  {
    public static bool IsShown { get; set; } = false;

    public SettingsMenu(Rectangle backgroundBounds, Texture2D buttonIcon, Texture2D titleIcon) :
      base(backgroundBounds)
    {
      CreateTitle("Settings", titleIcon, false);
      CreateButton("asd", Actions.Placeholder, buttonIcon, false);
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsShown) return;
      base.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content);
    }
  }
}
