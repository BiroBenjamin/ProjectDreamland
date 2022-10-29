using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Items;
using System;

namespace ProjectDreamland.Managers
{
  public static class EquipmentManager
  {
    public static Armor HeadSlot { get; set; }
    public static Armor ChestSlot { get; set; }
    public static Armor GlovesSlot { get; set; }
    public static Armor PantsSlot { get; set; }
    public static Armor BootsSlot { get; set; }
    public static Weapon WeaponSlot { get; set; }

    public static Item SwapEquipment(Item equipment)
    {
      Item swapWith = null;
      switch (equipment.Type)
      {
        case ItemTypesEnum.Head:
          swapWith = HeadSlot;
          HeadSlot = (equipment as Armor);
          break;
        case ItemTypesEnum.Chest:
          swapWith = ChestSlot;
          ChestSlot = (equipment as Armor);
          break;
        case ItemTypesEnum.Gloves:
          swapWith = GlovesSlot;
          GlovesSlot = (equipment as Armor);
          break;
        case ItemTypesEnum.Pants:
          swapWith = PantsSlot;
          PantsSlot = (equipment as Armor);
          break;
        case ItemTypesEnum.Boots:
          swapWith = BootsSlot;
          BootsSlot = (equipment as Armor);
          break;
        case ItemTypesEnum.Weapon:
          swapWith = WeaponSlot;
          WeaponSlot = (equipment as Weapon);
          break;
      }
      return swapWith;
    }
    public static Item TakeEquipment(ItemTypesEnum equipmentType)
    {
      Item taken = null;
      switch (equipmentType)
      {
        case ItemTypesEnum.Head:
          taken = HeadSlot;
          HeadSlot = null;
          break;
        case ItemTypesEnum.Chest:
          taken = ChestSlot;
          ChestSlot = null;
          break;
        case ItemTypesEnum.Gloves:
          taken = GlovesSlot;
          GlovesSlot = null;
          break;
        case ItemTypesEnum.Pants:
          taken = PantsSlot;
          PantsSlot = null;
          break;
        case ItemTypesEnum.Boots:
          taken = BootsSlot;
          BootsSlot = null;
          break;
        case ItemTypesEnum.Weapon:
          taken = WeaponSlot;
          WeaponSlot = null;
          break;
      }
      return taken;
    }

    public static Stats AddStats()
    {
      Stats result = new Stats();
      result = HeadSlot != null ? result.AddStats(HeadSlot.Stats) : result;
      result = ChestSlot != null ? result.AddStats(ChestSlot.Stats) : result;
      result = GlovesSlot != null ? result.AddStats(GlovesSlot.Stats) : result;
      result = PantsSlot != null ? result.AddStats(PantsSlot.Stats) : result;
      result = BootsSlot != null ? result.AddStats(BootsSlot.Stats) : result;
      result = WeaponSlot != null ? result.AddStats(WeaponSlot.Stats) : result;
      return result;
    }
  }
}
