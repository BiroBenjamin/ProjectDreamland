using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities.Interfaces;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Abilities
{
    public class RangedMagicAttack : BaseAbility
  {
    private List<Projectile> _projectiles;
    private Texture2D _projectileTexture;

    public RangedMagicAttack(string name, string description, ResourceTypesEnum resourceType, int cost, int damage, DamageTypesEnum damageType, 
      AbilityTypesEnum abilityType, float range, float cooldown, Texture2D projectileTexture, Texture2D icon = null) :
      base(name, description, resourceType, cost, damage, damageType, abilityType, range, cooldown, icon)
    {
      _projectiles = new List<Projectile>();
      _projectileTexture = projectileTexture;
      KeyBind = KeyBinds.AbilityThree;
    }

    public void Cast(GraphicsDevice graphicsDevice, List<BaseCharacter> characters, BaseCharacter caster, Vector2 startPosition, Vector2 endPosition)
    {
      if (!CanCast) return;
      base.Cast(characters, caster);

      if(caster.CurrentResourcePoints < Cost) return;

      Random rand = new Random();
      (int, int) damageRange = ((int)(Damage * .8f + caster.AttackDamage.Item1 * .8f), (int)(Damage * 1.5f + caster.AttackDamage.Item2 * 1.5f));
      int damageDone = rand.Next(damageRange.Item1, damageRange.Item2 + 1);
      characters.Remove(caster);
      Projectile projectile = new Projectile(graphicsDevice, characters, damageDone, startPosition, endPosition, 10f, 6f, _projectileTexture);
      _projectiles.Add(projectile);
      projectile.Start();
      caster.CurrentResourcePoints -= Cost;
    }
    public override List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster)
    {
      return null;
    }

    public override void Update(GameTime gameTime, List<BaseObject> components)
    {
      base.Update(gameTime, components);
      if (_projectiles.Count <= 0 || _projectiles == null) return;
      List<Projectile> removableProjectiles = new List<Projectile>();
      foreach (Projectile projectile in _projectiles)
      {
        projectile.Update(gameTime, components);
        if (!projectile.IsStarted)
        {
          removableProjectiles.Add(projectile);
        }
      }
      foreach(Projectile projectile in removableProjectiles)
      {
        _projectiles.Remove(projectile);
      }
      removableProjectiles = null;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      base.Draw(gameTime, spriteBatch, graphicsDevice);
      if (_projectiles.Count <= 0 || _projectiles == null) return;
      foreach (Projectile projectile in _projectiles)
      {
        projectile.Draw(gameTime, spriteBatch);
      }
    }
  }
}
