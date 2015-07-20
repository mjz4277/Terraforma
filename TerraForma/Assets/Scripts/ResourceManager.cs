using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {
    public Dictionary<Element, GameObject> unitGO = new Dictionary<Element, GameObject>();
    public Dictionary<Element, GameObject> minorUnitGO = new Dictionary<Element, GameObject>();

    void Awake()
    {
        LoadResources();
    }

	// Use this for initialization
	void Start () {
	
	}

    void LoadResources()
    {
        Debug.Log("Loading Resources");
        unitGO.Add(Element.Air, Resources.Load<GameObject>("Prefabs/Units/Air_Archer"));
        unitGO.Add(Element.Dark, Resources.Load<GameObject>("Prefabs/Units/Dark_Assassin"));
        unitGO.Add(Element.Earth, Resources.Load<GameObject>("Prefabs/Units/Earth_Shieldbearer"));
        unitGO.Add(Element.Electric, Resources.Load<GameObject>("Prefabs/Units/Electric_Fencer"));
        unitGO.Add(Element.Fire, Resources.Load<GameObject>("Prefabs/Units/Fire_Lancer"));
        unitGO.Add(Element.Ice, Resources.Load<GameObject>("Prefabs/Units/Ice_Crossbowman"));
        unitGO.Add(Element.Light, Resources.Load<GameObject>("Prefabs/Units/Light_Knight"));
        unitGO.Add(Element.Mech, Resources.Load<GameObject>("Prefabs/Units/Mech_Warrior"));
        unitGO.Add(Element.Nature, Resources.Load<GameObject>("Prefabs/Units/Nature_Axeman"));
        unitGO.Add(Element.Water, Resources.Load<GameObject>("Prefabs/Units/Water_Spearman"));

        minorUnitGO.Add(Element.Air, Resources.Load<GameObject>("Prefabs/Units/Minions/Air_Archer_Minion"));
        minorUnitGO.Add(Element.Dark, Resources.Load<GameObject>("Prefabs/Units/Minions/Dark_Assassin_Minion"));
        minorUnitGO.Add(Element.Earth, Resources.Load<GameObject>("Prefabs/Units/Minions/Earth_Shieldbearer_Minion"));
        minorUnitGO.Add(Element.Electric, Resources.Load<GameObject>("Prefabs/Units/Minions/Electric_Fencer_Minion"));
        minorUnitGO.Add(Element.Fire, Resources.Load<GameObject>("Prefabs/Units/Minions/Fire_Lancer_Minion"));
        minorUnitGO.Add(Element.Ice, Resources.Load<GameObject>("Prefabs/Units/Minions/Ice_Crossbowman_Minion"));
        minorUnitGO.Add(Element.Light, Resources.Load<GameObject>("Prefabs/Units/Minions/Light_Knight_Minion"));
        minorUnitGO.Add(Element.Mech, Resources.Load<GameObject>("Prefabs/Units/Minions/Mech_Warrior_Minion"));
        minorUnitGO.Add(Element.Nature, Resources.Load<GameObject>("Prefabs/Units/Minions/Nature_Axeman_Minion"));
        minorUnitGO.Add(Element.Water, Resources.Load<GameObject>("Prefabs/Units/Minions/Water_Spearman_Minion"));
    }
}
