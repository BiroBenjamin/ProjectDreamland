using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Core;
using System;

namespace ProjectDreamland.Handlers
{
  public class ShapesHandler
  {
    private GraphicsDevice _graphicsDevice;
    private BasicEffect _effect;

    private VertexPositionColor[] _vertices;

    public ShapesHandler(GraphicsDevice graphicsDevice, Camera camera)
    {
      _graphicsDevice = graphicsDevice;

      _effect = new BasicEffect(graphicsDevice);
      _effect.TextureEnabled = false;
      _effect.FogEnabled = false;
      _effect.LightingEnabled = false;
      _effect.VertexColorEnabled = true;
      _effect.World = Matrix.Identity;
      _effect.View = Matrix.Identity;
      _effect.Projection = Matrix.CreateOrthographicOffCenter(
        0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1);
    }

    public void Update(GameTime gameTime, Vector2 a, Vector2 b, Vector2 c, Color color)
    {
      _vertices = new VertexPositionColor[]
      {
        new VertexPositionColor(new Vector3(a, 0f), color),
        new VertexPositionColor(new Vector3(b, 0f), color),
        new VertexPositionColor(new Vector3(c, 0f), color),
      };      
    }
    public void Draw(GameTime gameTime)
    {
      foreach(EffectPass pass in _effect.CurrentTechnique.Passes)
      {
        pass.Apply();
        _graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertices, 0, 1);
      }
    }
  }
}
