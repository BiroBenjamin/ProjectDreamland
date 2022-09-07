using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.UI.Menu.Components;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.UI.Menu
{
  public class MenuBase
  {
    private Rectangle _backgroundBounds;
    private TitleHeader _titleHeader;
    private List<Button> _buttons;

    public MenuBase(Rectangle backgroundBounds)
    {
      _backgroundBounds = backgroundBounds;
      _buttons = new List<Button>();
    }

    protected void CreateTitle(string text, Texture2D titleIcon, bool isOnLeftSide)
    {
      _titleHeader = new TitleHeader(text, titleIcon, _backgroundBounds, isOnLeftSide);
    }
    protected void CreateButton(string text, Action action, Texture2D buttonIcon, bool isOnLeftSide)
    {
      _backgroundBounds.Y += buttonIcon.Height * 2;
      Button button = new Button(text, buttonIcon, _backgroundBounds, isOnLeftSide);
      button.MouseEventHandler.OnClick += action;
      _buttons.Add(button);
    }

    public virtual void Update(GameTime gameTime)
    {
      _titleHeader.Update(gameTime);
      foreach (Button button in _buttons)
      {
        button.Update(gameTime);
      }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      _titleHeader.Draw(gameTime, spriteBatch, content);
      foreach (Button button in _buttons)
      {
        button.Draw(gameTime, spriteBatch, content);
      }
    }
  }
}
