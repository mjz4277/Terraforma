using UnityEngine;
using System.Collections;

public class Ice_Crossbowman_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/PaleBlue");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Crossbowman";
        _element = Element.Ice;
        _move = 2;
        _attack = 1.0f;
        _defense = 3.0f;
        _range = 3;
        _health = 100.0f;
        _possibleMove = _move;
    }

    protected override void SetPowers()
    {
        powers[0] = new p_Ice_ChillingBolt();
        powers[1] = new Power();
        powers[1].Range = 2;
        powers[2] = new Power();
        powers[2].Range = 3;
        powers[2].Type = PowerType.Area;
        powers[2].AOE = 1;
        powers[3] = new Power();
        powers[3].Range = 4;

        foreach(Power p in powers)
        {
            p.Owner = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
