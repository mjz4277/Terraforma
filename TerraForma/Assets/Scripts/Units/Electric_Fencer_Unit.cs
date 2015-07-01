using UnityEngine;
using System.Collections;

public class Electric_Fencer_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/LightBlue");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Fencer";
        _element = Element.Electric;
        _move = 3;
        _attack = 3.0f;
        _defense = 2.0f;
        _range = 1;
        _health = 100.0f;
        _possibleMove = _move;
    }

    protected override void SetPowers()
    {
        powers[0] = new p_Electric_Thunderbolt();
        powers[1] = new p_Electric_Static();
        powers[2] = new p_Electric_Crackle();
        powers[3] = new p_Electric_FaradayCage();

        foreach(Power p in powers)
        {
            p.Owner = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
