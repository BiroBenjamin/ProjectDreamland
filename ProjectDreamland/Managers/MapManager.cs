using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectDreamland.Managers
{
  public static class MapManager
  {
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
          tile.ZIndex = -9999999;
        }
        foreach (WorldObject obj in map.WorldObjects)
        {
          string imagePath = Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, obj.ImagePath.Split('.')[0]);
          obj.Texture = content.Load<Texture2D>(imagePath);
        }
        Maps.Add(map);
      }
    }

  }
}
