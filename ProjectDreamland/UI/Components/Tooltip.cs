using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Constants;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.UI.Components
{
    public class Tooltip : BaseUI
  {
    public bool IsShown { get; set; }

    private MouseState _mouseState;

    private List<string> _splitText;
    private Vector2 _newLinePosition;
    private int _width = 250, _height, _spacing = 5;

    public Tooltip(GraphicsDevice graphicsDevice, Vector2 Size, string text = "") : 
      base(graphicsDevice, new Rectangle(0, 0, (int)Size.X, (int)Size.Y), Color.Gray * .9f)
    {
      _splitText = text.Split("\\n").ToList();
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsShown) return;
      _height = 10;
      foreach(string text in _splitText)
      {
        _height += (int)Fonts.ArialSmall.MeasureString(text).Y + _spacing;
      }
      if (_splitText.Count == 1) _width = (int)Fonts.ArialSmall.MeasureString(_splitText[0]).X + 20;
      Bounds = new Rectangle(Bounds.X, Bounds.Y, _width, _height);
      SetPosition();
      base.Update(gameTime);
    }
    private void SetPosition()
    {
      int x, y;
      _mouseState = Mouse.GetState();

      if (_mouseState.X + Bounds.Width + 15 > Game1.ScreenWidth)
      {
        x = _mouseState.X - Bounds.Width - 15;
      }
      else
      {
        x = _mouseState.X + 15;
      }
      if (_mouseState.Y + Bounds.Height + 15 > Game1.ScreenHeight)
      {
        y = _mouseState.Y - Bounds.Height - 15;
      }
      else
      {
        y = _mouseState.Y + 15;
      }
      Bounds = new Rectangle(x, y, Bounds.Width, Bounds.Height);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDetph = .9f)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content, layerDetph);
      _newLinePosition = new Vector2(Bounds.X + 10, Bounds.Y + 10);
      foreach (string text in _splitText)
      {
        spriteBatch.DrawString(Fonts.ArialSmall, text, _newLinePosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 
          layerDetph + .01f);
        _newLinePosition.Y += Fonts.ArialSmall.MeasureString(text).Y + 5;
      }
    }
  }
}
