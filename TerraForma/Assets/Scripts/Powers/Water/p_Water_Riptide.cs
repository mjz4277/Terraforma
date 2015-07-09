using UnityEngine;
using System.Collections;

public class p_Water_Riptide : Power {

	public p_Water_Riptide()
    {
        this.Name = "Riptide";
        this.ManaCost = 30;
        this.Target = PowerTarget.Both;
        this.Type = PowerType.Line;
        this.Damage = 20;
        this.Cooldown = 3;
        this.Range = 3;
        this.AOE = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
