using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.Data.GameFiles.Quests;
using ProjectDreamland.ExtensionClasses;
using ProjectDreamland.UI.Components;
using System;
using System.Linq;

namespace ProjectDreamland.UI.QuestPanel
{
    public class QuestWindow : BaseUI
  {
    public static bool IsShown { get; set; } = false;
    public static Quest Quest { get; set; }

    private Rectangle _titleBounds;
    private Rectangle _descriptionBounds;
    private Rectangle _summaryBounds;
    private Rectangle _rewardBounds;
    private Button _acceptButton;
    private Button _completeButton;
    private Button _closeButton;
    private int _buttonWidth;
    private SpriteFont _fontBig;
    private SpriteFont _fontSmall;
    private Button _rewardItem;

    public QuestWindow(GraphicsDevice graphicsDevice, Rectangle bounds, Color color) : 
      base(graphicsDevice, bounds, color)
    {
      _fontBig = Fonts.ArialBig;
      _fontSmall = Fonts.ArialSmall;
      _buttonWidth = Math.Max(Math.Max((byte)_fontBig.MeasureString("Accept").X, (byte)_fontBig.MeasureString("Cancel").X), 
        (byte)_fontBig.MeasureString("Complete").X) + 10;
      Rectangle acceptButtonBounds = new Rectangle(
        (int)(Bounds.X + Bounds.Width * .3f - _buttonWidth / 2),
        Bounds.Y + Bounds.Height - 60,
        _buttonWidth,
        50);
      _acceptButton = new Button(graphicsDevice, acceptButtonBounds, ColorCodes.OK, Color.Black, _fontBig, "Accept");
      _acceptButton.MouseEventHandler.OnLeftClick += () =>
      {
        Player.Quests.Add(Quest);
        Quest.IsAccepted = true;
      };
      _completeButton = new Button(graphicsDevice, acceptButtonBounds, ColorCodes.OK, Color.Black, _fontBig, "Complete");
      _completeButton.MouseEventHandler.OnLeftClick += () => 
      {
        Quest playersQuest = Player.Quests.Where(x => x.ID == Quest.ID).FirstOrDefault();
        if (playersQuest != null && playersQuest.Objective.IsDone)
        {
          Player.Quests.Where(x => x.ID == Quest.ID).FirstOrDefault().IsDone = true;
          Quest = null;
          IsShown = false;
        }
      };
      Rectangle closeButtonBounds = new Rectangle(
        (int)(Bounds.X + Bounds.Width * .7f - _buttonWidth / 2),
        Bounds.Y + Bounds.Height - 60,
        _buttonWidth,
        50);
      _closeButton = new Button(graphicsDevice, closeButtonBounds, ColorCodes.Cancel, Color.Black, _fontBig, "Cancel");
      _closeButton.MouseEventHandler.OnLeftClick += Actions.EscPressed;
    }

    public override void Update(GameTime gameTime)
    {
      if (Quest == null) return;
      base.Update(gameTime);
      if(Quest.RewardItem != null)
      {
        Rectangle rewardItemBounds = new Rectangle((int)(_rewardBounds.X + _rewardBounds.Width * .6f),
          (int)(_rewardBounds.Y + _fontBig.MeasureString("Reward:").Y + 10), 64, 64);
        _rewardItem = new Button(_graphicsDevice, rewardItemBounds, Color.White, Color.White, Fonts.ArialBig, 
          texture: Quest.RewardItem.Texture);
        _rewardItem.Tooltip = new Tooltip(_graphicsDevice, Vector2.Zero, Quest.RewardItem.ToString());
        _rewardItem.Update(gameTime);
      }

      _titleBounds = new Rectangle(
        Bounds.X + 30,
        Bounds.Y + 10,
        (int)_fontBig.MeasureString(Quest.Title).X,
        (int)_fontBig.MeasureString(Quest.Title).Y);
      _descriptionBounds = new Rectangle(
        Bounds.X + 20,
        Bounds.Y + _titleBounds.Height + 20,
        Bounds.Width - 20,
        (int)(Bounds.Height * .35f));
      _summaryBounds = new Rectangle(
        Bounds.X + 20,
        Bounds.Y + _titleBounds.Height + _descriptionBounds.Height + 10,
        Bounds.Width  - 20,
        (int)(Bounds.Height * .2f));
      _rewardBounds = new Rectangle(
        Bounds.X + 20,
        Bounds.Y + _titleBounds.Height + _descriptionBounds.Height + _summaryBounds.Height + 10,
        Bounds.Width - 20,
        (int)(Bounds.Height + .2f));

      if (!Quest.IsAccepted)
      {
        _acceptButton.Update(gameTime);
      }
      else
      {
        _completeButton.Update(gameTime);
      }
      _closeButton.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, float layerDepth)
    {
      if (!IsShown) return;
      base.Draw(gameTime, spriteBatch, content, layerDepth);

      if (Quest == null) return;
      spriteBatch.DrawString(_fontBig, Quest.Title, new Vector2(_titleBounds.X, _titleBounds.Y), ColorCodes.TitleColor, 0f, Vector2.Zero,
        1f, SpriteEffects.None, layerDepth + .01f);
      spriteBatch.DrawString(_fontSmall, Quest.Description, new Vector2(_descriptionBounds.X, _descriptionBounds.Y), _color.Invert(), 
        0f, Vector2.Zero, 1f, SpriteEffects.None, layerDepth + .01f);
      spriteBatch.DrawString(_fontBig, "Summary:", new Vector2(_summaryBounds.X, _summaryBounds.Y), _color.Invert(), 0f, Vector2.Zero,
        1f, SpriteEffects.None, layerDepth + .01f);
      spriteBatch.DrawString(_fontSmall, Quest.Objective.Description, 
        new Vector2(_summaryBounds.X + 10, _summaryBounds.Y + _fontBig.MeasureString("Summary:").Y + 10), _color.Invert(), 0f, Vector2.Zero,
        1f, SpriteEffects.None, layerDepth + .01f);
      spriteBatch.DrawString(_fontBig, "Reward:", new Vector2(_rewardBounds.X, _rewardBounds.Y), _color.Invert(), 0f, Vector2.Zero, 1f,
        SpriteEffects.None, layerDepth + .01f);
      spriteBatch.DrawString(_fontSmall, $"{Quest.RewardExp} EXP", 
        new Vector2(_rewardBounds.X + 10, _rewardBounds.Y + _fontBig.MeasureString("Reward:").Y + 10), _color.Invert(), 0f, Vector2.Zero,
        1f, SpriteEffects.None, layerDepth + .01f);
      if (_rewardItem != null)
        _rewardItem.Draw(gameTime, spriteBatch, content, layerDepth + .01f);

      if (!Quest.IsAccepted)
      {
        _acceptButton.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
      }
      else
      {
        _completeButton.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
      }
      _closeButton.Draw(gameTime, spriteBatch, content, layerDepth + .01f);
    }
  }
}
