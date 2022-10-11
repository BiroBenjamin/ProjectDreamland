using Microsoft.Xna.Framework;
using ProjectDreamland.Core;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities.Interfaces;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System;
using System.Collections.Generic;

namespace ProjectDreamland.Data.GameFiles.Abilities
{
  public class HealAbility : BaseAbility, IAbility
  {
    public HealAbility(string name, string description, ResourceTypesEnum resourceType, int cost, int damage,
      DamageTypesEnum damageType, float range, float cooldown, bool triggersInternalCooldown) :
      base(name, description, resourceType, cost, damage, damageType, range, cooldown, triggersInternalCooldown) { }

    public override void Cast(List<BaseCharacter> characters, BaseCharacter caster)
    {
      if (!CanCast) return;
      base.Cast(characters, caster);

      if(caster.CurrentResourcePoints < Cost)
      {
        return;
      }

      Random rand = new Random();
      (int, int) healRange = ((int)(Damage * 0.7f), (int)(Damage * 1.3f));
      int healingDone = -rand.Next(healRange.Item1, healRange.Item2 + 1);
      caster.TakeDamage(healingDone);
      caster.CurrentResourcePoints -= Cost;
    }
    public override List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster)
    {
      return null;
    }

    public override void Update(GameTime gameTime, List<BaseObject> components)
    {
      base.Update(gameTime, components);
    }
  }
}
