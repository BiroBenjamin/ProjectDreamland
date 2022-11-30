﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectDreamland.Managers
{
  public static class MapManager
  {
    public static Map CurrentMap { get; set; }
    public static List<BaseObject> CurrentMapComponents { get; set; } = new List<BaseObject>();
    public static Map LastMap { get; set; }
    public static List<Map> Maps { get; set; } = new List<Map>();

    private static readonly string _mapsFolder = SystemPrefsManager.SystemPrefs.FolderStructure[FileTypesEnum.Map.ToString()][0];

    public static void LoadMaps(ContentManager content)
    {
      DirectoryInfo directory = new DirectoryInfo(_mapsFolder);
      FileInfo[] files = directory.GetFiles();
      foreach (FileInfo fileInfo in files)
      {
        Map map = (Map)FileManager.LoadFile<Map>(fileInfo.FullName);
        foreach (Tile tile in map.Tiles)
        {
          string imagePath = Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, tile.ImagePath.Split('.')[0]);
          tile.Texture = content.Load<Texture2D>(imagePath);
        }
        foreach (WorldObject obj in map.WorldObjects)
        {
          string imagePath = Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, obj.ImagePath.Split('.')[0]);
          obj.Texture = content.Load<Texture2D>(imagePath);
          CommandManager.LoadCommand(obj.Instructions, obj, CommandLoadStateEnum.OnLoad);
        }
        foreach (BaseCharacter character in map.Characters)
        {
          string imagePath = Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, character.ImagePath.Split('.')[0]);
          character.Texture = content.Load<Texture2D>(imagePath);
          CommandManager.LoadCommand(character.Instructions, character, CommandLoadStateEnum.OnLoad);
        }
        Maps.Add(map);
      }
      CurrentMap = Maps.Where(x => x.ID == "testMap002").FirstOrDefault();
      LastMap = CurrentMap;
    }

    public static bool LoadNewMap(string id, Player player)
    {
      if (LastMap != CurrentMap)
      {
        LastMap = CurrentMap;
      }
      CurrentMap = Maps.Where(x => x.ID == id).FirstOrDefault();
      if (CurrentMap != null)
      {
        LoadMapContent(player);
        return true;
      }
      return false;
    }
    public static void LoadMapContent(Player player)
    {
      CurrentMapComponents.Clear();
      CurrentMapComponents.AddRange(CurrentMap.Tiles);
      CurrentMapComponents.AddRange(CurrentMap.WorldObjects);
      CurrentMapComponents.AddRange(CurrentMap.Characters);
      CurrentMapComponents.Add(player);
    }
  }
}
