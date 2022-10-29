using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.UI.Character;
using ProjectDreamland.UI.Components;
using ProjectDreamland.UI.Inventory;
using ProjectDreamland.UI.Menu;
using System.Collections.Generic;

namespace ProjectDreamland.UI.MenuBar
{
    public class MenuBar : BaseUI
  {
    private List<Button> _buttons;

    public MenuBar(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) : base(graphicsDevice, bounds, color)
    {
      _buttons = new List<Button>();
      int x = bounds.X + bounds.Width - bounds.Height;
      int boundsWidth = 5;
      for(int i = 0; i < 3; i++)
      {
        Rectangle buttonBounds = new Rectangle(x, bounds.Y + 5, bounds.Height - 5, bounds.Height - 10);
        _buttons.Add(new Button(graphicsDevice, buttonBounds, Color.White, Color.Black * .5f, Fonts.ArialSmall));
        x -= bounds.Height;
        boundsWidth += buttonBounds.Width + 5;
      }
      Bounds = new Rectangle(Bounds.X + Bounds.Width - boundsWidth, Bounds.Y, boundsWidth, Bounds.Height);
      _buttons[0].Texture = Game1.Self.Content.Load<Texture2D>("UI/menu_icon");
      _buttons[0].Tooltip = new Tooltip(graphicsDevice, Vector2.Zero, $"Menu ({KeyBinds.CloseAndEsc})");
      _buttons[0].MouseEventHandler.OnLeftClick += () => 
      {
        GameMenuWindow.IsShown = !GameMenuWindow.IsShown;
      };
      _buttons[1].Texture = Game1.Self.Content.Load<Texture2D>("UI/bag_icon");
      _buttons[1].Tooltip = new Tooltip(graphicsDevice, Vector2.Zero, $"Inventory ({KeyBinds.Inventory})");
      _buttons[1].MouseEventHandler.OnLeftClick += () => 
      {
        InventoryWindow.IsShown = !InventoryWindow.IsShown;
      };
      _buttons[2].Texture = Game1.Self.Content.Load<Texture2D>("UI/character_panel_icon");
      _buttons[2].Tooltip = new Tooltip(graphicsDevice, Vector2.Zero, $"Character ({KeyBinds.Character})");
      _buttons[2].MouseEventHandler.OnLeftClick += () =>
      {
        CharacterWindow.IsShown = !CharacterWindow.IsShown;
      };
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      foreach (Button button in _buttons)
      {
        button.Update(gameTime);
      }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDetph)
    {
      base.Draw(gameTime, spriteBatch, content, layerDetph);
      foreach (Button button in _buttons)
      {
        button.Draw(gameTime, spriteBatch, content, layerDetph + .01f);
      }
    }
  }
}
