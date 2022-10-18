using DreamlandLibrary.Data.Attributes;
using System.ComponentModel;

namespace DreamlandLibrary.Data.Enums
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
