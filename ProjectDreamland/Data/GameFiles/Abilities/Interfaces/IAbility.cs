using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Abilities.Interfaces
{
  public interface IAbility
  {
    void Cast(List<BaseCharacter> characters, BaseCharacter caster);
    List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster);
    void Update(GameTime gameTime, List<BaseObject> components);
    void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
  }
}
