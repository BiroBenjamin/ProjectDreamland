using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data;
using ProjectDreamland.UI.Menu.CharacterMenu;
using ProjectDreamland.UI.Menu.OptionsMenu;

namespace ProjectDreamland.UI.Menu
{
  public class MenuPanel
  {
    public static bool IsShown { get; set; }

    private int _screenWidth = Game1.ScreenWidth;
    private int _screenHeight = Game1.ScreenHeight;
    private KeyboardState _currentKeyboardState;
    private KeyboardState _prewiousKeyboardState;
    private ContentManager _content;

    private Texture2D _background;
    private Rectangle _backgroundBounds;

    private OptionsMenuPanel _optionsMenu;
    private CharacterMenuPanel _characterMenu;

    public MenuPanel(ContentManager content)
    {
      _content = content;
      LoadContent();
      _backgroundBounds = new Rectangle(
        (int)(_screenWidth - _background.Width * 1.5) / 2, 
        (int)(_screenHeight - _background.Height * 1.5) / 2,
        (int)(_background.Width * 1.5), 
        (int)(_background.Height * 1.5));
      _optionsMenu = new OptionsMenuPanel(content, _backgroundBounds, true);

    }

    private void LoadContent()
    {
      _background = _content.Load<Texture2D>(ImagePaths.MenuPanelBackground);
    }

    private void HandleKeyboardInput()
    {
      _currentKeyboardState = Keyboard.GetState();
      if(_currentKeyboardState.IsKeyDown(Keys.Escape) && _prewiousKeyboardState.IsKeyUp(Keys.Escape))
      {
        IsShown = !IsShown;
      }
      _prewiousKeyboardState = _currentKeyboardState;
    }

    public void Update(GameTime gameTime)
    {
      HandleKeyboardInput();
      if (!IsShown) return;
      _optionsMenu.Update(gameTime);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      if (!IsShown) return;
      spriteBatch.Draw(_background, _backgroundBounds, Color.White);
      _optionsMenu.Draw(gameTime, spriteBatch);
    }

  }
}
