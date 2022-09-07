using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles
{
  [Serializable]
  public class Tile : BaseFile
  {
    [XmlIgnore] public Size BaseSize { get; private set; } = new Size(32, 32);
    public Size Size { get; set; } = new Size(64, 64);
    public string TileType { get; set; }

    public Tile(){}
    public Tile(BaseFile baseFile) :
      base(baseFile){}

    public bool CursorIntersects(Microsoft.Xna.Framework.Vector2 cursor)
    {
      return cursor.X > Position.X && cursor.X < Size.Width + Position.X &&
        cursor.Y > Position.Y && cursor.Y < Size.Height + Position.Y;
    }

    public override BaseFile Clone()
    {
      Tile obj = new Tile(base.Clone());
      obj.BaseSize = BaseSize;
      obj.Size = Size;
      obj.TileType = TileType;
      return obj;
    }

  }
}
