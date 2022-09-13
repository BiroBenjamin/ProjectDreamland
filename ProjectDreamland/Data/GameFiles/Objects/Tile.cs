using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  [Serializable]
  public class Tile : BaseObject
  {
    
    public string TileType { get; set; }

    public Tile() { }
    public Tile(BaseObject baseObject) : base(baseObject) { }
    public Tile(Tile tile) : base(tile)
    {
      TileType = tile.TileType;
    }

    public bool CursorIntersects(Microsoft.Xna.Framework.Vector2 cursor)
    {
      return cursor.X > Position.X && cursor.X < Size.Width + Position.X &&
        cursor.Y > Position.Y && cursor.Y < Size.Height + Position.Y;
    }
  }
}
