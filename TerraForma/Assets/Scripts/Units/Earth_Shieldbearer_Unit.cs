using UnityEngine;
using System.Collections;

public class Earth_Shieldbearer_Unit : Unit {

	// Use this for initialization
	void Start () {
        Init();
	}

    public override void LoadResources()
    {
        mat_default = Resources.Load<Material>("Materials/Brown");
        shader_default = Shader.Find("Diffuse");
        shader_selected = Shader.Find("Self-Illumin/Specular");
    }

    protected override void SetStats()
    {
        _name = "Shieldbearer";
        _element = Element.Earth;

        stats.Health = 200.0f;
        stats.Mana = 100.0f;
        stats.Move = 2;
        stats.Attack = 1.0f;
        stats.PhysicalDefense = 3.0f;
        stats.MagicDefense = 3.0f;
        stats.Range = 1;

        _health = stats.Health;
        _mana = stats.Mana;
        _possibleMove = stats.Move;
    }

    protected override void SetPowers()
    {
        powers[0] = new p_Earth_Pillar();
        powers[1] = new p_Earth_EarthShatteringBlow();
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
