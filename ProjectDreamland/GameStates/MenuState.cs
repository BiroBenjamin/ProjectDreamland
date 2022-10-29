using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.Constants;
using ProjectDreamland.ExtensionClasses;
using ProjectDreamland.Managers;
using ProjectDreamland.UI.Components;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDreamland.GameStates
{
    public class MenuState : GameState
  {
    private SpriteFont _font;

    private List<string> _title;
    private Vector2 _titlePosition;
    private int _indentation;
    private int _spacing;

    private List<Button> _buttons;
    private Rectangle _buttonBounds;
    private int _buttonCount = 2;

    public MenuState(GraphicsDevice graphicsDevice, ContentManager content) : base(graphicsDevice, content)
    {
      _font = Fonts.ArialBig;
      _indentation = 40;
      _spacing = 30;
      _title = new List<string>() { "Project", "Dreamland" };
    }

    public override void LoadContent()
    {
      _titlePosition = new Vector2(
        _screenWidth / 2 - (_font.MeasureString(_title[0]).X + _indentation) / 2,
        _screenHeight * .15f - (_font.MeasureString(_title[0]).Y + _font.MeasureString(_title[1]).Y + _spacing) / 2);

      _buttonBounds = new Rectangle(
        (int)(_screenWidth / 2 - _screenWidth * .1f),
        (int)(_titlePosition.Y * 3),
        (int)(_screenWidth * .2f),
        (int)(100));
      _buttons = new List<Button>();
      for(int i = 0; i < _buttonCount; i++)
      {
        _buttons.Add(new Button(_graphicsDevice, _buttonBounds, Color.BlanchedAlmond * .8f, Color.BlanchedAlmond.Invert(), _font));
        _buttonBounds = new Rectangle(
          _buttonBounds.X, 
          _buttonBounds.Y + _buttonBounds.Height + 25, 
          _buttonBounds.Width, 
          _buttonBounds.Height);
      }
      _buttons[0].Text = "Play";
      _buttons[0].MouseEventHandler.OnLeftClick += () => { GameStateManager.ChangeGameState(Data.Enums.GameStatesEnum.Play); };
      _buttons[1].Text = "Exit";
      _buttons[1].MouseEventHandler.OnLeftClick += () => { Actions.ExitApplication(); };
    }

    public override void Update(GameTime gameTime)
    {
      foreach(Button button in _buttons)
      {
        button.Update(gameTime);
      }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Begin();

      spriteBatch.DrawString(_font, _title[0], _titlePosition, Color.White);
      spriteBatch.DrawString(
        _font, _title[1],
        new Vector2(_titlePosition.X + _indentation, _titlePosition.Y + _spacing),
        Color.White);
      foreach (Button button in _buttons)
      {
        button.Draw(gameTime, spriteBatch, _contentManager, .5f);
      }

      spriteBatch.End();
    }
  }
}
