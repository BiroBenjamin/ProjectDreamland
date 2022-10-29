using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Handlers;

namespace ProjectDreamland.UI.Components
{
  public class Button : BaseUI
  {
    public Tooltip Tooltip { get; set; }

    private MouseState _currentMouseState;
    private MouseState _previousMouseState;
    public string Text { get; set; }
    private SpriteFont _font;
    private Vector2 _textPosition;

    private Color _hoveredColor;

    public Button(GraphicsDevice graphicsDevice, Rectangle bounds, Color color, Color hoveredColor, SpriteFont font, string text = "", Texture2D texture = null) :
      base(graphicsDevice, bounds, color, texture)
    {
      MouseEventHandler.OnMouseEnter += () =>
      {
        if (Tooltip != null) Tooltip.IsShown = true;
      };
      MouseEventHandler.OnMouseLeave += () =>
      {
        if (Tooltip != null) Tooltip.IsShown = false;
      };
      _hoveredColor = hoveredColor;
      Text = text;
      _font = font;
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      _currentMouseState = Mouse.GetState();

      if (_font != null)
      {
        _textPosition = new Vector2(
          (Bounds.X + Bounds.Width / 2) - _font.MeasureString(Text).X / 2,
          (Bounds.Y + Bounds.Height / 2) - _font.MeasureString(Text).Y / 2);
      }
      if (Tooltip != null) Tooltip.Update(gameTime);

      _previousMouseState = _currentMouseState;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDetph)
    {
      if (Tooltip != null) Tooltip.Draw(gameTime, spriteBatch, content);
      if (IsHovered)
      {
        spriteBatch.Draw(Texture, Bounds, null, _hoveredColor, 0f, Vector2.Zero, SpriteEffects.None, layerDetph);
        spriteBatch.DrawString(_font, Text, _textPosition, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDetph + .001f);
        return;
      }
      base.Draw(gameTime, spriteBatch, content, layerDetph);
      spriteBatch.DrawString(_font, Text, _textPosition, _hoveredColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDetph + .001f);
    }

  }
}
