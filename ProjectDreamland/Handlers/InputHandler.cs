using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.UI.Character;
using ProjectDreamland.UI.Inventory;

namespace ProjectDreamland.Handlers
{
    public class InputHandler
  {
    private KeyboardState _currentKeyState;
    private KeyboardState _lastKeyState;

    public InputHandler() { }

    public void Update(GameTime gameTime)
    {
      _currentKeyState = Keyboard.GetState();

      if (_currentKeyState.IsKeyDown(KeyBinds.CloseAndEsc) && _lastKeyState.IsKeyUp(KeyBinds.CloseAndEsc))
      {
        Actions.EscPressed();
      }
      if (_currentKeyState.IsKeyDown(KeyBinds.Inventory) && _lastKeyState.IsKeyUp(KeyBinds.Inventory))
      {
        InventoryWindow.IsShown = !InventoryWindow.IsShown;
      }
      if (_currentKeyState.IsKeyDown(KeyBinds.Character) && _lastKeyState.IsKeyUp(KeyBinds.Character))
      {
        CharacterWindow.IsShown = !CharacterWindow.IsShown;
      }

      _lastKeyState = _currentKeyState;
    }
  }
}
