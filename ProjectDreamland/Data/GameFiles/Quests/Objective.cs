using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Managers;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Quests
{
  public class Objective
  {
    public BaseFile Target { get; set; }
    public int Amount { get; set; }
    public int Remaining { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Objective(string targteID, int amount, string type)
    {
      if (type == "kill")
      {
        foreach (Map map in MapManager.Maps)
        {
          foreach (BaseFile objective in map.Characters)
          {
            if (objective.ID == targteID)
              Target = objective;
          }
        }
      }
      else if (type == "collect")
      {
        foreach(BaseFile item in ItemManager.Items)
        {
          if(item.ID == targteID)
          {
            Target = item;
          }
        }
      }
      
      Amount = amount;
      Remaining = amount;
      Description = $" - {Amount - Remaining} / {Amount} - {Target.Name}";
    }

    public void Update(GameTime gameTime)
    {
      if (Remaining == 0) IsDone = true;
      if (IsDone)
      {
        Description = " - Completed";
        return;
      }
      Description = $" - {Amount - Remaining} / {Amount} - {Target.Name}";
    }
  }
}
