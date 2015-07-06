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
	
	// Update is called once per frame
	void Update () {
	
	}
}