using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Components;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Data.GameFiles.Quests;
using ProjectDreamland.Handlers;
using ProjectDreamland.Managers;
using ProjectDreamland.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  public class Player : BaseCharacter
  {
    new public float Speed { get; set; } = 2f;
    public int ExperienceNeeded { get; set; }
    public static int CurrentExperience { get; set; }
    public Stats BaseStats { get; set; }
    public Stats BonusStats { get; set; }

    public static Player Self { get; set; }
    public static RespawnPoint RespawnPoint { get; set; } = new RespawnPoint();
    public new static BaseAbility MeleeAttack { get; set; }
    public static BaseAbility NurturingWinds { get; set; }
    public static BaseAbility Fireball { get; set; }

    private GraphicsDevice _graphicsDevice;
    private KeyboardState _currentKeyState;
    private KeyboardState _lastKeyState;
    private MouseState _currentMouseState;
    private MouseState _lastMouseState;

    public static List<Quest> Quests { get; set; }

    public Player(GraphicsDevice graphicsDevice, Texture2D texture, Map currentMap) : base(texture)
    {
      Size = new System.Drawing.Size(texture.Width, texture.Height);
      SetCollision(Position.X, Position.Y, texture.Width, texture.Height);
      _graphicsDevice = graphicsDevice;
      SetStatus();
    }
    public Player(GraphicsDevice graphicsDevice, Texture2D texture, Map currentMap, int x, int y) : base(texture)
    {
      Size = new System.Drawing.Size(texture.Width, texture.Height);
      Position = new System.Drawing.Point(x, y);
      SetCollision(Position.X, Position.Y, texture.Width, texture.Height);
      _graphicsDevice = graphicsDevice;
      SetStatus();
    }
    private void SetCollision(int posX, int posY, int textWidth, int textHeight)
    {
      int width = textWidth - textWidth / 2;
      int height = textHeight / 4;

      CollisionSize = new System.Drawing.Size(width, height);
      CollisionPosition = new System.Drawing.Point(posX + width / 2, posY + textHeight - height);
    }
    private void SetStatus()
    {
      Level = 4;
      AttackDamage = (1, 3);
      MaxHealthPoints = 200;
      CurrentHealthPoints = MaxHealthPoints;
      ResourceType = ResourceTypesEnum.Mana.ToString();
      MaxResourcePoints = 238;
      CurrentResourcePoints = 238;
      _resourceBar = new ResourceBar(ResourceType);
      ExperienceNeeded = (int)Math.Pow(Level * 100, 1.1);
      MeleeAttack = AbilitiesList.AbilitiesList.MeleeAttack;
      NurturingWinds = AbilitiesList.AbilitiesList.NurturingWinds;
      Fireball = AbilitiesList.AbilitiesList.Fireball;
      Quests = new List<Quest>();
      BaseStats = new Stats(AttackDamage, MaxHealthPoints, MaxResourcePoints, ManaInterval);
      CommandManager.Player = this;
      Self = this;
    }

    public void SetPosition(System.Drawing.Point position)
    {
      Position = position;
      SetCollision(Position.X, Position.Y, Texture.Width, Texture.Height);
    }

    private void Move(List<BaseObject> components)
    {
      if (CharacterState != CharacterStatesEnum.Alive) return;
      _currentKeyState = Keyboard.GetState();
      velocity = new Vector2();

      if (_currentKeyState.IsKeyDown(Keys.W))
      {
        Facing = LookDirectionsEnum.North;
        velocity.Y -= Speed;
      }
      if (_currentKeyState.IsKeyDown(Keys.S))
      {
        Facing = LookDirectionsEnum.South;
        velocity.Y += Speed;
      }
      if (_currentKeyState.IsKeyDown(Keys.A))
      {
        Facing = LookDirectionsEnum.West;
        velocity.X -= Speed;
      }
      if (_currentKeyState.IsKeyDown(Keys.D))
      {
        Facing = LookDirectionsEnum.East;
        velocity.X += Speed;
      }
      Collision(components);
      Position = new System.Drawing.Point((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y));
      CollisionPosition = new System.Drawing.Point(
        (int)Math.Round(CollisionPosition.X + velocity.X),
        (int)Math.Round(CollisionPosition.Y + velocity.Y)
      );
    }
    public override Rectangle GetCollision()
    {
      return new Rectangle(
        CollisionPosition.X,
        CollisionPosition.Y,
        CollisionSize.Width,
        CollisionSize.Height);
    }

    private void PerformAttack(GameTime gameTime, List<BaseCharacter> characters, List<BaseObject> objects)
    {
      if (CharacterState != CharacterStatesEnum.Alive) return;
      MeleeAttack.Update(gameTime, objects);
      if (_currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released && 
        !UIHandler.IsHoveredOver)
      {
        MeleeAttack.Cast(characters, this);
      }
      NurturingWinds.Update(gameTime, objects);
      if (_currentKeyState.IsKeyDown(NurturingWinds.KeyBind.Value) && _lastKeyState.IsKeyUp(NurturingWinds.KeyBind.Value) && Level >= 4)
      {
        NurturingWinds.Cast(characters, this);
      }
      Fireball.Update(gameTime, objects);
      if (_currentKeyState.IsKeyDown(Fireball.KeyBind.Value) && _lastKeyState.IsKeyUp(Fireball.KeyBind.Value) && Level >= 2)
      {
        (Fireball as RangedMagicAttack).Cast(_graphicsDevice, characters, this, new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2),
          _currentMouseState.Position.ToVector2());
      }
      CharacterManager.HandleDeadCharacters(characters);
    }

    private void Collision(List<BaseObject> components)
    {
      foreach (BaseObject comp in components)
      {
        if (comp == this || !comp.IsCollidable)
          continue;
        if (comp.FileType == FileTypesEnum.WorldObject.ToString() &&
          Position.Y > comp.Position.Y && Position.Y + Size.Height < comp.Position.Y + comp.Size.Height &&
          Position.Y < comp.Position.Y + comp.Size.Height && Position.Y + Size.Height > comp.Position.Y &&
          Position.X >= comp.Position.X && Position.X + Size.Width <= comp.Position.X + comp.Size.Width)
        {
          comp.Alpha = .45f;
        }
        else
        {
          comp.Alpha = 1f;
        }

        if (velocity.X > 0 && IsCollidingLeft(comp.GetCollision()) || velocity.X < 0 && IsCollidingRight(comp.GetCollision()))
          velocity.X = 0;
        if (velocity.Y > 0 && IsCollidingTop(comp.GetCollision()) || velocity.Y < 0 && IsCollidingBottom(comp.GetCollision()))
          velocity.Y = 0;
      }
    }

    private void HandleLevel()
    {
      if (CurrentExperience >= ExperienceNeeded)
      {
        CurrentExperience -= ExperienceNeeded;
        Level++;
        ExperienceNeeded = (int)Math.Pow(Level * 100, 1.1);
      }
    }

    private void SetCurrentStates()
    {
      _currentMouseState = Mouse.GetState();
      _currentKeyState = Keyboard.GetState();
    }
    private void SetLastStates()
    {
      _lastMouseState = _currentMouseState;
      _lastKeyState = _currentKeyState;
    }

    public override void Update(GameTime gameTime, List<BaseObject> components)
    {
      //if (CharacterState != CharacterStatesEnum.Alive) return;
      SetCurrentStates();

      ZIndex = Position.Y + Size.Height;
      BonusStats = EquipmentManager.AddStats();
      SetStats();
      Move(components);
      PerformAttack(gameTime, components.Where(x => x.FileType == FileTypesEnum.Character.ToString()).Cast<BaseCharacter>().ToList(), components);
      HandleLevel();
      _healthBar.Update(gameTime, MaxHealthPoints, CurrentHealthPoints);
      _resourceBar.Update(gameTime, MaxResourcePoints, CurrentResourcePoints);
      if (_resourceTimer.Count(gameTime) == 0)
      {
        if (CurrentResourcePoints < MaxResourcePoints) AddResource();
        else CurrentResourcePoints = MaxResourcePoints;
        _resourceTimer.Reset();
      }
      HandleInteraction(components.Where(x => x.GetType() == typeof(WorldObject) || x.GetType() == typeof(BaseCharacter)).ToList());
      HandleDeath();

      SetLastStates();
    }

    private void SetStats()
    {
      AttackDamage = (BaseStats.AttackDamage.Item1 + BonusStats.AttackDamage.Item1, 
        BaseStats.AttackDamage.Item2 + BonusStats.AttackDamage.Item2);
      MaxHealthPoints = BaseStats.HealthPoints + BonusStats.HealthPoints;
      MaxResourcePoints = BaseStats.ManaPoints + BonusStats.ManaPoints;
      ManaInterval = BaseStats.ManaInterval + BonusStats.ManaInterval;
    }
    private void HandleInteraction(List<BaseObject> components)
    {
      if (CharacterState != CharacterStatesEnum.Alive) return;
      Vector2 mousePosition = Vector2.Transform(_currentMouseState.Position.ToVector2(), Matrix.Invert(Camera.Transform));
      foreach (BaseObject baseObject in components)
      {
        if (Vector2.Distance(new Vector2(Position.X, Position.Y), new Vector2(baseObject.Position.X, baseObject.Position.Y)) > 64) 
          continue;
        if (baseObject.GetType() == typeof(WorldObject) && (baseObject as WorldObject).CursorIntersects(mousePosition) &&
          _currentMouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released)
        {
          (baseObject as WorldObject).Interact(this);
        }
        if(baseObject.GetType() == typeof(BaseCharacter) && (baseObject as BaseCharacter).CursorIntersects(mousePosition) &&
          _currentMouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released)
        {
          (baseObject as BaseCharacter).Interact(this);
        }
      }
    }
    private void HandleDeath()
    {
      if (CharacterState != CharacterStatesEnum.Alive)
      {
        RespawnWindow.IsShown = true;
      }
    }

    public override void Draw(ContentManager content, GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(content, gameTime, spriteBatch);
    }
    public void DrawAbilities(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      Fireball.Draw(gameTime, spriteBatch, graphicsDevice);
    }
    public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      if (_healthBar == null) return;
      _healthBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y), Color.GreenYellow);
      _resourceBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y));
    }

    public override string ToString()
    {
      return $"Level: {Level}\n" +
        $"Health: {CurrentHealthPoints}/{MaxHealthPoints}\n" +
        $"{ResourceType}: {CurrentResourcePoints}/{MaxResourcePoints}\n" +
        $"Attack Damage: {AttackDamage.Item1} - {AttackDamage.Item2}\n" +
        $"Attack Range: {(AttackRange == 0 ? "Melee" : AttackRange)}\n" +
        $"Mana per five seconds: {ManaInterval}\n\n\n" +
        $"Quests accepted: {Quests.Count}";
    }
  }
}
