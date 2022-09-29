using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Abilities.Interfaces
{
  public interface IAbility
  {
    void Cast(List<BaseCharacter> characters, BaseCharacter caster);
    List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster);
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
  }
}
