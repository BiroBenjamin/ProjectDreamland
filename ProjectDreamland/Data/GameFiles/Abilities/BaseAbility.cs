using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Core;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities.Interfaces;
using ProjectDreamland.Data.GameFiles.Characters;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Abilities
{
  public class BaseAbility : IAbility
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public ResourceTypesEnum ResourceType { get; set; }
    public int Cost { get; set; }
    public int Damage { get; set; }
    public DamageTypesEnum DamageType { get; set; }
    public float Range { get; set; }
    public float Cooldown { get; set; }
    public bool TriggersInternalCooldown { get; set; }
    public bool CanCast { get; set; }
    protected int _timer;

    public BaseAbility(string name, string description, ResourceTypesEnum resourceType, int cost, int damage,
      DamageTypesEnum damageType, float range, float cooldown, bool triggersInternalCooldown)
    {
      Name = name;
      Description = description;
      ResourceType = resourceType;
      Cost = cost;
      Damage = damage;
      DamageType = damageType;
      Range = range;
      Cooldown = cooldown;
      TriggersInternalCooldown = triggersInternalCooldown;
      CanCast = true;
      _timer = (int)(60 * cooldown);
    }

    public virtual void Update(GameTime gameTime)
    {
      if (!CanCast) _timer--;
      if (_timer == 0)
      {
        CanCast = true;
        _timer = (int)(60 * Cooldown);
      }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) { }

    public virtual void Cast(List<BaseCharacter> characters, BaseCharacter caster)
    {
      CanCast = false;
    }
    public virtual List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster) { return null; }
  }
}
