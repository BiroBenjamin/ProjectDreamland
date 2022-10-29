using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDreamland.UI.ActionBar
{
  public class ActionBar : BaseUI
  {
    private AbilityHolder _ability1;
    private AbilityHolder _ability2;
    private AbilityHolder _ability3;

    public ActionBar(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) : base(graphicsDevice, bounds, color)
    {
      Rectangle ability1Bounds = new Rectangle(
        bounds.X + 5,
        bounds.Y + 5,
        (bounds.Width - 20) / 3,
        bounds.Height - 10);
      _ability1 = new AbilityHolder(graphicsDevice, ability1Bounds, Player.MeleeAttack);
      Rectangle ability2Bounds = new Rectangle(
        ability1Bounds.X + ability1Bounds.Width + 5,
        bounds.Y + 5,
        (bounds.Width - 20) / 3,
        bounds.Height - 10);
      _ability2 = new AbilityHolder(graphicsDevice, ability2Bounds, Player.NurturingWinds);
      Rectangle ability3Bounds = new Rectangle(
        ability2Bounds.X + ability2Bounds.Width + 5,
        bounds.Y + 5,
        (bounds.Width - 20) / 3,
        bounds.Height - 10);
      _ability3 = new AbilityHolder(graphicsDevice, ability3Bounds, Player.Fireball);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      _ability3.Update(gameTime);
      _ability2.Update(gameTime);
      _ability1.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDetph)
    {
      base.Draw(gameTime, spriteBatch, content, layerDetph);
      _ability3.Draw(gameTime, spriteBatch, content, layerDetph + .01f);
      _ability2.Draw(gameTime, spriteBatch, content, layerDetph + .01f);
      _ability1.Draw(gameTime, spriteBatch, content, layerDetph + .01f);
    }
  }
}
