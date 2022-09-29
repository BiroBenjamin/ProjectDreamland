using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.Data.GameFiles.Objects;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  [Serializable]
  public class BaseCharacter : BaseObject
  {
    public int Level { get; set; }
    public string ResourceType { get; set; }
    public int MaxResourcePoints { get; set; }
    public int CurrentResourcePoints;
    public (int, int) AttackDamage { get; set; } = (5, 9);
    public float AttackRange { get; set; } = 1.5f;
    public int MaxHealthPoints { get; set; }
    public int CurrentHealthPoints;
    public bool IsDead { get; set; } = false;
    public bool IsTakingDamage { get; set; } = false;
    public float Speed { get; set; } = 3f;
    public LookDirectionsEnum Facing { get; set; } = LookDirectionsEnum.South;
    [XmlIgnore] public Rectangle AttackBounds = new Rectangle();

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

    public void Attack(List<BaseCharacter> characters, BaseAbility ability)
    {
      ability.Cast(characters, this);
    }
    public void TakeDamage(int damage)
    {
      //IsTakingDamage = true;
      if (CurrentHealthPoints - damage < 0)
      {
        CurrentHealthPoints = 0;
        IsDead = true;
      }
      else if (CurrentHealthPoints - damage > MaxHealthPoints)
      {
        CurrentHealthPoints = MaxHealthPoints;
      }
      else
      {
        CurrentHealthPoints -= damage;        
      }
    }

    #region Collision
    public bool IsCollidingLeft(Rectangle targetCollision)
    {
      Rectangle thisCollision = GetCollision();
      return thisCollision.Right + velocity.X > targetCollision.Left &&
             thisCollision.Right < targetCollision.Right &&
             thisCollision.Top < targetCollision.Bottom &&
             thisCollision.Bottom > targetCollision.Top;

    }
    public bool IsCollidingRight(Rectangle targetCollision)
    {
      Rectangle thisCollision = GetCollision();
      return thisCollision.Left + velocity.X < targetCollision.Right &&
             thisCollision.Left > targetCollision.Left &&
             thisCollision.Top < targetCollision.Bottom &&
             thisCollision.Bottom > targetCollision.Top;

    }
    public bool IsCollidingTop(Rectangle targetCollision)
    {
      Rectangle thisCollision = GetCollision();
      return thisCollision.Bottom + this.velocity.Y > targetCollision.Top &&
             thisCollision.Top < targetCollision.Top &&
             thisCollision.Right > targetCollision.Left &&
             thisCollision.Left < targetCollision.Right;

    }
    public bool IsCollidingBottom(Rectangle targetCollision)
    {
      int zindex = ZIndex;
      Rectangle thisCollision = GetCollision();
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
    public override void Draw(ContentManager content, GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (IsDead) return;
      base.Draw(content, gameTime, spriteBatch);
      if (IsTakingDamage)
      {
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts/ArialBig"), $"{MaxHealthPoints}/{CurrentHealthPoints}",
          new Vector2(Position.X, Position.Y - 10), Color.Red);
      }
      else
      {
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts/ArialBig"), $"{MaxHealthPoints}/{CurrentHealthPoints}",
          new Vector2(Position.X, Position.Y - 10), Color.Black);
      }
    }
  }
}
