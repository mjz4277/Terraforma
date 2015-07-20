using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


public class LevelManager : MonoBehaviour {

    private ResourceManager m_resources;
    private TileManager m_tiles;
    private UnitManager m_units;

    private XmlSerializer xml_serializer = new XmlSerializer(typeof(BiomeTable));
    private StreamReader loadStream;
    private BiomeTable biomeTable = new BiomeTable();

    private Transform boardHolder;
    //Board might be ~30x30
    int map_width = 15;
    int map_height = 15;
    int minor_units = 0;
    GameObject[,] map;
    List<Tile> tiles = new List<Tile>();
    List<Unit> units = new List<Unit>();
    public GameObject[] tile_types;
    float tile_size = 1.5f;
    float tile_spacing = 0.05f;
    float tile_height;
    float tile_width;
	// Use this for initialization

    public List<Unit> Units
    {
        get { return units; }
    }
   
	void Start () {
        Debug.Log("Level Manager Started");
        m_resources = GetComponent<ResourceManager>();
        m_tiles = GetComponent<TileManager>();
        m_units = GetComponent<UnitManager>();
        LoadBiomeTable();
        //BuildMap();
        BuildRegion(0);
        SetUpUnits();
        m_tiles.Init();
        m_units.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void LoadBiomeTable()
    {
        try
        {
            loadStream = new StreamReader(@"Assets/XML/biomes.xml");
            biomeTable = (BiomeTable)xml_serializer.Deserialize(loadStream);
            loadStream.Close();
        }
        catch(IOException e)
        {
            Debug.Log("Error: " + e.Message);
        }
    }

    public void BuildMap()
    {
        tile_height = 2.0f;
        tile_width = (Mathf.Sqrt(3) / 2) * tile_height;

        map = new GameObject[map_width, map_height];
        boardHolder = new GameObject("Board").transform;

        float offset = (tile_size / 2) * Mathf.Sqrt(2);
        offset = (tile_width / 2) + (tile_spacing / 2);
        for (int i = 0; i < map_width; i++)
        {
            float nextOff = (i % 2 == 0) ? 0 : offset;

            for (int j = 0; j < map_height; j++)
            {
                Vector3 pos = new Vector3(i * ((tile_height * 0.75f) + tile_spacing), 0, j * (tile_width + tile_spacing) + nextOff);
                GameObject tile = Instantiate(tile_types[0], pos, Quaternion.identity) as GameObject;
                tile.name = "Tile";
                tile.transform.SetParent(boardHolder);
                map[i, j] = tile;
                Tile t = tile.GetComponent<Tile>();
                tiles.Add(t);
            }
        }

        //Give the tiles references to their neighbors
        LinkTiles();
    }

    public void BuildRegion(int regionIndex)
    {
        int regionSize = biomeTable.Biomes.Biomes[regionIndex].Size;
        boardHolder = new GameObject("Board").transform;

        tile_height = 2.0f;
        tile_width = (Mathf.Sqrt(3) / 2) * tile_height;

        float offset = (tile_size / 2) * Mathf.Sqrt(2);
        offset = (tile_width / 2) + (tile_spacing / 2);
        float totalOff = 0.0f;
        int numRows = regionSize * 2 - 1;
        int rowSize = regionSize;
        for(int i = 0; i < numRows; i++)
        {
            for(int j = 0; j < rowSize; j++)
            {
                Vector3 pos = new Vector3(i * ((tile_height * 0.75f) + tile_spacing), 0, j * (tile_width + tile_spacing) + totalOff);
                GameObject tile = Instantiate(tile_types[0], pos, Quaternion.identity) as GameObject;
                tile.name = "Tile";
                tile.transform.SetParent(boardHolder);
                //map[i, j] = tile;
                Tile t = tile.GetComponent<Tile>();
                tiles.Add(t);
            }

            if(i < regionSize - 1)
            {
                totalOff -= offset;
                rowSize++;
            }
            else
            {
                totalOff += offset;
                rowSize--;
            }
        }

        //Give the tiles references to their neighbors
        LinkTiles();
        SetTileData(regionIndex);
    }

    private void LinkTiles()
    {
        float distance = tile_width + tile_spacing + 0.5f;
        List<Tile> adj_tiles;
        foreach(Tile t in tiles)
        {
            //Cannot clear array, because of referencing
            adj_tiles = new List<Tile>();
            foreach(Tile adj in tiles)
            {
                if (t == adj) continue;

                float tile_dist = Vector3.Distance(t.transform.position, adj.transform.position);
                if(tile_dist <= distance)
                {
                    adj_tiles.Add(adj);
                }
            }
            //Save the adjacent tiles in Tile class
            Tile s_tile = t.GetComponent<Tile>();
            s_tile.AdjacentTiles = adj_tiles;
        }
    }

    private void SetTileData(int regionIndex)
    {
        List<Row_XML> rows = biomeTable.Biomes.Biomes[regionIndex].Rows.Row;
        int tileIndex = 0;
        foreach(Row_XML rowxml in rows)
        {
            foreach(Tile_XML txml in rowxml.Tile)
            {
                Tile t = tiles[tileIndex];
                t.ChangeTileBaseData(txml.Base);
                tileIndex++;
                if (tileIndex >= tiles.Count) return;
            }
        }
    }

    public void SetUpUnits()
    {
        m_tiles = GetComponent<TileManager>();
        m_units = GetComponent<UnitManager>();
        GameObject[] playerGO = GameObject.FindGameObjectsWithTag("Player");
        Player[] players = new Player[playerGO.Length];
        for (int i = 0; i < playerGO.Length; i++ )
        {
            players[i] = playerGO[i].GetComponent<Player>();
        }
        foreach(Player p in players)
        {
            for (int i = 0; i < p.Elements.Length; i++)
            {
                Element e = p.Elements[i];
                GameObject u_obj = Instantiate(m_resources.unitGO[e]) as GameObject;
                //u.transform.SetParent(this.gameObject.transform);
                Unit u = u_obj.GetComponent<Unit>();
                units.Add(u);
                u.Team = p.Team;
                p.AddUnit(u);

                int iter = 0;
                while (iter < 100)
                {
                    int rand = Random.Range(0, tiles.Count);
                    if (!tiles[rand].Occupied)
                    {
                        u.CurrentTile = tiles[rand];
                        u.SnapToCurrent();
                        tiles[rand].Occupied = true;
                        tiles[rand].OccupiedBy = u;
                        break;
                    }
                }

                SetUpMinorUnits(p, u, e);
            }
        }
    }

    public void SetUpMinorUnits(Player p, Unit u, Element e)
    {
        for(int i = 0; i < minor_units; i++)
        {

            GameObject uMinor_obj = Instantiate(m_resources.minorUnitGO[e]) as GameObject;
            Unit uMinor = uMinor_obj.GetComponent<Unit>();

            units.Add(uMinor);
            uMinor.Team = p.Team;
            p.AddUnit(uMinor);

            foreach(Tile t in u.CurrentTile.AdjacentTiles)
            {
                if (t.Occupied)
                {
                    continue;
                }
                else
                {
                    uMinor.CurrentTile = t;
                    uMinor.SnapToCurrent();
                    t.Occupied = true;
                    t.OccupiedBy = u;
                    break;
                }
            }
        }
    }

    public void ResetUnits()
    {
        foreach (Unit u in units)
        {
            u.ResetUnit();
        }
    }

    public void ChangeTurn(int nextTurn)
    {
        foreach(Unit u in units)
        {
            if(u.Team == nextTurn)
            {
                u.CalculateNextTurnStats();
            }
            else
            {
                u.ChangeTurn();
            }
        }
    }
}
