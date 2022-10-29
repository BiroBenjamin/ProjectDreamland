using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.UI;
using ProjectDreamland.UI.ActionBar;
using ProjectDreamland.UI.Character;
using ProjectDreamland.UI.Components;
using ProjectDreamland.UI.Inventory;
using ProjectDreamland.UI.Menu;
using ProjectDreamland.UI.MenuBar;
using ProjectDreamland.UI.QuestPanel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;

namespace ProjectDreamland.Handlers
{
  public class UIHandler
  {
    public static bool IsHoveredOver { get; set; }

    private GraphicsDevice _graphicsDevice;

    private Player _player;
    private List<BaseUI> _components;
    private ExperienceBar _experienceBar;

    public UIHandler(GraphicsDevice graphicsDevice, ContentManager content, Player player)
    {
      _graphicsDevice = graphicsDevice;
      _player = player;

      int screenWidth = Game1.ScreenWidth;
      int screenHeight = Game1.ScreenHeight;
      Rectangle expBarBounds = new Rectangle(0, screenHeight - 50, screenWidth, 50);
      _experienceBar = new ExperienceBar(expBarBounds);
      Rectangle gameMenuBounds = new Rectangle(
        (int)(screenWidth / 2 - screenWidth * .075f),
        (int)(screenHeight / 2 - screenHeight * .25f), 
        (int)(screenWidth * .15f), 
        (int)(screenHeight * .4f));
      Rectangle characterWindowBounds = new Rectangle(
        (int)(25),
        (int)(screenHeight / 2 - screenHeight * .35f),
        (int)(screenWidth * .3f),
        (int)(screenHeight * .7f));
      Rectangle questWindowBounds = new Rectangle(
        (int)(25),
        (int)(screenHeight / 2 - screenHeight * .35f),
        (int)(screenWidth * .3f),
        (int)(screenHeight * .7f));
      Rectangle questPanelBounds = new Rectangle(
        (int)(screenWidth - (screenWidth * .15f + 10)),
        (int)(screenHeight - screenHeight * .4f - ((screenHeight - screenHeight * .4f) / 2)),
        (int)(screenWidth * .15f),
        (int)(screenHeight * .4f));
      Rectangle actionbarBounds = new Rectangle(
        (int)(screenWidth / 2 - screenWidth * .075f),
        (int)(screenHeight - screenHeight * .1f),
        (int)(screenHeight * .075f * 3),
        (int)(screenHeight * .075f));
      Rectangle menuBarBounds = new Rectangle(
        (int)(screenWidth),
        (int)(screenHeight - screenHeight * .05f - _experienceBar.BaseBounds.Height),
        (int)(0),
        (int)(screenHeight * .05f));
      Rectangle inventoryBounds = new Rectangle(
        (int)(100),
        (int)(100),
        (int)(0),
        (int)(0));

      _components = new List<BaseUI>()
      {
        new GameMenuWindow(graphicsDevice, gameMenuBounds, Color.Black * .9f),
        new CharacterWindow(content, graphicsDevice, characterWindowBounds, Color.Black * .9f, _player),
        new QuestWindow(graphicsDevice, questWindowBounds, Color.Black * .9f),
        new QuestPanel(graphicsDevice, questPanelBounds, Color.Black * .8f),
        new ActionBar(graphicsDevice, actionbarBounds, Color.Black * .9f),
        new MenuBar(graphicsDevice, menuBarBounds, Color.Black * .9f),
        new InventoryWindow(graphicsDevice, inventoryBounds, Color.Black * .8f, 32),
      };
    }

    public void Update(GameTime gameTime)
    {
      _experienceBar.Update(gameTime, _player.ExperienceNeeded, Player.CurrentExperience, _player.Level);
      foreach(BaseUI ui in _components)
      {
        ui.Update(gameTime);
      }
      IsHoveredOver = IsHovered();
    }
    private bool IsHovered()
    {
      bool result = false;
      foreach (BaseUI comp in _components)
      {
        if (comp.IsHovered &&
          comp.GetType().GetProperties().SingleOrDefault(prop => prop.Name == "IsShown")?.GetValue(null).ToString() == true.ToString())
        {
          result = true;
        }
      }
      return result;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content)
    {
      _experienceBar.Draw(gameTime, spriteBatch, _graphicsDevice, .1f);
      foreach (BaseUI ui in _components)
      {
        ui.Draw(gameTime, spriteBatch, content, .1f);
      }
    }
  }
}
