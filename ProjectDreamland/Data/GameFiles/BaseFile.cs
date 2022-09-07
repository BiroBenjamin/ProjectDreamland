using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles
{
  public class BaseFile
  {
    public string FileType { get; set; }
    public string ID { get; set; }
    public string Name { get; set; }
    public int ZIndex { get; set; }
    [XmlIgnore] public string FullImagePath { get; set; }
    public string ImagePath { get; set; }
    [XmlIgnore] public string FullFilePath { get; set; }
    public string FilePath { get; set; }
    [XmlIgnore] public Texture2D Texture { get; set; }
    public Point Position { get; set; }
    public bool IsCollidable { get; set; } = false;
    public Point CollisionPosition { get; set; } = new Point(0, 0);
    public Size CollisionSize { get; set; } = new Size(0, 0);

    public BaseFile(){}
    public BaseFile(BaseFile baseFile)
    {
      FileType = baseFile.FileType;
      ID = baseFile.ID;
      Name = baseFile.Name;
      ZIndex = baseFile.ZIndex;
      FullImagePath = baseFile.FullImagePath;
      ImagePath = baseFile.ImagePath;
      FullFilePath = baseFile.FullFilePath;
      FilePath = baseFile.FilePath;
      Texture = baseFile.Texture;
      Position = baseFile.Position;
      IsCollidable = baseFile.IsCollidable;
      CollisionPosition = baseFile.CollisionPosition;
      CollisionSize = baseFile.CollisionSize;
    }

    public Microsoft.Xna.Framework.Rectangle GetCollision()
    {
      return new Microsoft.Xna.Framework.Rectangle(
        Position.X + CollisionPosition.X,
        Position.Y + CollisionPosition.Y,
        CollisionSize.Width,
        CollisionSize.Height);
    }

    public virtual BaseFile Clone()
    {
      return new BaseFile()
      {
        FileType = FileType,
        ID = ID,
        Name = Name,
        ZIndex = ZIndex,
        FullImagePath = FullImagePath,
        ImagePath = ImagePath,
        FullFilePath = FullFilePath,
        FilePath = FilePath,
        Texture = Texture,
        Position = Position,
        IsCollidable = IsCollidable,
        CollisionPosition = CollisionPosition,
        CollisionSize = CollisionSize,
      };
    }
  }
}
