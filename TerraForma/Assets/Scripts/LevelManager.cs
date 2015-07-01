using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

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

    public void ClearSelectedTiles()
    {
        foreach(Tile t in tiles)
        {
            t.ClearSelection();
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

    public void SelectTile(GameObject obj)
    {
        Tile s_tile = obj.GetComponent<Tile>();
        if(s_tile)
            s_tile.Selected = true;
    }

    public void MoveUnitToTile(Unit u, Tile t)
    {
        u.CurrentTile.Occupied = false;
        u.CurrentTile = t;
        u.CurrentTile.Occupied = true;
        u.CurrentTile.OccupiedBy = u;
        u.SnapToCurrent();
    }

    public void ParsePower(Power p, ref List<Tile> affectedTiles, ref List<Unit> affectedUnits)
    {

    }

    public List<Tile> GetPossibleTiles(Tile startTile, int distance, bool ignoreUnits)
    {
        List<Tile> unvisitedList = new List<Tile>();
        List<Tile> visitedList = new List<Tile>();

        Tile current = startTile;

        foreach(Tile t in tiles)
        {
            if (t == startTile)
                t.Distance = 0.0f;
            else
                t.Distance = Mathf.Infinity;

            unvisitedList.Add(t);
        }

        DijkstraStep(current, ref unvisitedList, ref visitedList, distance, ignoreUnits);

        //Now that all the tiles have values, remove all of the ones above the movement amount
        List<Tile> removalList = new List<Tile>();
        foreach(Tile t in visitedList)
        {
            if (t.Distance > distance)
                removalList.Add(t);
        }

        foreach(Tile t in removalList)
        {
            visitedList.Remove(t);
        }

        return visitedList;
    }

    public Tile GetNearestPossibleTile(Tile hovered, List<Tile> possible)
    {
        Tile closest = null;
        float nearDist = Mathf.Infinity;
        foreach (Tile t in possible)
        {
            float dist = Vector3.Distance(t.transform.position, hovered.transform.position);
            if (dist < nearDist)
            {
                nearDist = dist;
                closest = t;
            }
        }
        return closest;
    }

    void DijkstraStep(Tile current, ref List<Tile>unvisited, ref List<Tile> visited, int distance, bool ignoreUnits)
    {
        List<Tile> adjacent = current.AdjacentTiles;
        List<Tile> removal = new List<Tile>();
        foreach (Tile t in adjacent)
        {

            //Cannot move into occupied tiles
            if(t.Occupied  && !ignoreUnits)
            {
                unvisited.Remove(t);
                continue;
            }

            //Don't overlap
            if (visited.Contains(t) || !unvisited.Contains(t)) continue;

            //Get the possible distance to the tile
            float cost = current.Distance;

            if (ignoreUnits) cost += 1;
            else cost += t.MoveCost;

            //There should be no possible way to get there
            if(cost > distance * 2)
            {
                removal.Add(t);
                continue;
            }

            //If it is not possible to reach, continue
            if (cost > distance)
            {
                continue;
            }

            //If its closer than before, change the distance
            if (cost < t.Distance)
            {
                t.Distance = cost;
            }
        }

        //Add the current node to the visited nodes
        visited.Add(current);

        //Remove it from the unvisited nodes
        unvisited.Remove(current);

        //Stop checking impossible tiles
        foreach (Tile t in removal)
        {
            unvisited.Remove(t);
        }

        //If there are no more unvisited nodes, return
        if(unvisited.Count == 0)
            return;

        Tile next = unvisited[0];

        //Get the next closest tile as the new current
        foreach(Tile t in unvisited)
        {
            if(t.Distance < next.Distance)
                next = t;
        }

        //Keep doing it
        DijkstraStep(next, ref unvisited, ref visited, distance, ignoreUnits);
    }
}
