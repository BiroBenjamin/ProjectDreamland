using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ProjectDreamland.ExtensionClasses
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum enumValue)
		{
			var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
			var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
		}

		public static IEnumerable<string> GetDescriptionOfAll(this Type enumType, Type attributeType = null)
		{
			if (attributeType != null)
			{
				return enumType.GetFields()
					.Where(x => x.IsDefined(attributeType))
					.SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true)
						.Cast<DescriptionAttribute>())
					.Select(x => x.Description);
			}
			return enumType.GetFields()
				.SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true)
					.Cast<DescriptionAttribute>())
				.Select(x => x.Description);
        }
	}
}
