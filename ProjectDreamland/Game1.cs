using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ProjectDreamland.Core;
using ProjectDreamland.UI.Menu;
using ProjectDreamland.Managers;
using ProjectDreamland.Data.GameFiles;
using ProjectDreamland.Data.GameFiles.Characters;
using System.Linq;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.Handlers;

namespace ProjectDreamland {
  public class Game1 : Game
  {
    public static Game1 Self { get; set; }

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera;
    private RenderHandler _renderHandler;
    private List<BaseObject> _components;
    private List<BaseObject> _renderedComponents;
    private Player _player;
    private MenuPanel _characterPanel;

    private UIHandler _uiHandler;

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
      _graphics.IsFullScreen = true;
      _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
      _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
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

      // Load camera
      _camera = new Camera();
      _renderHandler = new RenderHandler();

      // Loading the player character
      _player = new Player(GraphicsDevice, Content.Load<Texture2D>("Sprites/Characters/CharacterBaseFront"), MapManager.CurrentMap, -64, -64);

      //Load components
      MapManager.LoadMapContent(_player);
      _renderedComponents = new List<BaseObject>();

      _uiHandler = new UIHandler(ScreenWidth, ScreenHeight, _player, Content);
    }
    

    protected override void Update(GameTime gameTime)
    {
      // Update every component
      _renderedComponents = _renderHandler.GetRenderableObjects(MapManager.CurrentMapComponents, _player, new Rectangle(0, 0, ScreenWidth, ScreenHeight));
      _player.Update(gameTime, _renderedComponents);
      foreach (BaseObject comp in _renderedComponents)
      {
        comp.Update(gameTime, _renderedComponents);
      }
      _camera.Follow(_player);
      _characterPanel.Update(gameTime);
      _uiHandler.Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Gray);

      // Draw every component
      _spriteBatch.Begin(transformMatrix: Camera.Transform);
      //Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
      //texture.SetData(new Color[] { Color.Red } );
      foreach (BaseObject comp in _renderedComponents.OrderBy(x => x.ZIndex))
      {
        comp.Draw(Content, gameTime, _spriteBatch);
        if(comp.GetType() == typeof(BaseCharacter) || comp == _player)
        {
          (comp as BaseCharacter).DrawUI(gameTime, _spriteBatch, GraphicsDevice);
        }
        // _spriteBatch.Draw(texture, new Rectangle(comp.GetCollision().X, comp.GetCollision().Y, comp.GetCollision().Width, comp.GetCollision().Height), Color.Red);
      }
      _player.DrawAbilities(gameTime, _spriteBatch, GraphicsDevice);
      _spriteBatch.End();

      //Static elements, like UI
      _spriteBatch.Begin();
      _uiHandler.Draw(gameTime, _spriteBatch, GraphicsDevice);
      _characterPanel.Draw(gameTime, _spriteBatch);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
