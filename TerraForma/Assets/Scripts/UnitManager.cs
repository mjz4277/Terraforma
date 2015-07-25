using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {

    private List<Unit> units = new List<Unit>();

    public void Init()
    {
        GameObject[] obj_units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < obj_units.Length; i++)
        {
            Unit u = obj_units[i].GetComponent<Unit>();
            units.Add(u);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void ClearSelectedUnits()
    {
        foreach (Unit u in units)
        {
            u.Selected = false;
        }
    }

    public void ResetUnits()
    {
        foreach (Unit u in units)
        {
            u.ResetUnit();
        }
    }
}
