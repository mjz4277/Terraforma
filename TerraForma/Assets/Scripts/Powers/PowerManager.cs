using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerManager : MonoBehaviour {

    private delegate List<Tile> PowerProcesser(Tile chosen, Tile origin, List<Tile> range);
    private PowerProcesser ProcessPower;

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
                ProcessPower = ProcessAreaPower;
                break;
            case PowerType.Biome:
                ProcessPower = ProcessBiomePower;
                break;
            case PowerType.Burst:
                ProcessPower = ProcessBurstPower;
                break;
            case PowerType.Cone:
                ProcessPower = ProcessConePower;
                break;
            case PowerType.Line:
                ProcessPower = ProcessLinePower;
                break;
            default:
                Debug.Log("ERROR: Power type [" + currentPower.Type + "] not recognized");
                break;
        }

        tilesInPower = ProcessPower(chosen, origin, range);

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

    private List<Tile> ProcessLinePower(Tile chosen, Tile origin, List<Tile> range)
    {
        Vector3 line = GetVectorBetweenTiles(origin, chosen);
        List<Tile> tilesInPower = m_tiles.GetTilesAlongVector(line, range, origin, chosen, 0.01f);
        return tilesInPower;
    }

    private List<Tile> ProcessConePower(Tile chosen, Tile origin, List<Tile> range)
    {
        Vector3 line = GetVectorBetweenTiles(chosen, origin);
        List<Tile> tilesInPower = m_tiles.GetTilesAlongCone(line, range, origin, 3.0f, 0.5f);
        return tilesInPower;
    }

    private List<Tile> ProcessBiomePower(Tile chosen, Tile origin, List<Tile> range)
    {
        return null;
    }

    private Vector3 GetVectorBetweenTiles(Tile t1, Tile t2)
    {
        Vector3 v = t1.gameObject.transform.position - t2.gameObject.transform.position;
        return v;
    }
}