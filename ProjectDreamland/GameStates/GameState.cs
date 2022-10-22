using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDreamland.GameStates
{
  public abstract class GameState : IGameState
  {
    protected GraphicsDevice _graphicsDevice;
    protected ContentManager _contentManager;
    protected readonly int _screenWidth, _screenHeight;

    public GameState(GraphicsDevice graphicsDevice, ContentManager contentManager, int screendWidth, int screenHeight)
    {
      _graphicsDevice = graphicsDevice;
      _contentManager = contentManager;
      _screenWidth = screendWidth;
      _screenHeight = screenHeight;
    }

    public abstract void LoadContent();

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
  }
}
