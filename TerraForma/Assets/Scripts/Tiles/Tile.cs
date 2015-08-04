using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

    private List<Tile> adjacentTiles = new List<Tile>();

    public Material mat_default;
    public Material mat_selected;
    public Material mat_adjacent;

    public TileBaseData tbd;

    float moveCost = 1;
    float distance = 1;
    int visibility;

    bool occupied;
    Unit occupiedBy;

    bool selected;
    bool adjacent;

    public List<Tile> AdjacentTiles
    {
        get { return adjacentTiles; }
        set 
        { 
            adjacentTiles = value; 
        }
    }

    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }

    public bool Adjacent
    {
        get { return adjacent; }
        set { adjacent = value; }
    }

    public bool Occupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    public Unit OccupiedBy
    {
        get { return occupiedBy; }
        set { occupiedBy = value; }
    }

    public float MoveCost
    {
        get { return moveCost; }
        set { moveCost = value; }
    }

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MeshRenderer tile_renderer = gameObject.GetComponentInChildren<MeshRenderer>();
        if (selected)
        {
            tile_renderer.material = mat_selected;
        }
        else if(adjacent)
        {
            tile_renderer.material = mat_adjacent;
        }
        else
        {
            tile_renderer.material = mat_default;
        }
	}

    public void ChangeTileBaseData(string tileType)
    {
        switch(tileType)
        {
            case "Rubble":
                tbd = new tbd_Rubble();
                break;
            case "Mountain":
                tbd = new tbd_Mountain();
                break;
            case "Jungle":
                tbd = new tbd_Jungle();
                break;
            case "Woods":
                tbd = new tbd_Woods();
                break;
            case "Water":
                tbd = new tbd_Water();
                break;
            case "DeepWater":
                tbd = new tbd_DeepWater();
                break;
            case "Default":
                tbd = new tbd_Default();
                break;
            default:
                tbd = new tbd_Default();
                break;
        }

        mat_default = tbd.Material;
        this.MoveCost = tbd.MoveCost;
    }

    public void ClearSelection()
    {
        selected = false;
        adjacent = false;
    }
}
