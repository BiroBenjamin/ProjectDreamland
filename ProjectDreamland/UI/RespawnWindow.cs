using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.UI.Components;

namespace ProjectDreamland.UI
{
  public class RespawnWindow : BaseUI
  {
    public static bool IsShown { get; set; } = false;

    private string _title = "You died!";
    private Vector2 _titlePosition;
    private Button _respawnButton;
    private SpriteFont _font;

    public RespawnWindow(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) : base(graphicsDevice, bounds, color)
    {
      _font = Fonts.ArialBig;
      _titlePosition = new Vector2(
        bounds.X + bounds.Width / 2 - _font.MeasureString(_title).X / 2,
        bounds.Y + 20);
      Rectangle respawnButtonBounds = new Rectangle(
        (int)(bounds.X + bounds.Width / 2 - bounds.Width * .325f),
        (int)(bounds.Y + bounds.Height - bounds.Height * .45f),
        (int)(bounds.Width * .65f),
        (int)(bounds.Height * .4f));
      _respawnButton = new Button(graphicsDevice, respawnButtonBounds, Color.BlanchedAlmond * .8f, Color.Black * .8f, _font, "Respawn");
      _respawnButton.MouseEventHandler.OnLeftClick += () => 
      {
        Player.RespawnPoint.Respawn(Player.Self);
        IsShown = false;
      };
    }

    public override void Update(GameTime gameTime)
    {
      if (!IsShown) return;
      base.Update(gameTime);
      _respawnButton.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content, layerDepth - .02f);
      _respawnButton.Draw(gameTime, spriteBatch, content, layerDepth - .01f);
      spriteBatch.DrawString(_font, _title, _titlePosition, Color.DarkRed, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth - .01f);
    }
  }
}
