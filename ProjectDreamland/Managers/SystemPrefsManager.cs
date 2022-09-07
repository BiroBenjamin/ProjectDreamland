using ProjectDreamland.Data;
using ProjectDreamland.UI;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ProjectDreamland.Managers
{
  public static class SystemPrefsManager
  {
    private static readonly XmlSerializer serializer = new XmlSerializer(typeof(SystemPrefs));
    private static readonly string path = $@"C:\Users\{Environment.UserName}\Documents\DreamlandEditor\SystemPrefs.xml";
    public static SystemPrefs SystemPrefs { get; set; } = new SystemPrefs();

    public static SystemPrefs SetUpSystemPrefs()
    {
      if (!File.Exists(path))
      {
        SerializeSystemPrefs();
        return SystemPrefs;
      }
      DeserializeSystemPrefs();
      return SystemPrefs;
    }
    private static void DeserializeSystemPrefs()
    {
      using (TextReader reader = new StreamReader(path))
      {
        SystemPrefs = (SystemPrefs)serializer.Deserialize(reader);
      }
    }
    private static void SerializeSystemPrefs()
    {
      using (StreamWriter writer = new StreamWriter(path))
      {
        SystemPrefs = new SystemPrefs();
        serializer.Serialize(writer, SystemPrefs);
      }
    }
  }
}
