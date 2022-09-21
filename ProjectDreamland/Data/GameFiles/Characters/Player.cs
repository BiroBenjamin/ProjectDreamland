using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  public class Player : BaseCharacter
  {
    new public float Speed { get; set; } = 3.5f;

    private KeyboardState _keyState;
    private MouseState _currentMouseState;
    private MouseState _lastMouseState;
    public Rectangle attackBounds = new Rectangle();
    public Player(Texture2D texture) : base(texture)
    {
      Size = new System.Drawing.Size(texture.Width, texture.Height);
      CollisionSize = new System.Drawing.Size(texture.Width, texture.Height / 4);
      CollisionPosition = new System.Drawing.Point(0, texture.Height - texture.Height / 4);
    }
    public Player(Texture2D texture, int x, int y) : base(texture)
    {
      Size = new System.Drawing.Size(texture.Width, texture.Height);
      Position = new System.Drawing.Point(x, y);
      CollisionSize = new System.Drawing.Size(texture.Width - texture.Width / 2, texture.Height / 4);
      CollisionPosition = new System.Drawing.Point(0 + texture.Width / 4, texture.Height - texture.Height / 4);
    }

    private void Move(List<BaseObject> components)
    {
      _keyState = Keyboard.GetState();
      velocity = new Vector2();

      // Getting player movement input
      if (_keyState.IsKeyDown(Keys.W))
      {
        Facing = LookDirectionsEnum.North;
        velocity.Y -= Speed;
      }
      if (_keyState.IsKeyDown(Keys.S))
      {
        Facing = LookDirectionsEnum.South;
        velocity.Y += Speed;
      }
      if (_keyState.IsKeyDown(Keys.A))
      {
        Facing = LookDirectionsEnum.West;
        velocity.X -= Speed;
      }
      if (_keyState.IsKeyDown(Keys.D))
      {
        Facing = LookDirectionsEnum.East;
        velocity.X += Speed;
      }
      Collision(components);
      Position = new System.Drawing.Point((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y));
      CollisionPosition = new System.Drawing.Point((int)(CollisionPosition.X + velocity.X), (int)(CollisionPosition.Y + velocity.Y));
    }
    private void PerformAttack(List<BaseCharacter> characters)
    {
      _currentMouseState = Mouse.GetState();
        List<BaseCharacter> targets = GetTargets(characters);
      if (_currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
      {
        foreach(BaseCharacter target in targets)
        {
          Attack(target);
        }
      }
      _lastMouseState = _currentMouseState;
    }
    private List<BaseCharacter> GetTargets(List<BaseCharacter> characters)
    {
      List<BaseCharacter> targets = new List<BaseCharacter>();
      int calculatedAttackRange = (int)AttackRange * 32;
      switch (Facing)
      {
        case LookDirectionsEnum.North:
          attackBounds = new Rectangle(Position.X, CollisionPosition.Y, Size.Width, calculatedAttackRange);
          break;
        case LookDirectionsEnum.South:
          attackBounds = new Rectangle(Position.X, Position.Y + Size.Height, Size.Width, calculatedAttackRange);
          break;
        case LookDirectionsEnum.West:
          attackBounds = new Rectangle(Position.X - calculatedAttackRange, Position.Y, calculatedAttackRange, Size.Height);
          break;
        case LookDirectionsEnum.East:
          attackBounds = new Rectangle(Position.X + Size.Width, Position.Y, calculatedAttackRange, Size.Height);
          break;
      }
      targets = characters.Where(x => x.GetCollision().Intersects(attackBounds)).ToList();

      return targets;
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

    public void Update(GameTime gameTime, List<BaseObject> components)
    {
      base.Update(gameTime);
      Move(components);
      PerformAttack(components.Where(x => x.FileType == FileTypesEnum.Character.ToString()).Cast<BaseCharacter>().ToList());
    }
    public override void Draw(ContentManager content, GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(content, gameTime, spriteBatch);
    }
  }
}
