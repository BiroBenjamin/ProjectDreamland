using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Items;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Data.GameFiles.Quests;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ProjectDreamland.Managers
{
  public static class CommandManager
  {
    private static Dictionary<string, List<string>> _commandList = new Dictionary<string, List<string>>();

    public static void LoadCommand(string command, Player player, BaseFile emitter)
    {
      foreach(string cmd in command.Replace("\\n", "").Split(';'))
      {
        string name = cmd.Split("(")[0];
        string value = cmd.Split("(")[1].Replace(")", "");
        if (_commandList.ContainsKey(name))
        {
          _commandList[name].Add(value);
        }
        else
        {
          _commandList.Add(name, new List<string>() { value });
        }
      }

      foreach (KeyValuePair<string, List<string>> cmd in _commandList)
      {
        switch (cmd.Key)
        {
          case "teleport":
            Teleport(cmd.Value, player);
            break;
          case "give":
            Give(cmd.Value, player, emitter as WorldObject);
            break;
        }
      }

      _commandList.Clear();
    }

    private static void Teleport(List<string> command, Player player)
    {
      foreach(string line in command)
      {
        string[] args = line.Split(',');
        string map = args[0];
        MapManager.LoadNewMap(map, player);
        int.TryParse(args[1], out int x);
        int.TryParse(args[2], out int y);
        player.SetPosition(new Point(x, y));
      }
    }
    private static void Give(List<string> command, Player player, WorldObject emitter)
    {
      if (emitter.IsLooted || !emitter.IsInteractable) return;
      foreach (string line in command)
      {
        int givenAmount = 0;
        string[] args = line.Split(',');
        Item itemToGive = ItemManager.Items.Where(x => x.ID == args[0]).FirstOrDefault();
        if (itemToGive == null) continue;
        if (args.Length == 2)
        {
          int.TryParse(args[1], out givenAmount);
          InventoryManager.AddItem(itemToGive, givenAmount);
        }
        else if (args.Length == 3)
        {
          Random random = new Random();
          _ = int.TryParse(args[1], out int countMin);
          _ = int.TryParse(args[2], out int countMax);
          givenAmount = random.Next(countMin, countMax + 1);
          InventoryManager.AddItem(itemToGive, givenAmount);
        }
        foreach (Quest quest in Player.Quests)
        {
          if (quest.Objective.IsDone) continue;
          if (quest.Objective.Target.ID == itemToGive.ID)
          {
            int remaining = quest.Objective.Remaining - givenAmount;
            quest.Objective.Remaining = remaining < 0 ? 0 : remaining;
          }
        }
      }
      emitter.IsLooted = true;
    }
  }
}
