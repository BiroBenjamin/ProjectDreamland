using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  public class BaseObject : BaseFile
  {
    public int ZIndex { get; set; }
    [XmlIgnore] public string FullImagePath { get; set; }
    public string ImagePath { get; set; }
    [XmlIgnore] public Texture2D Texture { get; set; }
    public System.Drawing.Point Position { get; set; }
    public System.Drawing.Size Size { get; set; }
    public System.Drawing.Point CollisionPosition { get; set; } = new System.Drawing.Point(0, 0);
    public System.Drawing.Size CollisionSize { get; set; } = new System.Drawing.Size(0, 0);
    public bool IsCollidable { get; set; } = false;

    public BaseObject() { }
    public BaseObject(BaseFile baseFile) : base(baseFile) { }

    public Rectangle GetCollision()
    {
      return new Rectangle(
        Position.X + CollisionPosition.X,
        Position.Y + CollisionPosition.Y,
        CollisionSize.Width,
        CollisionSize.Height);
    }

    public override BaseFile Clone()
    {
      BaseObject obj = new BaseObject(base.Clone());
      obj.ZIndex = ZIndex;
      obj.FullImagePath = FullImagePath;
      obj.ImagePath = ImagePath;
      obj.Texture = Texture;
      obj.Position = Position;
      obj.Size = Size;
      obj.IsCollidable = IsCollidable;
      obj.CollisionPosition = CollisionPosition;
      obj.CollisionSize = CollisionSize;
      return obj;
    }

    public virtual void Update(GameTime gameTime)
    {

    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), Color.White);
    }
  }
}
