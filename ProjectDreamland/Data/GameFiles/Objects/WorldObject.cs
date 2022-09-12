using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  [Serializable]
  public class WorldObject : BaseObject
  {
    public bool IsInteractable { get; set; } = false;

    public WorldObject() { }
    public WorldObject(BaseFile baseFile) :
      base(baseFile)
    { }

    public Microsoft.Xna.Framework.Rectangle GetRectangle()
    {
      return new Microsoft.Xna.Framework.Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
    }

    public bool CursorIntersects(Microsoft.Xna.Framework.Vector2 cursor)
    {
      return cursor.X > Position.X && cursor.X < Size.Width + Position.X &&
        cursor.Y > Position.Y && cursor.Y < Size.Height + Position.Y;
    }

    public override string ToString()
    {
      return $"ID: {ID}\nName: {Name}\nImagePath: {ImagePath}\nLocation: {Position}";
    }

    public override BaseObject Clone()
    {
      WorldObject obj = new WorldObject(base.Clone());
      obj.Size = Size;
      obj.CollisionSize = CollisionSize;
      obj.IsInteractable = IsInteractable;
      return obj;
    }
  }
}
