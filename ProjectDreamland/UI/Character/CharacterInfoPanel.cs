using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.UI.Components;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.UI.Character
{
    public class CharacterInfoPanel : BaseUI
  {
    private Player _player;
    private List<string> _splitText;
    private Vector2 _newLinePosition;

    public CharacterInfoPanel(GraphicsDevice graphicsDevice, Rectangle bounds, Color color, Player player) :
      base(graphicsDevice, bounds, color)
    {
      _player = player;
      _splitText = _player.ToString().Split("\\n").ToList();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      _splitText = _player.ToString().Split("\\n").ToList();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      base.Draw(gameTime, spriteBatch, content, layerDepth);
      _newLinePosition = new Vector2(Bounds.X + 10, Bounds.Y + 10);
      foreach (string text in _splitText)
      {
        spriteBatch.DrawString(Fonts.ArialSmall, text, _newLinePosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 
          layerDepth + .01f);
        _newLinePosition.Y += Fonts.ArialSmall.MeasureString(text).Y + 5;
      }
    }
  }
}
