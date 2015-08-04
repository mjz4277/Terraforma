using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BiomeLookupEntry : MonoBehaviour
{

    public string _name;

    public List<TileEntry> tileDict = new List<TileEntry>();

    public string BiomeName
    {
        get { return _name; }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddTileTolerance(float tol, string tile)
    {
        TileEntry entry = new TileEntry(tile, tol);
        tileDict.Add(entry);
    }

    public string GetTileType(float tol)
    {
        string type = "Default";

        foreach (TileEntry t in tileDict)
        {
            if (t.tolerance <= tol) type = t.type;
        }

        return type;
    }
}

[Serializable]
public class TileEntry
{
    public string type;
    public float tolerance;

    public TileEntry(string type, float tol)
    {
        this.type = type;
        this.tolerance = tol;
    }
}