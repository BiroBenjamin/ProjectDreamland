using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Components;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Objects;
using ProjectDreamland.GameStates;
using ProjectDreamland.Handlers;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Menu;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectDreamland.GameStates
{
  public class PlayState : GameState
  {
    private Camera _camera;
    private RenderHandler _renderHandler;
    private List<BaseObject> _renderedComponents;
    private Player _player;
    private UIHandler _uiHandler;
    private InputHandler _inputHandler;

    public PlayState(GraphicsDevice graphicsDevice, ContentManager contentManager) : 
      base(graphicsDevice, contentManager) { }

    public override void LoadContent()
    {
      MapManager.LoadMaps(_contentManager);
      QuestManager.Initialize();

      _camera = new Camera();
      _renderHandler = new RenderHandler();
      _player = new Player(_graphicsDevice, 
        _contentManager.Load<Texture2D>(Path.Combine(SystemPrefsManager.SystemPrefs.RootPath, "Sprites/Characters/CharacterBaseFront")), 
        MapManager.CurrentMap, -64, -64);

      MapManager.LoadMapContent(_player);
      _renderedComponents = new List<BaseObject>();

      _uiHandler = new UIHandler(_graphicsDevice, _contentManager, _player);
      _inputHandler = new InputHandler();
    }

    public override void Update(GameTime gameTime)
    {
      _inputHandler.Update(gameTime);
      _uiHandler.Update(gameTime);

      if (!GameMenuWindow.IsShown)
      {
        _renderedComponents = _renderHandler
          .GetRenderableObjects(MapManager.CurrentMapComponents, _player, new Rectangle(0, 0, _screenWidth, _screenHeight));
        _player.Update(gameTime, _renderedComponents);
        foreach (BaseObject comp in _renderedComponents)
        {
          comp.Update(gameTime, _renderedComponents);
        }
        _camera.Follow(_player);
      }

      CursorManager.Update(gameTime, _renderedComponents);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      _graphicsDevice.Clear(Color.Gray);

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

      spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
      _uiHandler.Draw(gameTime, spriteBatch, _contentManager);
      spriteBatch.End();
    }
  }
}
