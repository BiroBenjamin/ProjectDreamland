using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.GameFiles.Abilities;
using ProjectDreamland.UI.Components;
using System;

namespace ProjectDreamland.UI.ActionBar
{
    public class AbilityHolder : BaseUI
  {
    private Button _abilityButton;
    private BaseAbility _ability;
    private BaseUI _abilityShade;
    private Vector2 _cooldownPosition;
    private int _cooldownRounded;
    private SpriteFont _font;
    private string _keyBind;
    private Vector2 _keyBindPosition;

    public AbilityHolder(GraphicsDevice graphicsDevice, Rectangle bounds, BaseAbility ability) : base(graphicsDevice, bounds, new Color())
    {
      _ability = ability;
      _abilityButton = new Button(graphicsDevice, bounds, Color.White, Color.Gray * .8f, Fonts.ArialSmall, texture: _ability.Icon);
      _abilityShade = new BaseUI(graphicsDevice, bounds, Color.Black * .8f);
      _font = Fonts.ArialBig;
      _abilityButton.Tooltip = new Tooltip(graphicsDevice, new Vector2(400, 250), _ability.ToString());
      _keyBind = _ability.KeyBind == null ? "LMB" : _ability.KeyBind.Value.ToString();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      _abilityButton.Update(gameTime);
      _keyBind = _ability.KeyBind == null ? "LMB" : _ability.KeyBind.Value.ToString();
      _keyBindPosition = new Vector2(
        Bounds.X + Bounds.Width - Fonts.ArialSmall.MeasureString(_keyBind).X, 
        Bounds.Y + Bounds.Height - Fonts.ArialSmall.MeasureString(_keyBind).Y);
      if (!_ability.CanCast)
      {
        _abilityShade.Update(gameTime);
        _cooldownRounded = (int)_ability.RemainingCooldown;
        _cooldownPosition = new Vector2(
          Bounds.X + Bounds.Width / 2 - _font.MeasureString(_cooldownRounded.ToString()).X / 2,
          Bounds.Y + Bounds.Height / 2 - _font.MeasureString(_cooldownRounded.ToString()).Y / 2);
      }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDetph)
    {
      base.Draw(gameTime, spriteBatch, content, layerDetph);
      _abilityButton.Draw(gameTime, spriteBatch, content, layerDetph + .01f);
      spriteBatch.DrawString(Fonts.ArialSmall, _keyBind, _keyBindPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None,
        layerDetph + .02f);
      if (!_ability.CanCast)
      {
        _abilityShade.Draw(gameTime, spriteBatch, content, layerDetph + .03f);
        spriteBatch.DrawString(Fonts.ArialBig, _cooldownRounded.ToString(), _cooldownPosition, Color.White, 0f, Vector2.Zero, 1f, 
          SpriteEffects.None, layerDetph + .04f);
      }
    }
  }
}
