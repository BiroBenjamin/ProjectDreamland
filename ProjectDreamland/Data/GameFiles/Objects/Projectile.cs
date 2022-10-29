using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Components;
using ProjectDreamland.Data.GameFiles.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Objects
{
  internal class Projectile : BaseObject
  {
    public bool IsStarted;

    private int _damage;
    private Vector2 _origin;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private float _range;
    private float _speed;
    List<BaseCharacter> _characters;
    Vector2 _direction;
    private float _rotation;

    public Projectile(GraphicsDevice graphicsDevice, List<BaseCharacter> characters, int damage, Vector2 startPosition, Vector2 endPosition, 
      float range, float speed, Texture2D texture)
    {
      IsStarted = false;
      _damage = damage;
      _origin = startPosition;
      _startPosition = Vector2.Add(startPosition, Vector2.One);
      _endPosition = Vector2.Transform(endPosition, Matrix.Invert(Camera.Transform));
      _range = range * 32;
      _speed = speed;
      _characters = characters.Where(x => x.CharacterAffiliation != Enums.CharacterAffiliationsEnum.Friendly).ToList();
      Texture = texture;
      Vector2 difference = Vector2.Subtract(_startPosition, _endPosition);
      _direction = Vector2.Normalize(difference);
    }

    public void Start()
    {
      IsStarted = true;
    }

    public override void Update(GameTime gameTime, List<BaseObject> components)
    {
      if (!IsStarted) return;
      _rotation = (float)Math.Atan2(_direction.Y, _direction.X);
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
      foreach (WorldObject comp in components.Where(x => x.GetType() == typeof(WorldObject)))
      {
        if (comp.GetCollision().Intersects(collision))
        {
          IsStarted = false;
        }
      }
    }
    private void Move(GameTime gameTime)
    {
      if (Vector2.Distance(_startPosition, _origin) < _range)
      {
        _startPosition -= _direction * _speed;
      }
      else
      {
        IsStarted = false;
      }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (!IsStarted) return;
      spriteBatch.Draw(Texture, new Rectangle((int)_startPosition.X, (int)_startPosition.Y, (int)(Texture.Width * 1.5f), (int)(Texture.Height * 1.5f)), 
        null, Color.DarkOrange, _rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0f);
    }
  }
}
