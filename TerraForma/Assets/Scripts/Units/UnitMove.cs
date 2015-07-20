using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour, IMovable {

    Unit unit;
    TileManager m_tiles;

	// Use this for initialization
	void Start () {
        unit = GetComponent<Unit>();
        m_tiles = GameObject.FindGameObjectWithTag("GameController").GetComponent<TileManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Tile> DisplayMove()
    {
        List<Tile> possibleTiles = new List<Tile>();
        m_tiles.ClearSelectedTiles();
        if (unit.PossibleMove > 0)
            possibleTiles = m_tiles.GetPossibleTiles(unit.CurrentTile, unit.PossibleMove, false);

        foreach (Tile t in possibleTiles)
        {
            t.Adjacent = true;
        }

        return possibleTiles;
    }

    public void MoveTo(Tile t)
    {
        unit.PossibleMove -= (int)t.Distance;
        unit.CurrentTile.Occupied = false;
        unit.CurrentTile = t;
        unit.CurrentTile.Occupied = true;
        unit.CurrentTile.OccupiedBy = unit;
        unit.SnapToCurrent();
    }
}
