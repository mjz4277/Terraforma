using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Biome {
    private List<Tile> tiles = new List<Tile>();
    private string b_type;
    private Transform center;
    private Transform t_biome;
    private int size;

    public string BiomeType
    {
        get { return b_type; }
        set { b_type = value; }
    }

    public void AddTile(Tile t)
    {
        tiles.Add(t);
    }

    public Biome()
    {
        b_type = "Default";
    }

    public Biome(string type)
    {
        b_type = type;
    }

    public void ColorTiles()
    {
        foreach(Tile t in tiles)
        {
            t.ChangeTileBaseData(b_type);
        }
    }

    public void GenerateBiome()
    {
        float seedX = Random.Range(0, 1000f);
        float seedY = Random.Range(0, 1000f);
        float x = seedX;
        float y = seedY;

        Transform startLoc = tiles[0].gameObject.transform;

        foreach(Tile t in tiles)
        {
            Transform tileLoc = t.gameObject.transform;

            float diffX = tileLoc.position.x - startLoc.position.x;
            float diffZ = tileLoc.position.z - startLoc.position.z;

            x += diffX;
            y += diffZ;

            BiomeLookupTable b_table = GameObject.FindGameObjectWithTag("BiomeTable").GetComponent<BiomeLookupTable>();

            if(!b_table.biomeLookupDict.ContainsKey(b_type))
            {
                Debug.Log("Biome [" + b_type + "] does not exist");
                return;
            }
            BiomeLookupEntry biomeEntry = b_table.biomeLookupDict[b_type];

            t.ChangeTileBaseData(biomeEntry.GetTileType(Mathf.PerlinNoise(x, y)));

        }
    }
}
