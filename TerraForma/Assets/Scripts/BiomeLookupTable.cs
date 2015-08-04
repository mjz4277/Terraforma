using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BiomeLookupTable : MonoBehaviour {

    public Dictionary<string, BiomeLookupEntry> biomeLookupDict = new Dictionary<string, BiomeLookupEntry>();

    void Start()
    {
        BiomeLookupEntry[] entries = GetComponents<BiomeLookupEntry>();

        foreach(BiomeLookupEntry e in entries)
        {
            biomeLookupDict.Add(e.BiomeName, e);
        }
    }
}
