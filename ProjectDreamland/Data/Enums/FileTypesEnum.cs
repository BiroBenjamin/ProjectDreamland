using ProjectDreamland.Data.Attributes;
using System.ComponentModel;

namespace ProjectDreamland.Data.Enums
{
  public enum FileTypesEnum
  {
    [Description("Map")]
    Map,
    [Description("Character")]
    Character,
    [Description("World Object")]
    WorldObject,
    [Description("Tile")]
    Tile,
    [Description("Item")]
    Item,
  }
}
