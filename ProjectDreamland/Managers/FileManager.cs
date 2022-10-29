using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Objects;
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
        BaseFile baseFile = obj as BaseFile;
        string path = Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, baseFile.FilePath);
        using (StreamWriter writer = new StreamWriter(path))
        {

          serializer.Serialize(writer, obj);
        }
      }
      catch (Exception ex)
      {
        
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
          loadedItem.FilePath = loadedItem.FilePath.Replace(SystemPrefsManager.SystemPrefs.RootPath + "\\", "");
          if (typeof(T) != typeof(Map))
          {
            BaseObject baseObject = (BaseObject)loadedItem;
            baseObject.ImagePath = baseObject.ImagePath.Replace(SystemPrefsManager.SystemPrefs.RootPath + "\\", "");
          }
        }
        catch (Exception ex)
        {
          
        }
      }

      return loadedItem;
    }
  }
}
