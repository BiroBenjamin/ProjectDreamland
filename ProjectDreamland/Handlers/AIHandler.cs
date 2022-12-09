using Microsoft.Xna.Framework;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDreamland.Handlers
{
  public class AIHandler
  {
    private BaseCharacter _baseCharacter;

    public AIHandler(BaseCharacter baseCharacter)
    {
      _baseCharacter = baseCharacter;
    }

    public void Update(GameTime gameTime, List<BaseObject> components)
    {
      if (_baseCharacter.CharacterState != Data.Enums.CharacterStatesEnum.Alive) return;
      List<BaseObject> comps = components.Where(x => x.IsCollidable || x.GetType() == typeof(BaseCharacter)).ToList();
      switch (_baseCharacter.CharacterAffiliation)
      {
        case Data.Enums.CharacterAffiliationsEnum.Hostile:
          HandleEnemyAI(comps);
          break;
      }
    }

    public void HandleEnemyAI(List<BaseObject> components)
    {
      Player player = components.Where(x => x.GetType() == typeof(Player)).Cast<Player>().FirstOrDefault();
      Vector2 targetPosition = new Vector2(player.Position.X + player.Size.Width / 2, player.Position.Y + player.Size.Height / 2);
      Vector2 thisPosition = new Vector2(_baseCharacter.Position.X + _baseCharacter.Size.Width / 2, _baseCharacter.Position.Y + _baseCharacter.Size.Height / 2);
      Vector2 direction = Vector2.Subtract(targetPosition, thisPosition);

      switch (_baseCharacter.BehaviourState)
      {
        case Data.Enums.BehaviourStatesEnum.Chase:
          if (player.CharacterState != Data.Enums.CharacterStatesEnum.Alive) _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Idle;
          HandleChasing(direction, components);
          break;
        case Data.Enums.BehaviourStatesEnum.Attack:
          if (player.CharacterState != Data.Enums.CharacterStatesEnum.Alive) _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Idle;
          HandleAttacking(player, direction);
          break;
        default:
          HandleIdleing(direction);
          break;
      }
    }
    private void HandleIdleing(Vector2 direction)
    {
      if (Math.Abs(direction.X) <= _baseCharacter.AggroRange * 32 && Math.Abs(direction.Y) <= _baseCharacter.AggroRange * 32)
      {
        _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Chase;
        return;
      }
      _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Idle;
    }
    private void HandleChasing(Vector2 direction, List<BaseObject> components)
    {
      if (Math.Abs(direction.X) <= _baseCharacter.AttackRange * 32 && Math.Abs(direction.Y) <= _baseCharacter.AttackRange * 32)
      {
        _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Attack;
        return;
      }
      else if (Math.Abs(direction.X) > _baseCharacter.AggroRange * 32 || Math.Abs(direction.Y) > _baseCharacter.AggroRange * 32)
      {
        _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Idle;
        return;
      }
      direction.Normalize();
      _baseCharacter.Move(direction, components);
    }
    private void HandleAttacking(BaseCharacter target, Vector2 direction)
    {
      if (Math.Abs(direction.X) <= _baseCharacter.AttackRange * 32 && Math.Abs(direction.Y) <= _baseCharacter.AttackRange * 32)
      {
        _baseCharacter.Attack(target, _baseCharacter.MeleeAttack);
        return;
      }
      _baseCharacter.BehaviourState = Data.Enums.BehaviourStatesEnum.Chase;
    }
  }
}
