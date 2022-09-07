using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data;

namespace ProjectDreamland.UI.Menu
{
  public class MenuPanelBase
  {
    protected ContentManager _content;

    protected Texture2D _titleIcon;
    protected Texture2D _buttonIcon;

    public MenuPanelBase(ContentManager content)
    {
      _content = content;
      LoadContent();
    }

    private void LoadContent()
    {
      _titleIcon = _content.Load<Texture2D>(ImagePaths.HeaderSmall);
      _buttonIcon = _content.Load<Texture2D>(ImagePaths.ButtonSmall);
    }

    public virtual void Update(GameTime gameTime)
    {
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
  }
}
