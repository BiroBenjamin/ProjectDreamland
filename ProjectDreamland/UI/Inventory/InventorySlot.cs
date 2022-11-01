using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Items;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Components;

namespace ProjectDreamland.UI.Inventory
{
  public class InventorySlot : BaseUI
  {
    public Button Slot { get; set; }
    public int SlotIndex { get; set; }

    public InventorySlot(GraphicsDevice graphicsDevice, Rectangle bounds, Color color, int slotIndex) : base(graphicsDevice, bounds, color)
    {
      SlotIndex = slotIndex;
      Slot = new Button(graphicsDevice, bounds, Color.Gray, Color.White, Fonts.ArialSmall);
      Slot.MouseEventHandler.OnRightClick += () =>
      {
        if (SlotIndex >= InventoryManager.Items.Count) return;
        Item item = InventoryManager.Items[SlotIndex];
        if (item == null || !item.IsEquipable) return;
        switch (item.GetType() == typeof(Armor) ? (item as Armor).Type : (item as Weapon).Type)
        {
          case ItemTypesEnum.Head:
            InventoryManager.Items[SlotIndex] = EquipmentManager.SwapEquipment(item);
            break;
          case ItemTypesEnum.Chest:
            InventoryManager.Items[SlotIndex] = EquipmentManager.SwapEquipment(item);
            break;
          case ItemTypesEnum.Gloves:
            InventoryManager.Items[SlotIndex] = EquipmentManager.SwapEquipment(item);
            break;
          case ItemTypesEnum.Pants:
            InventoryManager.Items[SlotIndex] = EquipmentManager.SwapEquipment(item);
            break;
          case ItemTypesEnum.Boots:
            InventoryManager.Items[SlotIndex] = EquipmentManager.SwapEquipment(item);
            break;
          case ItemTypesEnum.Weapon:
            InventoryManager.Items[SlotIndex] = EquipmentManager.SwapEquipment(item);
            break;
        }
        if (InventoryManager.Items[SlotIndex] == null) 
        {
          Slot.ResetTexture();
        }
      };
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (SlotIndex < InventoryManager.Items.Count)
      {
        Item item = InventoryManager.Items[SlotIndex];
        if(item != null)
        {
          Slot.Texture = item.Texture;
          Slot.Tooltip = new Tooltip(_graphicsDevice, Vector2.Zero, item.ToString());
        }
        else
        {
          Slot.ResetTexture();
        }
      }
      else
      {
        Slot.Texture = null;
        Slot.Tooltip = null;
      }
      Slot.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      base.Draw(gameTime, spriteBatch, content, layerDepth);
      Slot.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
    }
  }
}
