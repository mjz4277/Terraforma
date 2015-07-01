using UnityEngine;
using System.Collections;

public class Air_Archer_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/Silver");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Archer";
        _element = Element.Air;
        _move = 3;
        _attack = 1.0f;
        _defense = 1.0f;
        _range = 3;
        _health = 100.0f;
        _possibleMove = _move;
    }

    protected override void SetPowers()
    {
        powers[0] = new Power();
        powers[0].Type = PowerType.Burst;
        powers[0].AOE = 2;
        powers[0].Range = 1;
        powers[1] = new Power();
        powers[1].Range = 2;
        powers[2] = new Power();
        powers[2].Range = 3;
        powers[2].Type = PowerType.Area;
        powers[2].AOE = 1;
        powers[3] = new Power();
        powers[3].Range = 4;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
