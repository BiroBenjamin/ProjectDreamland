using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data;
using ProjectDreamland.UI.Menu.Components;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.UI.Menu.OptionsMenu
{
  public class OptionsMenuPanel : MenuPanelBase
  {
    private PauseMenu _pauseMenu;
    private SettingsMenu _settingsMenu;

    public OptionsMenuPanel(ContentManager content, Rectangle backgroundBounds, bool isOnLeftSide) :
      base(content)
    {
      _pauseMenu = new PauseMenu(backgroundBounds, _buttonIcon, _titleIcon);
      _settingsMenu = new SettingsMenu(backgroundBounds, _buttonIcon, _titleIcon);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      _pauseMenu.Update(gameTime);
      _settingsMenu.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(gameTime, spriteBatch);
      _pauseMenu.Draw(gameTime, spriteBatch, _content);
      _settingsMenu.Draw(gameTime, spriteBatch, _content);
    }

  }
}
