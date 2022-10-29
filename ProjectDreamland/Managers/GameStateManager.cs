using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Enums;
using ProjectDreamland.GameStates;

namespace ProjectDreamland.Managers
{
  public static class GameStateManager
  {
    private static readonly GraphicsDevice _graphicsDevice = Game1.Self.GraphicsDevice;
    private static readonly ContentManager _contentManager = Game1.Self.Content;

    public static PlayState PlayState = new PlayState(_graphicsDevice, _contentManager);
    public static MenuState MenuState = new MenuState(_graphicsDevice, _contentManager);

    public static GameState CurrentState = MenuState;

    public static void ChangeGameState(GameStatesEnum gameState)
    {
      switch (gameState)
      {
        case GameStatesEnum.Menu:
          CurrentState = MenuState;
          CurrentState.LoadContent();
          break;
        case GameStatesEnum.Play:
          CurrentState = PlayState;
          CurrentState.LoadContent();
          break;
      }
    }
    public static void LoadContent()
    {
      CurrentState.LoadContent();
    }

    public static void Update(GameTime gameTime)
    {
      CurrentState.Update(gameTime);
    }
    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      CurrentState.Draw(gameTime, spriteBatch);
    }
  }
}
