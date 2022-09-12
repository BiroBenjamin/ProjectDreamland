using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
//using ProjectDreamland.UI.Debug;

namespace ProjectDreamland.Managers
{
  public static class DebugManager
  {
    //private static DebugWindow _debugWindow;
    private static bool _isShown = false;
    private static bool _isDevMode = true;

    public static void ShowWindow(GraphicsDevice graphicsDevice, int screenWidth, int screenHeight)
    {
      if (!_isDevMode) return;
      //_debugWindow = new DebugWindow(graphicsDevice, screenWidth, screenHeight);
      _isShown = true;
    }
    public static void Log(string message)
    {
      if (!_isDevMode) return;
      //_debugWindow.Messages.Add(message);
    }

    public static void Update(GameTime gameTime)
    {
      if (!_isShown) return;
      //_debugWindow.Update(gameTime);
    }
    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      if (!_isShown) return;
      //_debugWindow.Draw(gameTime, spriteBatch, content);
    }
  }
}
