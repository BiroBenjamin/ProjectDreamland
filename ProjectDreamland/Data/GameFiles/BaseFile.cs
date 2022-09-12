using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles
{
  public class BaseFile
  {
    public string FileType { get; set; }
    public string ID { get; set; }
    public string Name { get; set; }
    [XmlIgnore] public string FullFilePath { get; set; }
    public string FilePath { get; set; }

    public BaseFile() { }
    public BaseFile(BaseFile baseFile)
    {
      FileType = baseFile.FileType;
      ID = baseFile.ID;
      Name = baseFile.Name;
      FullFilePath = baseFile.FullFilePath;
      FilePath = baseFile.FilePath;
    }

    public virtual BaseFile Clone()
    {
      return new BaseFile()
      {
        FileType = FileType,
        ID = ID,
        Name = Name,
        FullFilePath = FullFilePath,
        FilePath = FilePath,
      };
    }
  }
}
