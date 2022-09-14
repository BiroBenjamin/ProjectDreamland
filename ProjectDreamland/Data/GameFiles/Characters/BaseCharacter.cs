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
    public int Level { get; set; }
    public string ResourceType { get; set; }
    public float MaxResourcePoints { get; set; }
    private float _currentResourcePoints;
    public float CurrentResourcePoints
    {
      get { return _currentResourcePoints; }
      set
      {
        if (value > MaxResourcePoints) _currentResourcePoints = MaxResourcePoints;
      }
    }
    public float MaxHealthPoints { get; set; }
    private float _currentHealthPoints;
    public float CurrentHealthPoints
    {
      get { return _currentHealthPoints; }
      set
      {
        if (value > MaxHealthPoints) _currentHealthPoints = MaxHealthPoints;
      }
    }
    public float Speed { get; set; } = 3f;
    protected Vector2 velocity;

    public BaseCharacter()
    {
      IsCollidable = true;
    }
    public BaseCharacter(Texture2D texture)
    {
      Texture = texture;
      IsCollidable = true;
    }
    public BaseCharacter(BaseObject baseObject) : base(baseObject) 
    {
      IsCollidable = true;
    }
    public BaseCharacter(BaseCharacter baseCharacter) : base(baseCharacter)
    {
      Texture = baseCharacter.Texture;
      Level = baseCharacter.Level;
      MaxHealthPoints = baseCharacter.MaxHealthPoints;
      CurrentHealthPoints = baseCharacter.CurrentHealthPoints;
      ResourceType = baseCharacter.ResourceType.ToString();
      MaxResourcePoints = baseCharacter.MaxResourcePoints;
      CurrentResourcePoints = baseCharacter.CurrentResourcePoints;
      Speed = baseCharacter.Speed;
      IsCollidable = true;
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
