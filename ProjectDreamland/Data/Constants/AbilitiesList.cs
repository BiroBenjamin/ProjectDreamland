using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities;

namespace ProjectDreamland.Data.Constants
{
  public static class AbilitiesList
  {
    public static readonly MeleeAttack MeleeAttack = new MeleeAttack(name: "Attack", description: "Deals moderate damage in a close range", 
      resourceType: ResourceTypesEnum.Mana, cost: 0, damage: 20, damageType: DamageTypesEnum.Physical, abilityType: AbilityTypesEnum.Damage, 
      range: 1.5f, cooldown: 1.5f, icon: Game1.Self.Content.Load<Texture2D>("UI/attack_icon"));

    public static readonly RangedMagicAttack Fireball = new RangedMagicAttack(name: "Fireball", description: "Deals fire damage over a long range", 
      resourceType: ResourceTypesEnum.Mana, cost: 30, damage: 30, damageType: DamageTypesEnum.Fire, abilityType: AbilityTypesEnum.Damage, 
      range: 10, cooldown: 5f, icon: Game1.Self.Content.Load<Texture2D>("UI/fireball_icon"),
      projectileTexture: Game1.Self.Content.Load<Texture2D>("Spells/fireball"));

    public static readonly HealAbility NurturingWinds = new HealAbility(name: "Nurturing Winds", description: "Heals the caster's wounds", 
      resourceType: ResourceTypesEnum.Mana, cost: 35, damage: 25, damageType: DamageTypesEnum.Nature, abilityType: AbilityTypesEnum.Heal, 
      range: 0, cooldown: 10f, icon: Game1.Self.Content.Load<Texture2D>("UI/heal_icon"));
  }
}
