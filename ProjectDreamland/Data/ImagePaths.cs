using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDreamland.Data
{
  public static class ImagePaths
  {
    private static readonly string ContentFolder = @"../../Content";
    private static readonly string UIFodler = Path.Combine(ContentFolder, "UI");
    private static readonly string MenuFolder = Path.Combine(UIFodler, "Menu");

    public static readonly string MenuPanelBackground = Path.Combine(MenuFolder, "MenuPanelBackground");
    public static readonly string HeaderSmall = Path.Combine(MenuFolder, "HeaderSmall");
    public static readonly string ButtonSmall = "UI/Menu/ButtonSmall";
  }
}
