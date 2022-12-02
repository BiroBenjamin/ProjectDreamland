using ProjectDreamland.Data.GameFiles.Items;
using ProjectDreamland.Data.GameFiles.Quests;
using ProjectDreamland.UI.Character;
using ProjectDreamland.UI.Inventory;
using ProjectDreamland.UI.Menu;
using ProjectDreamland.UI.QuestPanel;
using System;

namespace ProjectDreamland.Data.Constants
{
  public static class Actions
  {
    public static readonly Action ExitApplication = () =>
    {
      Game1.Self.Exit();
    };
    public static readonly Action EscPressed = () =>
    {
      if (QuestWindow.IsShown || InventoryWindow.IsShown || CharacterWindow.IsShown)
      {
        QuestWindow.IsShown = false;
        InventoryWindow.IsShown = false;
        CharacterWindow.IsShown = false;
      }
      else
      {
        GameMenuWindow.IsShown = !GameMenuWindow.IsShown;
      }
    };
  }
}
