using UnityEngine;
using System.Collections;

public class Fire_Lancer_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/Red");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Lancer";
        _element = Element.Fire;

        stats.Health = 100.0f;
        stats.Mana = 100.0f;
        stats.Move = 2;
        stats.Attack = 3.0f;
        stats.PhysicalDefense = 1.0f;
        stats.MagicDefense = 1.0f;
        stats.Range = 2;

        _health = stats.Health;
        _mana = stats.Mana;
        _possibleMove = stats.Move;
    }

    protected override void SetPowers()
    {
        powers[0] = new p_Fire_Ignite();
        powers[1] = new Power();
        powers[1].Range = 2;
        powers[2] = new Power();
        powers[2].Range = 3;
        powers[2].Type = PowerType.Area;
        powers[2].AOE = 0;
        powers[3] = new Power();
        powers[3].Name = "Meteor";
        powers[3].Range = 4;
        powers[3].Type = PowerType.Area;
        powers[3].AOE = 1;
        powers[3].Target = PowerTarget.Both;

        foreach(Power p in powers)
        {
            p.Owner = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}