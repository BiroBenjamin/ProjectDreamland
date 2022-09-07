using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Managers;
using System;

namespace ProjectDreamland.Handlers
{
  public class MouseEventHandler
  {
    public Action OnClick { get; set; }
    public Action MouseEnter { get; set; }
    public Action MouseLeave { get; set; }

    private Rectangle _objectBounds;

    public MouseEventHandler(Rectangle objectBounds)
    {
      _objectBounds = objectBounds;
    }

    private void ActionOnClick(MouseState currentMouseState, MouseState previousMouseState)
    {
      if (currentMouseState.X >= _objectBounds.X && currentMouseState.X < _objectBounds.X + _objectBounds.Width &&
        currentMouseState.Y >= _objectBounds.Y && currentMouseState.Y < _objectBounds.Y + _objectBounds.Height &&
        currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
      {
        OnClick?.Invoke();
      }
    }
    private void ActionOnMouseHover(MouseState currentMouseState)
    {
      if (currentMouseState.X >= _objectBounds.X && currentMouseState.X < _objectBounds.X + _objectBounds.Width &&
        currentMouseState.Y >= _objectBounds.Y && currentMouseState.Y < _objectBounds.Y + _objectBounds.Height)
      {
        MouseEnter?.Invoke();
        return;
      }
      MouseLeave?.Invoke();
    }
   
    public void Update(GameTime gameTime, MouseState currentMouseState, MouseState previousMouseState)
    {
      ActionOnClick(currentMouseState, previousMouseState);
      ActionOnMouseHover(currentMouseState);
    }
  }
}
