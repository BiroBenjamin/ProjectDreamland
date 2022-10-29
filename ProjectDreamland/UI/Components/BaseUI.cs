using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Handlers;

namespace ProjectDreamland.UI.Components
{
  public class BaseUI
  {
    public Rectangle Bounds { get; set; }
    public Vector2 Origin { get; set; }
    public Texture2D Texture { get; set; }
    public bool IsHovered { get; set; }
    public MouseEventHandler MouseEventHandler { get; set; }

    protected GraphicsDevice _graphicsDevice;
    protected Color _color;

    public BaseUI(GraphicsDevice graphicsDevice, Rectangle bounds, Color color, Texture2D texture = null)
    {
      if (texture != null) Texture = texture;
      else
      {
        Texture = new Texture2D(graphicsDevice, 1, 1);
        Texture.SetData(new Color[] { color });
      }
      _graphicsDevice = graphicsDevice;
      Bounds = bounds;
      Origin = new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
      _color = color;
      MouseEventHandler = new MouseEventHandler(bounds);
      MouseEventHandler.OnMouseEnter += () =>
      {
        IsHovered = true;
      };
      MouseEventHandler.OnMouseLeave += () => 
      {
        IsHovered = false;
      };
    }

    public void ResetTexture()
    {
      Texture = new Texture2D(_graphicsDevice, 1, 1);
      Texture.SetData(new Color[] { _color });
    }

    public virtual void Update(GameTime gameTime)
    {
      if(Texture == null)
      {
        Texture = new Texture2D(_graphicsDevice, 1, 1);
        Texture.SetData(new Color[] { _color });
      }
      Origin = new Vector2(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2);
      MouseEventHandler.Update(gameTime, Bounds);
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      spriteBatch.Draw(Texture, Bounds, null, _color, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
    }
  }
}
