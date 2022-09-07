using ProjectDreamland.Data.Attributes;
using System.ComponentModel;

namespace ProjectDreamland.Data.Enums
{
    public enum FileTypesEnum
	{
		[Description("Map")]
		Map,
		[Description("Character"), Displayable]
		Character,
		[Description("World Object"), Displayable]
		WorldObject,
		[Description("Tile"), Displayable]
		Tile,
	}
}
