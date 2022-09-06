using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectDreamland.UI.Menu.Components
{
  public class TitleHeader : BaseUI
  {
    public TitleHeader(string titleText, Texture2D titleImage, Rectangle backgroundBounds, bool isOnLeftSide) : 
      base(titleText, titleImage, backgroundBounds, isOnLeftSide)
    {
       
    }

    public override void Update(GameTime gameTime)
    {

    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      base.Draw(gameTime, spriteBatch, content);
    }

  }
}
