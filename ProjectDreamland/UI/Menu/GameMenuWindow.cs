using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Components;
using System.Collections.Generic;

namespace ProjectDreamland.UI.Menu
{
    public class GameMenuWindow : BaseUI
  {
    public static bool IsShown { get; set; } = false;

    private SpriteFont _font;
    private Vector2 _titlePosition;
    private string _title;

    private List<Button> _buttons;

    public GameMenuWindow(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) :
      base(graphicsDevice, bounds, color)
    {
      _font = Fonts.ArialBig;
      _title = "Pause";
      _titlePosition = new Vector2(
        Bounds.X + Bounds.Width / 2 - _font.MeasureString(_title).X * .5f, 
        Bounds.Y + 20 - _font.MeasureString(_title).Y * .2f);

      AddButtons(graphicsDevice);
      Bounds = new Rectangle(
        Bounds.X, 
        Bounds.Y, 
        Bounds.Width,
        (_buttons[0].Bounds.Height + 70) * _buttons.Count);
    }
    private void AddButtons(GraphicsDevice graphicsDevice)
    {
      _buttons = new List<Button>();
      int buttonY = (int)_font.MeasureString(_title).Y + Bounds.Y + 50;
      for (int i = 0; i < 2; i++)
      {
        Rectangle rect = new Rectangle(
          (int)(Bounds.X + Bounds.Width / 2 - (Bounds.Width * .325f)),
          buttonY,
          (int)(Bounds.Width * .65f),
          50);
        _buttons.Add(new Button(graphicsDevice, rect, Color.BlanchedAlmond * .8f, Color.Black * .8f, _font));
        buttonY += 70;
      }
      _buttons[0].Text = "Resume";
      _buttons[0].MouseEventHandler.OnLeftClick += Actions.EscPressed;
      _buttons[1].Text = "Quit";
      _buttons[1].MouseEventHandler.OnLeftClick += Actions.ExitApplication;
    }

    public override void Update(GameTime gameTime)
    {
      foreach (Button button in _buttons)
      {
        if (IsShown)
        {
          button.Update(gameTime);
        }
      }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDetph)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content, layerDetph);
      spriteBatch.DrawString(_font, _title, _titlePosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDetph + .01f);
      foreach (Button button in _buttons)
      {
        button.Draw(gameTime, spriteBatch, content, layerDetph + .01f);
      }
    }
  }
}
