using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.UI.Components;
using ProjectDreamland.ExtensionClasses;
using System.Collections.Generic;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.Enums;
using System;

namespace ProjectDreamland.UI.Character
{
    public class CharacterWindow : BaseUI
  {
    public static bool IsShown { get; set; }

    private Button _closeButton;
    private List<ItemSlot> _itemSlots;
    private int _slotCount = 6;
    private int _spacing;
    private Rectangle _slotBounds;
    private CharacterInfoPanel _infoPanel;

    public CharacterWindow(ContentManager content, GraphicsDevice graphicsDevice, Rectangle bounds, Color color, Player player) :
      base(graphicsDevice, bounds, color)
    {
      Rectangle closeButtonBounds = new Rectangle(
        (int)(Bounds.X + Bounds.Width - 35),
        (int)(Bounds.Y),
        (int)(35),
        (int)(35));
      _closeButton = new Button(graphicsDevice, closeButtonBounds, Color.White, Color.Gray, Fonts.ArialBig, text: "X");
      _closeButton.MouseEventHandler.OnLeftClick += () => { IsShown = false; };

      _itemSlots = new List<ItemSlot>();
      _spacing = (int)(bounds.Height * .045f);
      _slotBounds = new Rectangle(
        bounds.X + 15,
        bounds.Y + 65,
        (int)(bounds.Height * .1f),
        (int)(bounds.Height * .1f));
      Rectangle infoPanelBounds = new Rectangle(
        Bounds.X + _slotBounds.Width + 30,
        _slotBounds.Y,
        Bounds.Width - _slotBounds.Width - 45,
        (_slotBounds.Height + _spacing) * 6 - _spacing);
      _infoPanel = new CharacterInfoPanel(graphicsDevice, infoPanelBounds, Color.Black * .8f, player);
      for (int i = 0; i < _slotCount; i++)
      {
        _itemSlots.Add(new ItemSlot(graphicsDevice, _slotBounds, Color.Gray * .8f));
        _slotBounds = new Rectangle(
          _slotBounds.X,
          _slotBounds.Y + _slotBounds.Height + _spacing,
          _slotBounds.Width,
          _slotBounds.Height);
      }
      SetSlotTextures(content, graphicsDevice);
    }

    private void SetSlotTextures(ContentManager content, GraphicsDevice graphicsDevice)
    {
      List<string> textures = new List<string>() 
        { "helmet_icon", "chestplate_icon", "gloves_icon", "pants_icon", "boots_icon", "weapon_icon" };
      
      for(int i = 0; i < _slotCount; i++)
      {
        _itemSlots[i].ItemType = (ItemTypesEnum)Enum.GetValues(typeof(ItemTypesEnum)).GetValue(i);
        _itemSlots[i].BaseTexture = content.Load<Texture2D>($"UI/CharacterPanel/{textures[i]}");
        _itemSlots[i].BaseTooltip = new Tooltip(graphicsDevice, new Vector2(100, 100), $"{textures[i].Split('_')[0].ToTitleCase()}");
        _itemSlots[i].Tooltip = new Tooltip(graphicsDevice, new Vector2(100, 100), $"{textures[i].Split('_')[0].ToTitleCase()}");
        _itemSlots[i].SetEvents();
      }
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsShown) return;
      base.Update(gameTime);
      _closeButton.Update(gameTime);
      _infoPanel.Update(gameTime);
      foreach (ItemSlot slot in _itemSlots)
      {
        slot.Update(gameTime);
      }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content, layerDepth);
      _closeButton.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
      _infoPanel.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
      foreach (ItemSlot slot in _itemSlots)
      {
        slot.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
      }
    }
  }
}
