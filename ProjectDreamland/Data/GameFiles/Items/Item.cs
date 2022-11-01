using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;

namespace ProjectDreamland.Data.GameFiles.Items
{
  public class Item : BaseFile
  {
    public Texture2D Texture { get; set; }
    public bool IsEquipable { get; set; }
    public ItemTypesEnum Type { get; set; }

    public Item(string id, string name, ItemTypesEnum type, Texture2D texture, bool isEquipable)
    {
      FileType = FileTypesEnum.Item.ToString();
      ID = id;
      Name = name;
      Texture = texture;
      Type = type;
      IsEquipable = isEquipable;
    }

    public override string ToString()
    {
      return $"{Name}\n{Type} Item";
    }
  }
}
