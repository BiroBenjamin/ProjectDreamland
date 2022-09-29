using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  internal class Projectile : BaseObject
  {
    public bool IsStarted;

    private int _damage;
    private Vector2 _startPosition;
    private Vector2 _origin;
    private Vector2 _direction;
    private float _range;
    private float _speed;
    private Vector2 _velocity;
    List<BaseCharacter> _characters;

    public Projectile(List<BaseCharacter> characters, int damage, Vector2 startPosition, Vector2 direction, float range, float speed)
    {
      _characters = characters;
      _damage = damage;
      _startPosition = startPosition;
      _origin = startPosition;
      _direction = direction;
      _range = range;
      _speed = speed;
      IsStarted = false;
    }

    public void Start()
    {
      IsStarted = true;
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsStarted) return;
      Move(gameTime);
      Rectangle collision = new Rectangle((int)_startPosition.X, (int)_startPosition.Y, Texture.Width, Texture.Height);
      foreach (BaseCharacter character in _characters)
      {
        if (character.GetCollision().Intersects(collision))
        {
          character.TakeDamage(_damage);
          IsStarted = false;
        }
      }
    }
    private void Move(GameTime gameTime)
    {
      float x = (_direction.X - _startPosition.X) / 100;
      //float y = Math.Abs(_direction.Y - _startPosition.Y) / _speed;
      _velocity = new Vector2(x, 0);
      _startPosition = _direction;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      if (!IsStarted) return;
      if (Texture == null)
      {
        Texture = new Texture2D(graphicsDevice, 1, 1);
        Texture.SetData(new Color[] { Color.Blue });
      }
      spriteBatch.Draw(Texture, new Rectangle((int)_startPosition.X, (int)_startPosition.Y, 16, 16), Color.White);
    }
  }
}
