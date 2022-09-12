using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ProjectDreamland.Core;
using ProjectDreamland.UI.Menu;
//using ProjectDreamland.UI.Debug;
using ProjectDreamland.Managers;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Characters;
using System.Linq;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Handlers;

namespace ProjectDreamland
{
  public class Game1 : Game
  {
    public static Game1 Self { get; set; }

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera;
    //private DebugWindow _debugWindow;
    private RenderHandler _renderHandler;
    private List<BaseObject> _components;
    private List<BaseObject> _renderedComponents;
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

      // Create Map Manager and load map0
      MapManager.LoadMaps(Content);
      _currentMap = MapManager.Maps[0];

      // Load camera
      _camera = new Camera();
      _renderHandler = new RenderHandler();

      // Loading the player character
      _player = new Player(Content.Load<Texture2D>("Characters/CharacterBaseFront"), 100, 100);

      //Load components
      _components = new List<BaseObject>();
      _components.AddRange(_currentMap.Tiles);
      _components.AddRange(_currentMap.WorldObjects);
      _components.Add(_player);
      _renderedComponents = new List<BaseObject>();
    }

    protected override void Update(GameTime gameTime)
    {
      // Update every component
      _player.Update(gameTime, _components);
      _camera.Follow(_player);
      _characterPanel.Update(gameTime);
      DebugManager.Update(gameTime);
      _renderedComponents = _renderHandler.GetRenderableObjects(_components, _player, new Rectangle(0, 0, ScreenWidth, ScreenHeight));

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Gray);

      _spriteBatch.Begin(transformMatrix: _camera.Transform);

      // Draw every component
      foreach (BaseObject comp in _renderedComponents.OrderBy(x => x.ZIndex))
      {
        comp.Draw(gameTime, _spriteBatch);
      }

      _spriteBatch.End();

      //Static elements, like UI
      _spriteBatch.Begin();
      DebugManager.Draw(gameTime, _spriteBatch, Content);
      _characterPanel.Draw(gameTime, _spriteBatch);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
