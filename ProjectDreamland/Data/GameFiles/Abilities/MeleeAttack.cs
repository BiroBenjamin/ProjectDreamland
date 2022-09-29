﻿using Microsoft.Xna.Framework;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities.Interfaces;
using ProjectDreamland.Data.GameFiles.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Data.GameFiles.Abilities
{
  public class MeleeAttack : BaseAbility, IAbility
  {
    public MeleeAttack(string name, string description, ResourceTypesEnum resourceType, int cost, int damage, 
      DamageTypesEnum damageType, float range, float cooldown, bool triggersInternalCooldown) : 
      base(name, description, resourceType, cost, damage, damageType, range, cooldown, triggersInternalCooldown) { }

    public override void Cast(List<BaseCharacter> characters, BaseCharacter caster)
    {
      if (!CanCast) return;
      base.Cast(characters, caster);

      List<BaseCharacter> targets = GetTargets(characters, caster);
      foreach (BaseCharacter target in targets)
      {
        Random rand = new Random();
        (int, int) damageRange = ((int)(Damage * 0.8f), (int)(Damage * 1.2f));
        int damageDone = rand.Next(damageRange.Item1, damageRange.Item2 + 1);
        target.TakeDamage(damageDone);
      }
    }
    public override List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster)
    {
      List<BaseCharacter> targets = new List<BaseCharacter>();
      int calculatedAttackRange = (int)Range * 32;
      switch (caster.Facing)
      {
        case LookDirectionsEnum.North:
          caster.AttackBounds = new Rectangle(
            caster.CollisionPosition.X - (calculatedAttackRange * 2 - caster.CollisionSize.Width) / 2,
            caster.CollisionPosition.Y - calculatedAttackRange,
            calculatedAttackRange * 2,
            calculatedAttackRange
          );
          break;
        case LookDirectionsEnum.South:
          caster.AttackBounds = new Rectangle(
            caster.CollisionPosition.X - (calculatedAttackRange * 2 - caster.CollisionSize.Width) / 2,
            caster.CollisionPosition.Y + caster.CollisionSize.Height,
            calculatedAttackRange * 2,
            calculatedAttackRange
          );
          break;
        case LookDirectionsEnum.West:
          caster.AttackBounds = new Rectangle(
            caster.CollisionPosition.X - calculatedAttackRange,
            caster.CollisionPosition.Y - (calculatedAttackRange * 2 - caster.CollisionSize.Height) / 2,
            calculatedAttackRange,
            calculatedAttackRange * 2
          );
          break;
        case LookDirectionsEnum.East:
          caster.AttackBounds = new Rectangle(
            caster.CollisionPosition.X + caster.CollisionSize.Width,
            caster.CollisionPosition.Y - (calculatedAttackRange * 2 - caster.CollisionSize.Height) / 2,
            calculatedAttackRange,
            calculatedAttackRange * 2
          );
          break;
      }
      targets = characters.Where(x => x.GetCollision().Intersects(caster.AttackBounds)).ToList();

      return targets;
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }
  }
}