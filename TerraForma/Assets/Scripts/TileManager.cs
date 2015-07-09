using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

    private List<Tile> tiles = new List<Tile>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init()
    {
        GameObject[] obj_tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < obj_tiles.Length; i++)
        {
            Tile t = obj_tiles[i].GetComponent<Tile>();
            tiles.Add(t);
        }
    }

    public void SelectTile(Tile t)
    {
        t.Selected = true;
    }

    public void ClearSelectedTiles()
    {
        foreach (Tile t in tiles)
        {
            t.ClearSelection();
        }
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

    //TODO: Tolerance problems if too far away and only positive direction of lines
    //TODO: Distance restriction if tolerance problem persists
    public List<Tile> GetTilesAlongVector(Vector3 v, List<Tile> range, Tile origin, float tolerance)
    {
        List<Tile> validTiles = new List<Tile>();
        Tile next = origin;
        int iterations = 0;
        bool possibleTiles = true;

        while (possibleTiles && iterations < 100)
        {
            iterations++;
            possibleTiles = false;
            foreach (Tile t in next.AdjacentTiles)
            {
                Vector3 between = t.gameObject.transform.position - origin.gameObject.transform.position;
                //Vector3 between = origin.gameObject.transform.position - t.gameObject.transform.position;
                float dist = Vector3.Magnitude(Vector3.Cross(v, between)) / Vector3.Magnitude(v);
                if (dist <= tolerance && Vector3.Dot(v, between) > 0)
                {
                    if (range.Contains(t)  && !validTiles.Contains(t))
                    {
                        possibleTiles = true;
                        validTiles.Add(t);
                        next = t;
                        break;
                    }
                }
            }
        }

        return validTiles;
    }

    private void CalculateNextTileInLine(Tile next, Tile origin)
    {

    }

    public List<Tile> GetTilesAlongCone(Vector3 v, List<Tile> range, Tile origin, float coneExpansion, float tolerance)
    {
        return null;
    }

    public List<Tile> GetPossibleTiles(Tile startTile, int distance, bool ignoreUnits)
    {
        List<Tile> unvisitedList = new List<Tile>();
        List<Tile> visitedList = new List<Tile>();

        Tile current = startTile;

        foreach (Tile t in tiles)
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
        foreach (Tile t in visitedList)
        {
            if (t.Distance > distance)
                removalList.Add(t);
        }

        foreach (Tile t in removalList)
        {
            visitedList.Remove(t);
        }

        return visitedList;
    }

    void DijkstraStep(Tile current, ref List<Tile> unvisited, ref List<Tile> visited, int distance, bool ignoreUnits)
    {
        List<Tile> adjacent = current.AdjacentTiles;
        List<Tile> removal = new List<Tile>();
        foreach (Tile t in adjacent)
        {

            //Cannot move into occupied tiles
            if (t.Occupied && !ignoreUnits)
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
            if (cost > distance * 2)
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
        if (unvisited.Count == 0)
            return;

        Tile next = unvisited[0];

        //Get the next closest tile as the new current
        foreach (Tile t in unvisited)
        {
            if (t.Distance < next.Distance)
                next = t;
        }

        //Keep doing it
        DijkstraStep(next, ref unvisited, ref visited, distance, ignoreUnits);
    }
}
