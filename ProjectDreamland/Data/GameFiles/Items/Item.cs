using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;

namespace ProjectDreamland.Data.GameFiles.Items
{
  public class Item : BaseFile
  {
    public Texture2D Texture { get; set; }
    public ItemTypesEnum Type { get; set; }

    public Item(string id, string name, ItemTypesEnum type, Texture2D texture)
    {
      FileType = FileTypesEnum.Item.ToString();
      ID = id;
      Name = name;
      Texture = texture;
      Type = type;
    }

  }
}
