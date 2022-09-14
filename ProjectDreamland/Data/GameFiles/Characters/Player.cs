using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Characters
{
  public class Player : BaseCharacter
  {
    new public float Speed { get; set; } = 3.5f;
    private KeyboardState keyState = Keyboard.GetState();
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

    private void Move(GameTime gameTime, List<BaseObject> components)
    {
      keyState = Keyboard.GetState();
      velocity = new Vector2();

      // Getting player movement input
      if (keyState.IsKeyDown(Keys.W))
        velocity.Y -= Speed;
      if (keyState.IsKeyDown(Keys.S))
        velocity.Y += Speed;
      if (keyState.IsKeyDown(Keys.A))
        velocity.X -= Speed;
      if (keyState.IsKeyDown(Keys.D))
        velocity.X += Speed;
      Collision(components);
      Position = new System.Drawing.Point((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y));
    }
    private void Collision(List<BaseObject> components)
    {
      foreach (BaseObject comp in components)
      {
        if (comp == this || !comp.IsCollidable)
          continue;
        if (Position.Y > comp.Position.Y && Position.Y + Size.Height < comp.Position.Y + comp.Size.Height &&
          Position.Y < comp.Position.Y + comp.Size.Height && Position.Y + Size.Height > comp.Position.Y &&
          Position.X >= comp.Position.X && Position.X + Size.Width <= comp.Position.X + comp.Size.Width)
        {
          comp.Alpha = .45f;
        }
        else 
        { 
          comp.Alpha = 1f; 
        }

        if (velocity.X > 0 && IsCollidingLeft(comp) || velocity.X < 0 && IsCollidingRight(comp))
          velocity.X = 0;
        if (velocity.Y > 0 && IsCollidingTop(comp) || velocity.Y < 0 && IsCollidingBottom(comp))
          velocity.Y = 0;
      }
    }

    public void Update(GameTime gameTime, List<BaseObject> components)
    {
      base.Update(gameTime);
      Move(gameTime, components);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(gameTime, spriteBatch);
    }
  }
}
