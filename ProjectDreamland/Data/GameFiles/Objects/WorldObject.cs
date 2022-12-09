using Microsoft.Xna.Framework;
using ProjectDreamland.Components;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  [Serializable]
  public class WorldObject : BaseObject
  {
    public bool IsInteractable { get; set; } = false;
    public bool IsLooted { get; set; }

    private Timer _respawnTimer = new Timer(30);

    public WorldObject() { }
    public WorldObject(BaseObject baseObject) : base(baseObject) { }
    public WorldObject(WorldObject worldObject) : base(worldObject)
    {
      IsInteractable = worldObject.IsInteractable;
      IsLooted = worldObject.IsLooted;
    }

    public bool CursorIntersects(Vector2 cursor)
    {
      return cursor.X > Position.X && cursor.X <= Size.Width + Position.X &&
        cursor.Y > Position.Y && cursor.Y <= Size.Height + Position.Y;
    }

    public void Interact(Player player)
    {
      if (!IsInteractable) return;
      CommandManager.LoadCommand(Instructions, this, Enums.CommandLoadStateEnum.OnInteract);
    }

    public override void Update(GameTime gameTime, List<BaseObject> components)
    {
      if (!IsLooted) return;
      if (_respawnTimer.Count(gameTime) == 0)
      {
        IsLooted = false;
        _respawnTimer.Reset();
      }
    }
  }
}
