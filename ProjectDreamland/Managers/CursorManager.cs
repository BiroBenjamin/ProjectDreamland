using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Components;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.Managers
{
  public static class CursorManager
  {
    private static readonly Texture2D _cursorBase = Game1.Self.Content.Load<Texture2D>("UI/alt_cursor_base");
    private static readonly Texture2D _cursorAttack = Game1.Self.Content.Load<Texture2D>("UI/alt_cursor_attack");
    private static readonly Texture2D _cursorInteract = Game1.Self.Content.Load<Texture2D>("UI/alt_cursor_interact");
    private static readonly Texture2D _cursorTalk = Game1.Self.Content.Load<Texture2D>("UI/alt_cursor_talk");

    private static Texture2D _currentCursor;

    public static void Initialize()
    {
      _currentCursor = _cursorBase;
    }

    public static void Update(GameTime gameTime, List<BaseObject> components)
    {
      MouseState mouseState = Mouse.GetState();
      Vector2 mousePosition = Vector2.Transform(mouseState.Position.ToVector2(), Matrix.Invert(Camera.Transform));
      foreach (WorldObject comp in components.Where(x => x.GetType() == typeof(WorldObject)))
      {
        if (comp.IsInteractable && comp.CursorIntersects(mousePosition))
        {
          _currentCursor = _cursorInteract;
        }
        else
        {
          _currentCursor = _cursorBase;
        }
      }
      foreach(BaseCharacter comp in components.Where(x => x.GetType() == typeof(BaseCharacter))){
        if (comp.CharacterAffiliation == Data.Enums.CharacterAffiliationsEnum.Friendly && 
          comp.Quest != null && comp.CursorIntersects(mousePosition))
        {
          _currentCursor = _cursorTalk;
        }
        else if (comp.CharacterAffiliation == Data.Enums.CharacterAffiliationsEnum.Hostile &&
          comp.CursorIntersects(mousePosition))
        {
          _currentCursor = _cursorAttack;
        }
        else
        {
          _currentCursor = _cursorBase;
        }
      }
    }
    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_currentCursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
    }
  }
}
