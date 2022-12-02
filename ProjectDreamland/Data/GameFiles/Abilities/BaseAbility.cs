using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Components;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.Data.GameFiles.Abilities.Interfaces;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
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
    public AbilityTypesEnum AbilityType { get; set; }
    public float Range { get; set; }
    public float Cooldown { get; set; }
    public float RemainingCooldown { get; set; }
    public Texture2D Icon { get; set; }
    public bool CanCast { get; set; }
    public Timer Timer { get; set; }
    public Keys? KeyBind { get; set; }

    public BaseAbility(string name, string description, ResourceTypesEnum resourceType, int cost, int damage, DamageTypesEnum damageType,
      AbilityTypesEnum abilityType, float range, float cooldown, Texture2D icon)
    {
      Name = name;
      Description = description;
      ResourceType = resourceType;
      Cost = cost;
      Damage = damage;
      DamageType = damageType;
      AbilityType = abilityType;
      Range = range;
      Cooldown = cooldown;
      Icon = icon;
      CanCast = true;
      Timer = new Timer(cooldown);
    }

    public virtual void Update(GameTime gameTime, List<BaseObject> components)
    {
      if (!CanCast) RemainingCooldown = Timer.Count(gameTime);
      if (RemainingCooldown == 0)
      {
        Timer.Reset();
        CanCast = true;
      }
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) { }

    public virtual void Cast(List<BaseCharacter> characters, BaseCharacter caster)
    {
      CanCast = false;
    }
    public virtual void Cast(BaseCharacter target, BaseCharacter caster)
    {
      CanCast = false;
    }

    public virtual List<BaseCharacter> GetTargets(List<BaseCharacter> characters, BaseCharacter caster) { return null; }

    public override string ToString()
    {
      return $"{Name}\nCost: {Cost} - {ResourceType}\n{AbilityType}: {Damage} - {DamageType}\nRange: {(Range > 0 ? Range : "Self")}\nCooldown: {Cooldown}sec\n{Description}";
    }
  }
}
