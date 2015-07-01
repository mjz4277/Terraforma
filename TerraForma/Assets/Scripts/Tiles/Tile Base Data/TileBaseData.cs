using UnityEngine;
using System.Collections;

public class TileBaseData {

    protected Material _material;

    protected string _name;
    protected int _moveCost;
    protected int _visionCost;

    public Material Material
    {
        get { return _material; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    
    public int MoveCost
    {
        get { return _moveCost; }
        set { if (value < 0) _moveCost = 0; else _moveCost = value; }
    }

    public int VisionCost
    {
        get { return _visionCost; }
        set { if (value < 0) _visionCost = 0; else _visionCost = value; }
    }

    public TileBaseData()
    {

    }
}
