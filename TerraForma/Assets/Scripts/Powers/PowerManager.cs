using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerManager : MonoBehaviour {

    TileManager m_tiles;

    private Power currentPower;

    public Power CurrentPower
    {
        get { return currentPower; }
        set { currentPower = value; }
    }

	// Use this for initialization
	void Start () {
        m_tiles = GameObject.FindGameObjectWithTag("GameController").GetComponent<TileManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public List<Tile> ProcessPowerType(Tile chosen, Tile origin)
    {
        List<Tile> range = m_tiles.GetPossibleTiles(origin, currentPower.Range, true);
        List<Tile> tilesInPower = new List<Tile>();

        switch(currentPower.Type)
        {
            case PowerType.Area:
                tilesInPower = ProcessAreaPower(chosen, origin, range);
                break;
            case PowerType.Burst:
                tilesInPower = ProcessBurstPower(chosen, origin, range);
                break;
        }

        return tilesInPower;
    }

    private List<Tile> ProcessAreaPower(Tile chosen, Tile origin, List<Tile> range)
    {
        if (!range.Contains(chosen))
        {
            chosen = m_tiles.GetNearestPossibleTile(chosen, range);
        }

        List<Tile> tilesInPower = m_tiles.GetPossibleTiles(chosen, currentPower.AOE, true);

        return tilesInPower;
    }

    private List<Tile> ProcessBurstPower(Tile chosen, Tile origin, List<Tile> range)
    {
        List<Tile> tilesInPower = m_tiles.GetPossibleTiles(origin, currentPower.AOE, true);

        return tilesInPower;
    }

    public void GetPossibleTargets(Power p)
    {
    }

    private void GetUnitsInRange(Power p)
    {

    }

    private void GetTilesInRange(Power p)
    {
    }
}
