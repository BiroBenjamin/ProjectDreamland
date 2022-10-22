using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  public class BaseObject : BaseFile
  {
    public int ZIndex { get; set; }
    public string ImagePath { get; set; }
    [XmlIgnore] public Texture2D Texture { get; set; }
    [XmlIgnore] public float Alpha { get; set; } = 1f;
    public System.Drawing.Point Position { get; set; }
    public System.Drawing.Size Size { get; set; }
    public System.Drawing.Point CollisionPosition { get; set; } = new System.Drawing.Point(0, 0);
    public System.Drawing.Size CollisionSize { get; set; } = new System.Drawing.Size(0, 0);
    public bool IsCollidable { get; set; } = false;
    public string OtherData { get; set; }

    public BaseObject() { }
    public BaseObject(BaseFile baseFile) : base(baseFile) { }
    public BaseObject(BaseObject baseObject) : base(baseObject)
    {
      ZIndex = baseObject.ZIndex;
      ImagePath = baseObject.ImagePath;
      Texture = baseObject.Texture;
      Position = baseObject.Position;
      Size = baseObject.Size;
      IsCollidable = baseObject.IsCollidable;
      CollisionPosition = baseObject.CollisionPosition;
      CollisionSize = baseObject.CollisionSize;
    }

    public virtual Rectangle GetCollision()
    {
      return new Rectangle(
        Position.X + CollisionPosition.X,
        Position.Y + CollisionPosition.Y,
        CollisionSize.Width,
        CollisionSize.Height);
    }

    public virtual void Update(GameTime gameTime, List<BaseObject> components)
    {

    }
    public virtual void Draw(ContentManager content, GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), Color.White * Alpha);
    }
  }
}
