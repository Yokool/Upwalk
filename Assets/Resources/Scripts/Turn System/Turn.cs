using System;
using System.Xml.Serialization;

[Serializable]
public enum Turn
{
    [XmlEnum(Name = "EASY")]
    EASY = 0,
    [XmlEnum(Name = "MEDIUM")]
    MEDIUM = 1,
    [XmlEnum(Name = "HARD")]
    HARD = 2,
    [XmlEnum(Name = "FULL")]
    FULL = 3
}
