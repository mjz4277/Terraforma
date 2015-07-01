using UnityEngine;
using System.Collections;

public class Light_Knight_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/White");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Knight";
        _element = Element.Light;
        _move = 2;
        _attack = 2.0f;
        _defense = 3.0f;
        _range = 1;
        _health = 100.0f;
        _possibleMove = _move;
    }

    protected override void SetPowers()
    {
        powers[0] = new p_Light_Engage();
        powers[1] = new p_Light_Purify();
        powers[2] = new Power();
        powers[2].Range = 3;
        powers[2].Type = PowerType.Area;
        powers[2].AOE = 1;
        powers[3] = new Power();
        powers[3].Range = 4;

        foreach (Power p in powers)
        {
            p.Owner = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
