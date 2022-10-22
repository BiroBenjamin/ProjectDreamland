using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Quests;
using ProjectDreamland.UI.QuestPanel;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Managers
{
  public static class CharacterManager
  {
    public static void HandleDeadCharacters(List<BaseCharacter> characters, Player player)
    {
      foreach (BaseCharacter character in characters)
      {
        if (character.CharacterState == CharacterStatesEnum.Dying)
        {
          player.CurrentExperience += (int)Math.Pow(character.Level * 5, 1.1);
          character.CharacterState = CharacterStatesEnum.Dead;
          foreach (Quest quest in player.Quests)
          {
            if (quest.Objective.IsDone) continue;
            if (quest.Objective.TargetID == character.ID)
            {
              quest.Objective.Remaining--;
            }
          }
        }
      }
    }
  }
}
