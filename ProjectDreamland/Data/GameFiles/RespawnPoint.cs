using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Managers;

namespace ProjectDreamland.Data.GameFiles
{
  public class RespawnPoint
  {
    public string Map { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public void Respawn(Player player)
    {
      MapManager.LoadNewMap(Map, player);
      player.SetPosition(new System.Drawing.Point(X, Y));
      player.CharacterState = CharacterStatesEnum.Alive;
      player.CurrentHealthPoints = (int)(player.MaxHealthPoints * .4f);
      player.CurrentResourcePoints = (int)(player.MaxResourcePoints * .1f);
    }
  }
}
