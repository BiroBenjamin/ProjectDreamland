using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ProjectDreamland.Sprites;
using ProjectDreamland.Core;
using ProjectDreamland.UI.Menu;
using ProjectDreamland.UI.Debug;
using ProjectDreamland.Managers;
using ProjectDreamland.Data.GameFiles;

namespace ProjectDreamland
{
  public class Game1 : Game
  {
    public static Game1 Self { get; set; }

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera;
    private DebugWindow _debugWindow;
    private List<Sprite> _components;
    private Player _player;
    private MenuPanel _characterPanel;
    private Map _currentMap;

    public static int ScreenWidth { get; set; }
    public static int ScreenHeight { get; set; }

    public Game1()
    {
      Self = this;
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      // Initialize the window data
      _graphics.IsFullScreen = false;
      _graphics.PreferredBackBufferWidth = 1280;
      _graphics.PreferredBackBufferHeight = 720;
      _graphics.ApplyChanges();

      ScreenHeight = _graphics.PreferredBackBufferHeight;
      ScreenWidth = _graphics.PreferredBackBufferWidth;

      SystemPrefsManager.SetUpSystemPrefs();

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      //Load UI
      DebugManager.ShowWindow(GraphicsDevice, ScreenWidth, ScreenHeight);
      _characterPanel = new MenuPanel(Content);

      // Load camera
      _camera = new Camera();

      // Loading the player character
      _player = new Player(Content.Load<Texture2D>("CharacterFrontBluePantsPurpleShirt"));

      _components = new List<Sprite>();
      // Create Map Manager and load map0
      MapManager.LoadMaps(Content);
      _currentMap = MapManager.Maps[0];

      // Load components
      _components.Add(new Sprite(Content.Load<Texture2D>("ShopFood1"), true) { Position = new Vector2(50, 50) });
      _components.Add(_player);
    }

    protected override void Update(GameTime gameTime)
    {
      // Update every component
      foreach(Sprite comp in _components) 
      {
        comp.Update(gameTime, _components);
      }
      _camera.Follow(_player);
      _characterPanel.Update(gameTime);
      DebugManager.Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _spriteBatch.Begin(transformMatrix: _camera.Transform);

      // Draw terrain
      foreach(Tile file in _currentMap.Tiles)
      {
        _spriteBatch.Draw(file.Texture, 
          new Rectangle(file.Position.X, file.Position.Y, file.Size.Width, file.Size.Height), Color.White);
      }
      // Draw every component
      foreach (Sprite comp in _components)
        comp.Draw(gameTime, _spriteBatch);

      _spriteBatch.End();

      _spriteBatch.Begin();
      DebugManager.Draw(gameTime, _spriteBatch, Content);
      _characterPanel.Draw(gameTime, _spriteBatch);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
