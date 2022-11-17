using ProjectDreamland.Data.Enums;
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
    public static Player Player { get; set; }

    private static Dictionary<string, List<string>> _commandList = new Dictionary<string, List<string>>();

    public static void LoadCommand(string command, BaseFile emitter, CommandLoadStateEnum loadState)
    {
      if (command == null) return;
      string commandToUse;
      switch (loadState)
      {
        case CommandLoadStateEnum.OnLoad:
          commandToUse = GetPartOfCommand(command, "OnLoad;", "OnLoadEnd;");
          break;
        case CommandLoadStateEnum.OnUpdate:
          commandToUse = GetPartOfCommand(command, "OnUpdate;", "OnUpdateEnd;");
          break;
        case CommandLoadStateEnum.OnInteract:
          commandToUse = GetPartOfCommand(command, "OnInteract;", "OnInteractEnd;");
          break;
        default:
          commandToUse = null;
          break;
      }
      if (commandToUse == null) return;
      foreach (string cmd in commandToUse.Replace("\\n", "").Split(';'))
      {
        if (cmd == "" || cmd == " " || cmd == null) continue;
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
            Teleport(cmd.Value, Player);
            break;
          case "give":
            Give(cmd.Value, Player, emitter as WorldObject);
            break;
          case "alwaysOnTop":
            AlwaysOnTop(cmd.Value, emitter as WorldObject);
            break;
          case "setRespawn":
            SetRespawnPoint(cmd.Value);
            break;
        }
      }

      _commandList.Clear();
    }
    private static string GetPartOfCommand(string command, string startSubStr, string endSubStr)
    {
      if (!command.Contains(startSubStr)) return null;
      int startIndex = command.IndexOf(startSubStr) + startSubStr.Length;
      int endIndex = command.IndexOf(endSubStr);
      string resultCommand = command.Substring(startIndex, endIndex - startIndex);
      return resultCommand;
    }

    private static void Teleport(List<string> command, Player player)
    {
      if (player == null) return;
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
      if (emitter.IsLooted || !emitter.IsInteractable || player == null) return;
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
    private static void AlwaysOnTop(List<string> command, WorldObject emitter)
    {
      foreach(string line in command)
      {
        if (line.ToLower().Equals("true"))
        {
          emitter.ZIndex = int.MaxValue;
        }
      }
    }
    private static void SetRespawnPoint(List<string> command)
    {
      foreach (string line in command)
      {
        string[] args = line.Split(',');
        string map = args[0];
        int.TryParse(args[1], out int x);
        int.TryParse(args[2], out int y);
        Player.RespawnPoint.Map = map;
        Player.RespawnPoint.X = x;
        Player.RespawnPoint.Y = y;
      }
    }
  }
}
