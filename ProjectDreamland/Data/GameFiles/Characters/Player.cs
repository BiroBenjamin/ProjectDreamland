using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Core;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Data.GameFiles.Quests;
using ProjectDreamland.Managers;
using ProjectDreamland.UI;
using ProjectDreamland.UI.QuestPanel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  public class Player : BaseCharacter
  {
    new public float Speed { get; set; } = 2f;
    public int ExperienceNeeded { get; set; }
    public int CurrentExperience { get; set; }

    private GraphicsDevice _graphicsDevice;

    private BaseAbility _ability1;
    private BaseAbility _ability2;
    private BaseAbility _ability3;

    private KeyboardState _currentKeyState;
    private KeyboardState _lastKeyState;
    private MouseState _currentMouseState;
    private MouseState _lastMouseState;

    private Map _currentMap;
    public Player(GraphicsDevice graphicsDevice, Texture2D texture, Map currentMap) : base(texture)
    {
      Size = new System.Drawing.Size(texture.Width, texture.Height);
      SetCollision(Position.X, Position.Y, texture.Width, texture.Height);
      _graphicsDevice = graphicsDevice;
      _currentMap = currentMap;
      SetStatus();
    }
    public Player(GraphicsDevice graphicsDevice, Texture2D texture, Map currentMap, int x, int y) : base(texture)
    {
      Size = new System.Drawing.Size(texture.Width, texture.Height);
      Position = new System.Drawing.Point(x, y);
      SetCollision(Position.X, Position.Y, texture.Width, texture.Height);
      _graphicsDevice = graphicsDevice;
      _currentMap = currentMap;
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
      MaxHealthPoints = 200;
      CurrentHealthPoints = 125;
      Level = 4;
      ResourceType = ResourceTypesEnum.Mana.ToString();
      MaxResourcePoints = 238;
      CurrentResourcePoints = 238;
      _resourceBar = new ResourceBar(ResourceType);
      ExperienceNeeded = (int)Math.Pow(Level * 100, 1.1);
      _ability1 = new MeleeAttack("Attack", "Very big damage", ResourceTypesEnum.None, 0, 20,
        DamageTypesEnum.Physical, 1.5f, .5f, true);
      _ability2 = new RangedMagicAttack("Magic Attack", "Very big magic damage", ResourceTypesEnum.Mana, 30, 30,
        DamageTypesEnum.Fire, 10, 5f, true);
      _ability3 = new HealAbility("Heal", "Very big heal", ResourceTypesEnum.Mana, 35, 25,
        DamageTypesEnum.Nature, .5f, 5f, true);

      QuestManager.Quests.AddRange(new List<Quest>()
      {
        new Quest("Monke dead", "You have to kill these monkes!!", "kill", 520, new Objective(_currentMap.Characters.FirstOrDefault(), 2)),
      });
    }

    private void Move(List<BaseObject> components)
    {
      _currentKeyState = Keyboard.GetState();
      velocity = new Vector2();

      // Getting player movement input
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
      SetCurrentStates();
      _ability1.Update(gameTime, objects);
      if (_currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
      {
        _ability1.Cast(characters, this);
      }
      _ability2.Update(gameTime, objects);
      if (_currentKeyState.IsKeyDown(Keys.E) && _lastKeyState.IsKeyUp(Keys.E) && Level >= 4)
      {
        (_ability2 as RangedMagicAttack).Cast(_graphicsDevice, characters, this, new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2),
          _currentMouseState.Position.ToVector2());
      }
      _ability3.Update(gameTime, objects);
      if (_currentKeyState.IsKeyDown(Keys.Q) && _lastKeyState.IsKeyUp(Keys.Q) && Level >= 2)
      {
        _ability3.Cast(characters, this);
      }
      CharacterManager.HandleDeadCharacters(characters, this);
      SetLastStates();
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
      if(CurrentExperience >= ExperienceNeeded)
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
      ZIndex = Position.Y + Size.Height;
      Move(components);
      PerformAttack(gameTime, components.Where(x => x.FileType == FileTypesEnum.Character.ToString()).Cast<BaseCharacter>().ToList(), components);
      HandleLevel();
      _healthBar.Update(gameTime, MaxHealthPoints, CurrentHealthPoints);
      _resourceBar.Update(gameTime, MaxResourcePoints, CurrentResourcePoints);
      _timer--;
      if (_timer == 0)
      {
        if (CurrentResourcePoints < MaxResourcePoints) AddResource();
        else CurrentResourcePoints = MaxResourcePoints;
        SetTimer();
      }
      HandleInteraction(components.Where(x => x.GetType() == typeof(WorldObject)).Cast<WorldObject>().ToList());
      HandleQuests();
    }
    private void HandleInteraction(List<WorldObject> components)
    {
      SetCurrentStates();
      Vector2 mousePosition = Vector2.Transform(_currentMouseState.Position.ToVector2(), Matrix.Invert(Camera.Transform));
      foreach(WorldObject worldObject in components)
      {
        if (worldObject.CursorIntersects(mousePosition) &&
          _currentMouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released)
        {
          worldObject.Interact(this);
        }
      }
      SetLastStates();
    }
    private void HandleQuests()
    {
      List<Quest> questsToRemove = new List<Quest>();
      foreach(Quest quest in QuestManager.Quests)
      {
        if (quest.IsDone)
        {
          CurrentExperience += quest.RewardExp;
          questsToRemove.Add(quest);
        }
      }
      foreach (Quest quest in questsToRemove)
      {
        QuestManager.Quests.Remove(quest);
      }
    }

    public override void Draw(ContentManager content, GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(content, gameTime, spriteBatch);
    }
    public void DrawAbilities(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      _ability2.Draw(gameTime, spriteBatch, graphicsDevice);
    }
    public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      if (_healthBar == null) return;
      _healthBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y), Color.GreenYellow);
      _resourceBar.Draw(gameTime, spriteBatch, graphicsDevice, new Vector2(Position.X + Size.Width / 2, Position.Y));
    }
  }
}
