using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Items;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Components;

namespace ProjectDreamland.UI.Character
{
  public class ItemSlot : BaseUI
  {
    public ItemTypesEnum ItemType { get; set; }
    public Texture2D BaseTexture { get; set; }
    public Tooltip BaseTooltip { get; set; }
    public Tooltip Tooltip { get; set; }
    public Button EquipmentButton { get; set; }

    public ItemSlot(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) :
      base(graphicsDevice, bounds, color)
    {
      MouseEventHandler.OnRightClick += () =>
      {
        Item item = EquipmentManager.TakeEquipment(ItemType);
        InventoryManager.AddItem(item);
        Tooltip = BaseTooltip;
        EquipmentButton = null;
      };
    }

    public void SetEvents()
    {
      MouseEventHandler.OnMouseEnter += () => { if (Tooltip != null) Tooltip.IsShown = true; };
      MouseEventHandler.OnMouseLeave += () => { if (Tooltip != null) Tooltip.IsShown = false; };
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      SetEquipmentTexture();
      if (EquipmentButton != null) EquipmentButton.Update(gameTime);
      if (Tooltip != null) Tooltip.Update(gameTime);
    }
    private void SetEquipmentTexture()
    {
      Item item = GetItem();
      SetTexture(item);
    }
    private Item GetItem()
    {
      Item item = null;
      switch (ItemType)
      {
        case ItemTypesEnum.Head:
          item = EquipmentManager.HeadSlot;
          break;
        case ItemTypesEnum.Chest:
          item = EquipmentManager.ChestSlot;
          break;
        case ItemTypesEnum.Gloves:
          item = EquipmentManager.GlovesSlot;
          break;
        case ItemTypesEnum.Pants:
          item = EquipmentManager.PantsSlot;
          break;
        case ItemTypesEnum.Boots:
          item = EquipmentManager.BootsSlot;
          break;
        case ItemTypesEnum.Weapon:
          item = EquipmentManager.WeaponSlot;
          break;
      }
      return item;
    }
    private void SetTexture(Item item)
    {
      if (item != null)
      {
        EquipmentButton = new Button(_graphicsDevice, Bounds, Color.White, Color.Gray, Fonts.ArialBig, texture: item.Texture);
        EquipmentButton.Tooltip = new Tooltip(_graphicsDevice, Vector2.Zero, item.ToString());
        Tooltip = null;
        ResetTexture();
      }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      //base.Draw(gameTime, spriteBatch, content, layerDepth);
      spriteBatch.Draw(Texture, Bounds, null, _color, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
      if (EquipmentButton == null) 
        spriteBatch.Draw(BaseTexture, Bounds, null, _color, 0f, Vector2.Zero, SpriteEffects.None, layerDepth + .01f);
      if (EquipmentButton != null) 
        EquipmentButton.Draw(gameTime, spriteBatch, content, layerDepth + .02f);
      if (Tooltip != null) 
        Tooltip.Draw(gameTime, spriteBatch, content);
    }
  }
}
