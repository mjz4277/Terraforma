using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class BiomeMap {

    [XmlElement("Map")]
    public MapXML Map { get; set; }
}

public class MapXML
{
    [XmlAttribute("Width")]
    public int Width { get; set; }

    [XmlAttribute("Height")]
    public int Height { get; set; }

    [XmlAttribute("BiomeCount")]
    public int BiomeCount { get; set; }

    [XmlElement("Rows")]
    public Rows_Map_XML Rows { get; set; }
}

public class Rows_Map_XML
{
    [XmlElement("Row")]
    public List<Row_Map_XML> Row { get; set; }
}

public class Row_Map_XML
{
    [XmlAttribute("Num")]
    public int Num { get; set; }

    [XmlElement("Tile")]
    public List<Tile_Map_XML> Tile { get; set; }
}

public class Tile_Map_XML
{
    [XmlAttribute("Biome")]
    public int Biome { get; set; }
}
