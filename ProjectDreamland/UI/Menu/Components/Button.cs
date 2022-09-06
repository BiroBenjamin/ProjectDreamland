using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProjectDreamland.UI.Menu.Components
{
  public class Button : BaseUI
  {
    public MouseState _currentMouseState;
    public MouseState _PreviousMouseState;
    private bool _isHovered = false;
    private readonly Action _onClick;
    
    public Button(string titleText, Texture2D titleImage, Rectangle backgroundBounds, bool isOnLeftSide, Action method) :
      base(titleText, titleImage, backgroundBounds, isOnLeftSide)
    {
      _onClick = method;
    }

    public override void Update(GameTime gameTime)
    {
      _currentMouseState = Mouse.GetState();

      if (_currentMouseState.X >= _imageBounds.X && _currentMouseState.X < _imageBounds.X + _imageBounds.Width &&
        _currentMouseState.Y >= _imageBounds.Y && _currentMouseState.Y < _imageBounds.Y + _imageBounds.Height)
      {
        _isHovered = true;
      }
      else
      {
        _isHovered = false;
      }
      if(_currentMouseState.LeftButton == ButtonState.Pressed && _PreviousMouseState.LeftButton == ButtonState.Released)
      {
        _onClick();
      }

      _PreviousMouseState = _currentMouseState;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      base.Draw(gameTime, spriteBatch, content);
      if (_isHovered)
      {
        spriteBatch.Draw(_image, _imageBounds, Color.Brown);
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts/Ubuntu32"), _text,
          new Vector2(_imageBounds.X + _imageBounds.Width / 4, _imageBounds.Y + _imageBounds.Height / 4), Color.Black);
      }
    }

  }
}
