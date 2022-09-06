using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data;
using ProjectDreamland.UI.Menu.Components;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.UI.Menu.OptionsMenu
{
  public class PauseMenu : MenuBase
  {
    public PauseMenu(Rectangle backgroundBounds, Texture2D buttonIcon, Texture2D titleIcon) :
      base(backgroundBounds)
    {
      CreateTitle("Paused", titleIcon, true);
      CreateButton("Resume", Actions.ToggleMenu, buttonIcon, true);
      CreateButton("Settings", Actions.ToggleSettings, buttonIcon, true);
      CreateButton("Quit", Actions.ExitApplication, buttonIcon, true);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      base.Draw(gameTime, spriteBatch, content);
    }
  }
}
