using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {
    public Dictionary<Element, GameObject> units = new Dictionary<Element,GameObject>();
    public Dictionary<Element, GameObject> minorUnits = new Dictionary<Element, GameObject>();

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

        minorUnits.Add(Element.Air, Resources.Load<GameObject>("Prefabs/Units/Minions/Air_Archer_Minion"));
        minorUnits.Add(Element.Dark, Resources.Load<GameObject>("Prefabs/Units/Minions/Dark_Assassin_Minion"));
        minorUnits.Add(Element.Earth, Resources.Load<GameObject>("Prefabs/Units/Minions/Earth_Shieldbearer_Minion"));
        minorUnits.Add(Element.Electric, Resources.Load<GameObject>("Prefabs/Units/Minions/Electric_Fencer_Minion"));
        minorUnits.Add(Element.Fire, Resources.Load<GameObject>("Prefabs/Units/Minions/Fire_Lancer_Minion"));
        minorUnits.Add(Element.Ice, Resources.Load<GameObject>("Prefabs/Units/Minions/Ice_Crossbowman_Minion"));
        minorUnits.Add(Element.Light, Resources.Load<GameObject>("Prefabs/Units/Minions/Light_Knight_Minion"));
        minorUnits.Add(Element.Mech, Resources.Load<GameObject>("Prefabs/Units/Minions/Mech_Warrior_Minion"));
        minorUnits.Add(Element.Nature, Resources.Load<GameObject>("Prefabs/Units/Minions/Nature_Axeman_Minion"));
        minorUnits.Add(Element.Water, Resources.Load<GameObject>("Prefabs/Units/Minions/Water_Spearman_Minion"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
