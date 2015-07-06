using UnityEngine;
using System.Collections;

public class Mech_Warrior_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/DarkGrey");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Warrior";
        _element = Element.Mech;

        stats.Health = 100.0f;
        stats.Mana = 100.0f;
        stats.Move = 2;
        stats.Attack = 2.0f;
        stats.PhysicalDefense = 2.0f;
        stats.MagicDefense = 2.0f;
        stats.Range = 1;

        _health = stats.Health;
        _mana = stats.Mana;
        _possibleMove = stats.Move;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
