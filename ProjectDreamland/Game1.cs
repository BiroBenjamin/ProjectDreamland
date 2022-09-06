using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ProjectDreamland.Sprites;
using ProjectDreamland.Core;
using ProjectDreamland.Data;
using System.Diagnostics;
using ProjectDreamland.UI.Menu;

namespace ProjectDreamland
{
  public class Game1 : Game
  {
    public static Game1 Self { get; set; }

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera;
    private List<Sprite> _components;
    private Player _player;
    private MenuPanel _characterPanel;

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

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      //Load UI
      _characterPanel = new MenuPanel(Content);

      // Load camera
      _camera = new Camera();

      // Loading the player character
      _player = new Player(Content.Load<Texture2D>("CharacterFrontBluePantsPurpleShirt"));

      _components = new List<Sprite>();
      // Create Map Manager and load map0
      /*mapManager = new FileManager(@"map/map0.map");
      map0 = new Map();
      map0 = (Map)mapManager.Read();
      map0.Initialize(Content);
      foreach (Sprite mapAsset in map0.Components) 
      {
          _components.Add(mapAsset);
          //Debug.WriteLine(mapAsset.Position);
      }*/
      // Load components
      _components.Add(new Sprite(Content.Load<Texture2D>("ShopFood1"), true) { Position = new Vector2(50, 50) });
      _components.Add(_player);
    }

    protected override void Update(GameTime gameTime)
    {
      /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();*/

      // Update every component
      foreach(Sprite comp in _components) 
      {
        comp.Update(gameTime, _components);
      }
      _camera.Follow(_player);
      _characterPanel.Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _spriteBatch.Begin(transformMatrix: _camera.Transform);

      // Draw terrain
      /*foreach (Sprite ter in _terrain)
          ter.Draw(gameTime, _spriteBatch);*/
      // Draw every component
      foreach (Sprite comp in _components)
        comp.Draw(gameTime, _spriteBatch);

      _spriteBatch.End();

      _spriteBatch.Begin();
      _characterPanel.Draw(gameTime, _spriteBatch);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
