using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Objects;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  public class BaseCharacter : BaseObject
  {
    protected float speed = 3f;
    protected Vector2 velocity;

    public Rectangle GetSize()
    {
      return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    }

    public BaseCharacter(Texture2D texture)
    {
      Texture = texture;
    }

    #region Collision
    protected bool IsCollidingLeft(BaseObject sprite)
    {
      Rectangle thisCollision = GetCollision();
      Rectangle targetCollision = sprite.GetCollision();
      return thisCollision.Right + velocity.X > targetCollision.Left &&
             thisCollision.Right < targetCollision.Right &&
             thisCollision.Top < targetCollision.Bottom &&
             thisCollision.Bottom > targetCollision.Top;

    }
    protected bool IsCollidingRight(BaseObject sprite)
    {
      Rectangle thisCollision = GetCollision();
      Rectangle targetCollision = sprite.GetCollision();
      return thisCollision.Left + velocity.X < targetCollision.Right &&
             thisCollision.Left > targetCollision.Left &&
             thisCollision.Top < targetCollision.Bottom &&
             thisCollision.Bottom > targetCollision.Top;

    }
    protected bool IsCollidingTop(BaseObject sprite)
    {
      Rectangle thisCollision = GetCollision();
      Rectangle targetCollision = sprite.GetCollision();
      return thisCollision.Bottom + this.velocity.Y > targetCollision.Top &&
             thisCollision.Top < targetCollision.Top &&
             thisCollision.Right > targetCollision.Left &&
             thisCollision.Left < targetCollision.Right;

    }
    protected bool IsCollidingBottom(BaseObject sprite)
    {
      int zindex = ZIndex;
      Rectangle thisCollision = GetCollision();
      Rectangle targetCollision = sprite.GetCollision();
      return thisCollision.Top + this.velocity.Y < targetCollision.Bottom &&
             thisCollision.Bottom > targetCollision.Bottom &&
             thisCollision.Right > targetCollision.Left &&
             thisCollision.Left < targetCollision.Right;

    }
    #endregion

    public override void Update(GameTime gameTime)
    {

    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(gameTime, spriteBatch);
    }
  }
}
