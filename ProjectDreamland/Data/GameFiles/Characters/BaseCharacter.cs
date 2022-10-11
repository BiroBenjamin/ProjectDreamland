using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.UI;
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
    [XmlIgnore] public CharacterStatesEnum CharacterState { get; set; }
    public bool IsTakingDamage { get; set; } = false;
    public float Speed { get; set; } = 3f;
    public LookDirectionsEnum Facing { get; set; } = LookDirectionsEnum.South;
    [XmlIgnore] public Rectangle AttackBounds = new Rectangle();

    protected Vector2 velocity;
    protected HealthBar _healthBar;
    protected ResourceBar _resourceBar;

    //Mana back per 5 second
    protected int _manaInterval = 25;
    //Energy back per 2 second
    protected int _energyInterval = 10;
    protected int _timer;

    public BaseCharacter()
    {
      IsCollidable = true;
      SetupUI();
    }
    public BaseCharacter(Texture2D texture)
    {
      Texture = texture;
      IsCollidable = true;
      SetupUI();
    }
    public BaseCharacter(BaseObject baseObject) : base(baseObject)
    {
      IsCollidable = true;
      SetupUI();
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
      CharacterState = CharacterStatesEnum.Alive;
      SetupUI();
    }
    private void SetupUI()
    {
      _healthBar = new HealthBar();
      _resourceBar = new ResourceBar(ResourceType);

      SetTimer();
    }

    protected void SetTimer()
    {
      switch (ResourceType)
      {
        case "Mana":
          _timer = 60 * 5;
          break;
        case "Energy":
          _timer = 60 * 2;
          break;
        default:
          _timer = 60 * 2;
          break;
      }
    }

    public void Attack(List<BaseCharacter> characters, BaseAbility ability)
    {
      ability.Cast(characters, this);
    }
    public void TakeDamage(int damage)
    {
      if (CharacterState != CharacterStatesEnum.Alive) return;
      if (CurrentHealthPoints - damage < 0)
      {
        CurrentHealthPoints = 0;
        CharacterState = CharacterStatesEnum.Dying;
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

    public override void Update(GameTime gameTime, List<BaseObject> components)
    {
      ZIndex = Position.Y + Size.Height;

      if (_healthBar == null || _resourceBar == null) return;
      _healthBar.Update(gameTime, MaxHealthPoints, CurrentHealthPoints);
      _resourceBar.Update(gameTime, MaxResourcePoints, CurrentHealthPoints);

      _timer--;
      if (_timer == 0)
      {
        AddResource();
        SetTimer();
      }
    }
    protected void AddResource()
    {
      if (ResourceType == ResourceTypesEnum.Mana.ToString() && CurrentResourcePoints + _manaInterval >= MaxResourcePoints)
      {
        CurrentResourcePoints = MaxResourcePoints;
      }
      else
      {
        CurrentResourcePoints += _manaInterval;
      }
      if (ResourceType == ResourceTypesEnum.Energy.ToString() && CurrentResourcePoints + _energyInterval >= MaxResourcePoints)
      {
        CurrentResourcePoints = MaxResourcePoints;
      }
      else
      {
        CurrentResourcePoints += _energyInterval;
      }
    }

    public override void Draw(ContentManager content, GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (CharacterState == CharacterStatesEnum.Alive)
      {
        base.Draw(content, gameTime, spriteBatch);
      }
      else
      {
        spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), Color.White * .5f);
      }
    }
    public virtual void DrawUI(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      if (_healthBar == null || _resourceBar == null) return;
      _healthBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y), Color.IndianRed);
      _resourceBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y));
    }
  }
}
