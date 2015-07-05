using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    private TileManager m_tiles;
    private UnitManager m_units;

    private Transform boardHolder;
    //Board might be ~30x30
    int map_width = 20;
    int map_height = 20;
    GameObject[,] map;
    List<Vector3> mapPositions = new List<Vector3>();
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
        m_tiles = GetComponent<TileManager>();
        m_units = GetComponent<UnitManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
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
                mapPositions.Add(pos);
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

    public void SetUpUnits(Player p)
    {
        UnitManager m_units = GameObject.Find("GameManager").GetComponent<UnitManager>();
        for (int i = 0; i < p.Elements.Length; i++)
        {
            Element e = p.Elements[i];
            GameObject u = Instantiate(m_units.units[e]) as GameObject;
            //u.transform.SetParent(this.gameObject.transform);
            Unit u_s = u.GetComponent<Unit>();
            units.Add(u_s);
            u_s.Team = p.Team;
            p.AddUnit(u_s);
            int rand = Random.Range(0, tiles.Count);
            u_s.CurrentTile = tiles[rand];
            u_s.SnapToCurrent();
            tiles[rand].Occupied = true;
            tiles[rand].OccupiedBy = u_s;
        }
    }

    public void ClearSelectedUnit()
    {
        foreach (Unit u in units)
        {
            u.Selected = false;
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
                u.EndTurn();
            }
        }
    }

    public void ParsePower(Power p, ref List<Tile> affectedTiles, ref List<Unit> affectedUnits)
    {

    }
}
