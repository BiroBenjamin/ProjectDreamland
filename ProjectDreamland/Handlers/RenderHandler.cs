using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Handlers
{
  public class RenderHandler
  {
    public RenderHandler()
    {

    }

    public List<BaseObject> GetRenderableObjects(List<BaseObject> objects, Player player, Rectangle renderView)
    {
      return objects
        .Where(obj => 
          obj.Position.X + obj.Size.Width >= player.Position.X - (renderView.X + renderView.Width) / 2 + player.Size.Width / 2 &&
            obj.Position.X <= player.Position.X + (renderView.X + renderView.Width) / 2 + player.Size.Width / 2 &&
          obj.Position.Y + obj.Size.Height >= player.Position.Y - (renderView.Y + renderView.Height) / 2 + player.Size.Height / 2 &&
            obj.Position.Y <= player.Position.Y + (renderView.Y + renderView.Height) / 2 + player.Size.Height / 2)
        .ToList();
    }
  }
}
