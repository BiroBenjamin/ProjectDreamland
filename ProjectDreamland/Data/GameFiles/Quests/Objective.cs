using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Quests
{
  public class Objective
  {
    public object Target { get; set; }
    public int Amount { get; set; }
    public int Remaining { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Objective(object target, int amount)
    {
      Target = target;
      Amount = amount;
      Remaining = amount;
    }

    public void Update(GameTime gameTime)
    {
      if (Remaining == 0) IsDone = true;
      if (IsDone)
      {
        Description = " - Completed";
        return;
      }
      if (Target.GetType() == typeof(BaseCharacter))
      {
        Description = $" - {Amount - Remaining} / {Amount} - {(Target as BaseCharacter).Name}";
      }
      else
      {
        Description = $" - {Amount - Remaining} / {Amount} - {(Target as BaseObject).Name}";
      }
    }
  }
}
