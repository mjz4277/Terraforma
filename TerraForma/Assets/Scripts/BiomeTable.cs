using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class BiomeTable {

    [XmlElement("Biomes")]
    public Biomes_XML Biomes { get; set; }
}

public class Biomes_XML
{
    [XmlAttribute("Count")]
    public int Count { get; set; }

    [XmlElement("Biome")]
    public List<Biome_XML> Biomes { get; set; }
}

public class Biome_XML
{
    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlAttribute("Size")]
    public int Size { get; set; }

    [XmlElement("Rows")]
    public Rows_XML Rows { get; set; }
}

public class Rows_XML
{
    [XmlElement("Row")]
    public List<Row_XML> Row { get; set; }
}

public class Row_XML
{
    [XmlAttribute("Num")]
    public string RowNum { get; set; }

    [XmlAttribute("Count")]
    public int Count { get; set; }

    [XmlElement("Tile")]
    public List<Tile_XML> Tile { get; set; }
}

public class Tile_XML
{
    [XmlAttribute("Base")]
    public string Base { get; set; }

    [XmlAttribute("Effect")]
    public string Effect { get; set; }
}
