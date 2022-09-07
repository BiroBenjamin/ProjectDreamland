using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Handlers;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.UI.Menu.Components
{
  public class Button : BaseUI
  {
    public MouseState _currentMouseState;
    public MouseState _previousMouseState;
    public MouseEventHandler MouseEventHandler { get; set; }

    private bool _isHovered = false;

    public Button(string titleText, Texture2D titleImage, Rectangle backgroundBounds, bool isOnLeftSide) :
      base(titleText, titleImage, backgroundBounds, isOnLeftSide)
    {
      MouseEventHandler = new MouseEventHandler(_imageBounds);
      MouseEventHandler.MouseEnter += () =>
      {
        _isHovered = true;
      };
      MouseEventHandler.MouseLeave+= () =>
      {
        _isHovered = false;
      };
    }

    public override void Update(GameTime gameTime)
    {
      _currentMouseState = Mouse.GetState();

      MouseEventHandler.Update(gameTime, _currentMouseState, _previousMouseState);

      _previousMouseState = _currentMouseState;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      base.Draw(gameTime, spriteBatch, content);
      if (_isHovered)
      {
        spriteBatch.Draw(_image, _imageBounds, Color.Brown);
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts/ArialBig"), _text,
          new Vector2(_imageBounds.X + _imageBounds.Width / 4, _imageBounds.Y + _imageBounds.Height / 4), Color.Black);
      }
    }

  }
}
