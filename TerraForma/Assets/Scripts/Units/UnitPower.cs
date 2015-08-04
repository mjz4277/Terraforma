using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitPower : MonoBehaviour {

    private List<Power> powers = new List<Power>();

    private Power selectedPower;

	// Use this for initialization
	void Start () {
        Power[] arr_powers = gameObject.GetComponents<Power>();
        foreach(Power p in arr_powers)
        {
            powers.Add(p);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectPower(int i)
    {
        selectedPower = powers[i];
    }

    public void DisplayPower()
    {

    }

    public void UsePower(List<Tile> tiles, List<Unit> units)
    {
        selectedPower.UsePower(tiles, units);
    }
}
