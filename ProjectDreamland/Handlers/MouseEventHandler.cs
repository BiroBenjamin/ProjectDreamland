using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProjectDreamland.Handlers
{
  public class MouseEventHandler
  {
    public Action OnLeftClick { get; set; }
    public Action OnRightClick { get; set; }
    public Action OnMouseEnter { get; set; }
    public Action OnMouseLeave { get; set; }
    public Rectangle Bounds { get; set; }

    private MouseState _currentMouseState;
    private MouseState _lastMouseState;

    public MouseEventHandler(Rectangle objectBounds)
    {
      Bounds = objectBounds;
    }

    private void ActionOnLeftClick()
    {
      if (_currentMouseState.X >= Bounds.X && _currentMouseState.X < Bounds.X + Bounds.Width &&
        _currentMouseState.Y >= Bounds.Y && _currentMouseState.Y < Bounds.Y + Bounds.Height &&
        _currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
      {
        OnLeftClick?.Invoke();
      }
    }
    private void ActionOnRightClick()
    {
      if (_currentMouseState.X >= Bounds.X && _currentMouseState.X < Bounds.X + Bounds.Width &&
        _currentMouseState.Y >= Bounds.Y && _currentMouseState.Y < Bounds.Y + Bounds.Height &&
        _currentMouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released)
      {
        OnRightClick?.Invoke();
      }
    }
    private void ActionOnMouseHover()
    {
      if (_currentMouseState.X >= Bounds.X && _currentMouseState.X < Bounds.X + Bounds.Width &&
        _currentMouseState.Y >= Bounds.Y && _currentMouseState.Y < Bounds.Y + Bounds.Height)
      {
        OnMouseEnter?.Invoke();
        return;
      }
      OnMouseLeave?.Invoke();
    }
   
    public void Update(GameTime gameTime, Rectangle bounds)
    {
      _currentMouseState = Mouse.GetState();

      Bounds = bounds;
      ActionOnLeftClick();
      ActionOnRightClick();
      ActionOnMouseHover();

      _lastMouseState = _currentMouseState;
    }
  }
}
