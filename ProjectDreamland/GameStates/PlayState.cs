using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Core;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.GameStates;
using ProjectDreamland.Handlers;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Menu;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.GameStates
{
  public class PlayState : GameState
  {
    private Camera _camera;
    private RenderHandler _renderHandler;
    private List<BaseObject> _components;
    private List<BaseObject> _renderedComponents;
    private Player _player;
    private MenuPanel _characterPanel;
    private UIHandler _uiHandler;

    public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight) : 
      base(graphicsDevice, contentManager, screenWidth, screenHeight) { }

    public override void LoadContent()
    {
      //Load UI
      _characterPanel = new MenuPanel(_contentManager);

      // Create Map Manager and load map0
      MapManager.LoadMaps(_contentManager);

      // Load camera
      _camera = new Camera();
      _renderHandler = new RenderHandler();

      // Loading the player character
      _player = new Player(_graphicsDevice, _contentManager.Load<Texture2D>("Sprites/Characters/CharacterBaseFront"), MapManager.CurrentMap, -64, -64);

      //Load components
      MapManager.LoadMapContent(_player);
      _renderedComponents = new List<BaseObject>();

      _uiHandler = new UIHandler(_screenWidth, _screenHeight, _player, _contentManager);
    }

    public override void Update(GameTime gameTime)
    {
      // Update every component
      _renderedComponents = _renderHandler
        .GetRenderableObjects(MapManager.CurrentMapComponents, _player, new Rectangle(0, 0, _screenWidth, _screenHeight));
      _player.Update(gameTime, _renderedComponents);
      foreach (BaseObject comp in _renderedComponents)
      {
        comp.Update(gameTime, _renderedComponents);
      }
      _camera.Follow(_player);
      _characterPanel.Update(gameTime);
      _uiHandler.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      _graphicsDevice.Clear(Color.Gray);

      // Draw every component
      spriteBatch.Begin(transformMatrix: Camera.Transform);
      foreach (BaseObject comp in _renderedComponents.OrderBy(x => x.ZIndex))
      {
        comp.Draw(_contentManager, gameTime, spriteBatch);
        if (comp.GetType() == typeof(BaseCharacter) || comp == _player)
        {
          (comp as BaseCharacter).DrawUI(gameTime, spriteBatch, _graphicsDevice);
        }
      }
      _player.DrawAbilities(gameTime, spriteBatch, _graphicsDevice);
      spriteBatch.End();

      //Static elements, like UI
      spriteBatch.Begin();
      _uiHandler.Draw(gameTime, spriteBatch, _graphicsDevice);
      _characterPanel.Draw(gameTime, spriteBatch);
      spriteBatch.End();
    }
  }
}
