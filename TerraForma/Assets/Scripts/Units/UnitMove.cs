using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour, IMovable {

    Unit unit;
    TileManager m_tiles;

    public int _move = 1;
    private int _move_current;

    private List<Tile> possibleMove = new List<Tile>();

    public List<Tile> PossibleMove
    {
        get { return possibleMove; }
    }

	// Use this for initialization
	void Start () {
        unit = GetComponent<Unit>();
        m_tiles = GameObject.FindGameObjectWithTag("GameController").GetComponent<TileManager>();
        _move_current = _move;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayMove()
    {
        possibleMove.Clear();
        m_tiles.ClearSelectedTiles();
        if (unit.PossibleMove > 0)
            possibleMove = m_tiles.GetPossibleTiles(unit.CurrentTile, unit.PossibleMove, false);

        foreach (Tile t in possibleMove)
        {
            t.Adjacent = true;
        }
    }

    public bool CanMove()
    {
        return (unit.PossibleMove > 0);
    }

    public bool TileInMove(Tile t)
    {
        return possibleMove.Contains(t);
    }

    public bool MoveTo(Tile t)
    {
        unit.PossibleMove -= (int)t.Distance;
        _move_current -= (int)t.Distance;
        unit.CurrentTile.Occupied = false;
        unit.CurrentTile = t;
        unit.CurrentTile.Occupied = true;
        unit.CurrentTile.OccupiedBy = unit;
        unit.SnapToCurrent();

        return true;
    }
}
