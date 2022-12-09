using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Managers;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Quests
{
  public class Objective
  {
    public string TargetID { get; set; }
    public string TargetName { get; set; }
    public string QuestType { get; set; }
    public int Amount { get; set; }
    public int Remaining { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Objective(string targetID, int amount, string type)
    {
      TargetID = targetID;
      QuestType = type;
      
      {
        //foreach (Map map in MapManager.Maps)
        //{
        //  foreach (BaseFile objective in map.Characters)
        //  {
        //    if (objective.ID == targteID)
        //      TargetID = objective;
        //  }
        //}
      }
      else if (type == "collect")
      {
        foreach(BaseFile item in ItemManager.Items)
        {
          //if(item.ID == targteID)
          //{
          //  Target = item;
          //}
        }
      }
      
      {
        //foreach (Map map in MapManager.Maps)
        //{
        //  foreach (BaseFile objective in map.Characters)
        //  {
        //    if (objective.ID == targteID)
        //      TargetID = objective;
        //  }
        //}
      }
      else if (type == "collect")
      {
        foreach(BaseFile item in ItemManager.Items)
        {
          //if(item.ID == targteID)
          //{
          //  Target = item;
          //}
        }
      }
      
      Amount = amount;
      Remaining = amount;
      Description = $" - {Amount - Remaining} / {Amount} - ???";
    }

    public void Update(GameTime gameTime, string questType)
    {
      if (string.IsNullOrEmpty(TargetName))
      {
        if (QuestType == "kill")
        {
          foreach (Map map in MapManager.Maps)
          {
            foreach (BaseFile objective in map.Characters)
            {
              if (objective.ID == TargetID)
                TargetName = objective.Name;
            }
          }
        }
        else if (QuestType == "collect")
        {
          foreach (BaseFile item in ItemManager.Items)
          {
            if (item.ID == TargetID)
            {
              TargetName = item.Name;
            }
          }
        }
      }
      if(questType == "collect")
      {
        int alreadyHave = InventoryManager.Items.Where(x => x != null && x.ID == TargetID).Count();
        Remaining = alreadyHave < 0 ? 0 : Amount - alreadyHave;
      }
      if (Remaining == 0) IsDone = true;
      if (IsDone)
      {
        Description = " - Completed";
        return;
      }
      Description = $" - {Amount - Remaining} / {Amount} - {TargetName}";
    }
  }
}
