using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {
    public Dictionary<Element, GameObject> units = new Dictionary<Element,GameObject>();

	// Use this for initialization
	void Awake () {
        LoadResources();
	}

    void LoadResources()
    {
        units.Add(Element.Air, Resources.Load<GameObject>("Prefabs/Units/Air_Archer"));
        units.Add(Element.Dark, Resources.Load<GameObject>("Prefabs/Units/Dark_Assassin"));
        units.Add(Element.Earth, Resources.Load<GameObject>("Prefabs/Units/Earth_Shieldbearer"));
        units.Add(Element.Electric, Resources.Load<GameObject>("Prefabs/Units/Electric_Fencer"));
        units.Add(Element.Fire, Resources.Load<GameObject>("Prefabs/Units/Fire_Lancer"));
        units.Add(Element.Ice, Resources.Load<GameObject>("Prefabs/Units/Ice_Crossbowman"));
        units.Add(Element.Light, Resources.Load<GameObject>("Prefabs/Units/Light_Knight"));
        units.Add(Element.Mech, Resources.Load<GameObject>("Prefabs/Units/Mech_Warrior"));
        units.Add(Element.Nature, Resources.Load<GameObject>("Prefabs/Units/Nature_Axeman"));
        units.Add(Element.Water, Resources.Load<GameObject>("Prefabs/Units/Water_Spearman"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
