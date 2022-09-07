using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.UI.Menu.Components
{
  public class BaseUI
  {
    protected Rectangle _imageBounds;
    protected Texture2D _image;
    protected string _text;
    protected Rectangle _textBounds;
    protected bool _isOnLeftSide;


    public BaseUI(string titleText, Texture2D titleImage, Rectangle backgroundBounds, bool isOnLeftSide)
    {
      _text = titleText;
      _isOnLeftSide = isOnLeftSide;

      _image = titleImage;
      SetBounds(backgroundBounds);
    }
    private void SetBounds(Rectangle backgroundBounds)
    {
      int x, y;
      int width = _image.Width * 2;
      int height = _image.Height * 2;
      if (_isOnLeftSide)
      {
        x = (backgroundBounds.X + backgroundBounds.Width / 4) + _image.Width / 8;
        y = (backgroundBounds.Y) + _image.Height * 3;
        _imageBounds = new Rectangle(x, y, width, height);
        _textBounds = new Rectangle(x, y, width, height);
        return;
      }
      x = (int)((backgroundBounds.X + backgroundBounds.Width / 2) + _image.Width * 1.75);
      y = (backgroundBounds.Y) + _image.Height * 3;
      _imageBounds = new Rectangle(x, y, width, height);
      _textBounds = new Rectangle(x, y, width, height);
    }

    public virtual void Update(GameTime gameTime)
    {

    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      spriteBatch.Draw(_image, _imageBounds, Color.White);
      spriteBatch.DrawString(content.Load<SpriteFont>("Fonts/ArialBig"), _text,
        new Vector2(_imageBounds.X + _imageBounds.Width / 4, _imageBounds.Y + _imageBounds.Height / 4), Color.Black);
    }
  }
}
