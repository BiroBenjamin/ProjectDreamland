using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ProjectDreamland.Managers
{
  public static class FileManager
  {
    public static void SaveFile<T>(T obj)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(T));
      try
      {
        using (StreamWriter writer = new StreamWriter((obj as BaseFile).FullFilePath))
        {

          serializer.Serialize(writer, obj);
        }
      }
      catch (Exception ex)
      {
        DebugManager.Log($"{ex.Message}\r\n{ex.InnerException}\r\n{ex.StackTrace}");
      }
    }

    public static BaseFile LoadFile<T>(string path)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(T));

      BaseFile loadedItem = default;
      using (StreamReader reader = new StreamReader(path))
      {
        try
        {
          loadedItem = (BaseFile)serializer.Deserialize(reader);
          loadedItem.FullFilePath = path;
          if(loadedItem.FileType != FileTypesEnum.Map.ToString())
            loadedItem.FullImagePath = Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, loadedItem.ImagePath);
        }
        catch (Exception ex)
        {
          DebugManager.Log($"{ex.Message}\r\n{ex.InnerException}\r\n{ex.StackTrace}");
        }
      }

      return loadedItem;
    }
  }
}
