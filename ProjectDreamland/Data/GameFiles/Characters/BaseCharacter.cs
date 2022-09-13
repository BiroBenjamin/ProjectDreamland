using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Objects;
using System;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  [Serializable]
  public class BaseCharacter : BaseObject
  {
    public float Health { get; set; }
    public string ResourceType { get; set; }
    public float MaxResourceAmount { get; set; }
    private float _currentResourceAmount;
    public float CurrentResourceAmount
    {
      get { return _currentResourceAmount; }
      set
      {
        if (value > MaxResourceAmount) _currentResourceAmount = MaxResourceAmount;
      }
    }
    public float Speed { get; set; } = 3f;

    protected Vector2 velocity;

    //public Rectangle GetSize()
    //{
    //  return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    //}

    public BaseCharacter() { }
    public BaseCharacter(Texture2D texture)
    {
      Texture = texture;
    }
    public BaseCharacter(Texture2D texture, float health, ResourceTypesEnum resourceType, float maxResource, float currentResource, float speed)
    {
      Texture = texture;
      Health = health;
      ResourceType = resourceType.ToString();
      MaxResourceAmount = maxResource;
      _currentResourceAmount = currentResource;
      Speed = speed;
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
      ZIndex = Position.Y + Size.Height;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(gameTime, spriteBatch);
    }
  }
}
