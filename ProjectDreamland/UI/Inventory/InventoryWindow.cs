using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Components;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.UI.Inventory
{
  public class InventoryWindow : BaseUI
  {
    public static bool IsShown { get; set; }

    private Button _closeButton;
    private List<InventorySlot> _inventorySlots;
    private readonly int _maxWidth = 6;
    private int _width;
    private int _height;
    private int _remainder;
    private int _spacing = 5;
    private int _headerHeight = 30;

    public InventoryWindow(GraphicsDevice graphicsDevice, Rectangle bounds, Color color, int size) : base(graphicsDevice, bounds, color)
    {
      _inventorySlots = new List<InventorySlot>();
      _width = size > _maxWidth ? _maxWidth : size;
      _height = (int)Math.Floor((double)size / _width);
      _remainder = size - _width * _height;
      int slotSize = 50;
      Bounds = new Rectangle(
        (int)(Game1.ScreenWidth - Game1.ScreenWidth * .3f),
        Game1.ScreenHeight - (35 + (_height + 1) * (slotSize + _spacing)) - 55,
        20 + _width * (slotSize + _spacing),
        35 + (_height + 1) * (slotSize + _spacing));
      Rectangle slotBounds = new Rectangle(
        (int)(Bounds.X + 10),
        (int)(Bounds.Y + _headerHeight + 2),
        (int)(slotSize),
        (int)(slotSize));
      int slotIndex = 0;
      for (int i = 0; i < _height; i++)
      {
        for (int j = 0; j < _width; j++)
        {
          _inventorySlots.Add(new InventorySlot(graphicsDevice, slotBounds, Color.Black * .9f, slotIndex));
          slotBounds = new Rectangle(
            slotBounds.X + slotBounds.Width + _spacing,
            slotBounds.Y,
            slotBounds.Width,
            slotBounds.Height);
          slotIndex++;
        }
        slotBounds = new Rectangle(
          Bounds.X + 10,
          slotBounds.Y + slotBounds.Height + _spacing,
          slotBounds.Width,
          slotBounds.Height);
      }
      for (int i = 0; i < _remainder; i++)
      {
        _inventorySlots.Add(new InventorySlot(graphicsDevice, slotBounds, Color.Gray * .9f, slotIndex));
        slotBounds = new Rectangle(
          slotBounds.X + slotBounds.Width + _spacing,
          slotBounds.Y,
          slotBounds.Width,
          slotBounds.Height);
        slotIndex++;
      }
      Rectangle closeButtonBounds = new Rectangle(
        (int)(Bounds.X + Bounds.Width - _headerHeight),
        (int)(Bounds.Y),
        (int)(_headerHeight),
        (int)(_headerHeight));
      _closeButton = new Button(graphicsDevice, closeButtonBounds, Color.White, Color.Gray, Fonts.ArialBig, text: "X");
      _closeButton.MouseEventHandler.OnLeftClick += () =>
      {
        IsShown = false;
      };
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsShown) return;
      base.Update(gameTime);
      InventoryManager.Update(gameTime);
      foreach (InventorySlot slot in _inventorySlots)
      {
        slot.Update(gameTime);
      }
      _closeButton.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content, layerDepth);
      foreach (InventorySlot slot in _inventorySlots)
      {
        slot.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
      }
      _closeButton.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
    }
  }
}
