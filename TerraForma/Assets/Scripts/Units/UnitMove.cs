using UnityEngine;
using System.Collections;

public class UnitMove : MonoBehaviour, IMovable {

    Unit unit;

	// Use this for initialization
	void Start () {
        unit = GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayMove()
    {
        throw new System.NotImplementedException();
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
