using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Components;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Data.GameFiles.Quests;
using ProjectDreamland.Handlers;
using ProjectDreamland.Managers;
using ProjectDreamland.UI;
using ProjectDreamland.UI.QuestPanel;
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
    public float AttackRange { get; set; }
    public float AggroRange { get; set; }
    public int MaxHealthPoints { get; set; }
    public int CurrentHealthPoints;
    public bool IsTakingDamage { get; set; } = false;
    public float Speed { get; set; } = 3f;
    public CharacterAffiliationsEnum CharacterAffiliation { get; set; }
    public CharacterStatesEnum CharacterState { get; set; }
    public BehaviourStatesEnum BehaviourState { get; set; }
    public LookDirectionsEnum Facing { get; set; } = LookDirectionsEnum.South;
    [XmlIgnore] public Rectangle AttackBounds = new Rectangle();
    [XmlIgnore] public MeleeAttack MeleeAttack { get; set; }
    [XmlIgnore] public Quest Quest { get; set; }

    protected Vector2 velocity;
    protected HealthBar _healthBar;
    protected ResourceBar _resourceBar;

    //Mana back per 5 second
    public int ManaInterval = 5;
    //Energy back per 2 second
    public int EnergyInterval = 10;
    protected Timer _resourceTimer;
    protected AIHandler _aiHandler;

    protected string _questGivenID;
    protected bool _isQuestAccepted = false;

    private Timer _respawnTimer;
    private Vector2 _origin;

    public BaseCharacter()
    {
      IsCollidable = true;
      Initialize();
    }
    public BaseCharacter(Texture2D texture)
    {
      Texture = texture;
      IsCollidable = true;
      Initialize();
    }
    public BaseCharacter(BaseObject baseObject) : base(baseObject)
    {
      IsCollidable = true;
      Initialize();
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
      Initialize();
    }

    private void Initialize()
    {
      CharacterState = CharacterStatesEnum.Alive;
      BehaviourState = BehaviourStatesEnum.Idle;
      AggroRange = 10;
      MeleeAttack = new MeleeAttack("Attack", "", ResourceTypesEnum.None, 0, 35, DamageTypesEnum.Physical, AbilityTypesEnum.Damage, 64, 2, true);
      _aiHandler = new AIHandler(this);
      _origin = new Vector2(Position.X, Position.Y);
      SetupUI();
      SetTimer();
    }
    private void SetupUI()
    {
      _healthBar = new HealthBar();
      _resourceBar = new ResourceBar(ResourceType);
    }

    protected void SetTimer()
    {
      switch (ResourceType)
      {
        case "Mana":
          _resourceTimer = new Timer(5);
          break;
        case "Energy":
          _resourceTimer = new Timer(2);
          break;
        default:
          _resourceTimer = new Timer(2);
          break;
      }
      _respawnTimer = new Timer(60);
    }

    public void Attack(List<BaseCharacter> characters, BaseAbility ability)
    {
      ability.Cast(characters, this);
    }
    public void Attack(BaseCharacter character, BaseAbility ability)
    {
      ability.Cast(character, this);
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

    public void Move(Vector2 direction, List<BaseObject> components)
    {
      if (CharacterState != CharacterStatesEnum.Alive) return;
      velocity = new Vector2(direction.X * Speed, direction.Y * Speed);
      Collision(components);
      Position = new System.Drawing.Point((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y));
    }

    private void Collision(List<BaseObject> components)
    {
      foreach (BaseObject comp in components)
      {
        if (velocity.X > 0 && IsCollidingLeft(comp.GetCollision()) || velocity.X < 0 && IsCollidingRight(comp.GetCollision()))
          velocity.X = 0;
        if (velocity.Y > 0 && IsCollidingTop(comp.GetCollision()) || velocity.Y < 0 && IsCollidingBottom(comp.GetCollision()))
          velocity.Y = 0;
      }
    }

    public bool CursorIntersects(Vector2 cursor)
    {
      return cursor.X > Position.X && cursor.X <= Size.Width + Position.X &&
        cursor.Y > Position.Y && cursor.Y <= Size.Height + Position.Y;
    }
    public void Interact(Player player)
    {
      if (CharacterAffiliation != CharacterAffiliationsEnum.Friendly || Quest == null) return;
      QuestWindow.Quest = Quest;
      if (QuestWindow.Quest.IsDone) return;
      QuestWindow.IsShown = true;
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

      _aiHandler.Update(gameTime, components);

      if (_healthBar == null || _resourceBar == null) return;
      _healthBar.Update(gameTime, MaxHealthPoints, CurrentHealthPoints);
      _resourceBar.Update(gameTime, MaxResourcePoints, CurrentHealthPoints);

      MeleeAttack.Update(gameTime, components);

      if (_resourceTimer.Count(gameTime) == 0)
      {
        AddResource();
        _resourceTimer.Reset();
      }
      if(_respawnTimer.Count(gameTime) == 0 && CharacterState == CharacterStatesEnum.Dead)
      {
        CharacterState = CharacterStatesEnum.Alive;
        Position = new System.Drawing.Point((int)_origin.X, (int)_origin.Y);
        CurrentHealthPoints = MaxHealthPoints;
        _respawnTimer.Reset();
      }
    }
    protected void AddResource()
    {
      if (ResourceType == ResourceTypesEnum.Mana.ToString() && CurrentResourcePoints + ManaInterval >= MaxResourcePoints)
      {
        CurrentResourcePoints = MaxResourcePoints;
      }
      else
      {
        CurrentResourcePoints += ManaInterval;
      }
      if (ResourceType == ResourceTypesEnum.Energy.ToString() && CurrentResourcePoints + EnergyInterval >= MaxResourcePoints)
      {
        CurrentResourcePoints = MaxResourcePoints;
      }
      else
      {
        CurrentResourcePoints += EnergyInterval;
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
      Color healtbarColor = CharacterAffiliation == CharacterAffiliationsEnum.Hostile ? Color.IndianRed : Color.LawnGreen;
      _healthBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y), healtbarColor);
      _resourceBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y));
    }
  }
}
