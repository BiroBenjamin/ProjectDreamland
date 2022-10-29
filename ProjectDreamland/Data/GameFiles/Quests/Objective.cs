using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Quests
{
  public class Objective
  {
    public string TargetID { get; set; }
    public int Amount { get; set; }
    public int Remaining { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Objective(string targetID, int amount)
    {
      TargetID = targetID;
      Amount = amount;
      Remaining = amount;
      Description = $" - {Amount - Remaining} / {Amount} - {TargetID}";
    }

    public void Update(GameTime gameTime)
    {
      if (Remaining == 0) IsDone = true;
      if (IsDone)
      {
        Description = " - Completed";
        return;
      }
      Description = $" - {Amount - Remaining} / {Amount} - {TargetID}";
    }
  }
}
