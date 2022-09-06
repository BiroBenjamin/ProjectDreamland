using ProjectDreamland.UI.Menu;
using ProjectDreamland.UI.Menu.OptionsMenu;
using System;

namespace ProjectDreamland.Data
{
  public static class Actions
  {
    //Placeholder action, does nothing
    public static readonly Action Placeholder = () =>
    {

    };
    //Toggles the pause menu
    public static readonly Action ToggleMenu = () =>
    {
      MenuPanel.IsShown = !MenuPanel.IsShown;
    };
    //Quits the application
    public static readonly Action ExitApplication = () =>
    {
      //Game1.Self.Exit();
    };
    //Toggles the settings menu
    public static readonly Action ToggleSettings = () =>
    {
      SettingsMenu.IsShown = !SettingsMenu.IsShown;
    };
  }
}
