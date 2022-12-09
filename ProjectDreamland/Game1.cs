using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Managers;
using ProjectDreamland.GameStates;
using System;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Handlers;
using ProjectDreamland.Components;
using Microsoft.Xna.Framework.Content;
using ProjectDreamland.Data.GameFiles.Characters;

namespace ProjectDreamland
{
  public class Game1 : Game
  {
    public static Game1 Self { get; set; }

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static int ScreenWidth { get; set; }
    public static int ScreenHeight { get; set; }

    public Game1()
    {
      Self = this;
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = false;
    }

    protected override void Initialize()
    {
      _graphics.IsFullScreen = true;
      if (_graphics.IsFullScreen)
      {
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
      }
      else
      {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
      }
      _graphics.ApplyChanges();

      ScreenHeight = _graphics.PreferredBackBufferHeight;
      ScreenWidth = _graphics.PreferredBackBufferWidth;

      SystemPrefsManager.SetUpSystemPrefs();

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      GameStateManager.LoadContent();
      CursorManager.Initialize();
    }
    

    protected override void Update(GameTime gameTime)
    {
      if (!IsActive) return;
      GameStateManager.Update(gameTime);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      if (!IsActive) return;
      GraphicsDevice.Clear(Color.Gray);

      GameStateManager.Draw(gameTime, _spriteBatch);
      base.Draw(gameTime);

      _spriteBatch.Begin();
      CursorManager.Draw(gameTime, _spriteBatch);
      _spriteBatch.End();
    }
  }
}
