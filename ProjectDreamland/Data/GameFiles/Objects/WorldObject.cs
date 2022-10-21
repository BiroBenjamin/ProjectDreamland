using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Managers;
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
    public WorldObject(BaseObject baseObject) : base(baseObject) { }
    public WorldObject(WorldObject worldObject) : base(worldObject)
    {
      IsInteractable = worldObject.IsInteractable;
    }

    public Microsoft.Xna.Framework.Rectangle GetRectangle()
    {
      return new Microsoft.Xna.Framework.Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
    }

    public bool CursorIntersects(Microsoft.Xna.Framework.Vector2 cursor)
    {
      return cursor.X > Position.X && cursor.X <= Size.Width + Position.X &&
        cursor.Y > Position.Y && cursor.Y <= Size.Height + Position.Y;
    }

    public void Interact(Player player)
    {
      if (!IsInteractable) return;
      if(ID == "dirt_cliff_entrance_open") {
        MapManager.LoadNewMap("testMap001", player);
        player.SetPosition(new Point(0, 0));
      }
    }
  }
}
