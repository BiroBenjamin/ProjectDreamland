using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ProjectDreamland.Sprites;
using ProjectDreamland.Core;
using ProjectDreamland.Data;
using System.Diagnostics;

namespace ProjectDreamland{
    public class Game1 : Game{
        private Map map0;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private List<Sprite> _components;
        private Player _player;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Game1(){
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize(){
            // Initialize the window data
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        protected override void LoadContent(){
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load camera
            _camera = new Camera();

            // Loading the player character
            _player = new Player(Content.Load<Texture2D>("CharacterFrontBluePantsPurpleShirt"));

            _components = new List<Sprite>();
            // Load map0
            map0 = new Map(@"D:\GitRepos\ProjectDreamland\ProjectDreamland\map\map0.pdmap");
            map0.Initialize(Content);
            foreach (Sprite mapAsset in map0.Components) {
                _components.Add(mapAsset);
                //Debug.WriteLine(mapAsset.Position);
            }
            // Load components
            _components.Add(new Sprite(Content.Load<Texture2D>("ShopFood1"), true) { Position = new Vector2(50, 50) });
            _components.Add(_player);
        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update every component
            foreach(Sprite comp in _components) {
                comp.Update(gameTime, _components);
            }
            _camera.Follow(_player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            // Draw terrain
            /*foreach (Sprite ter in _terrain)
                ter.Draw(gameTime, _spriteBatch);*/
            // Draw every component
            foreach (Sprite comp in _components)
                comp.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
