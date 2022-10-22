using System.Xml.Serialization;

namespace ProjectDreamland.Data.Enums
{
  public enum CharacterAffiliationsEnum
  {
    [XmlEnum(Name = "Friendly")]
    Friendly,
    [XmlEnum(Name = "Neutral")]
    Neutral,
    [XmlEnum(Name = "Hostile")]
    Hostile
  }
}
