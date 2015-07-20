using UnityEngine;
using System.Collections;

public class p_Light_BlindingLight : Power {

	public p_Light_BlindingLight()
    {
        this.Name = "Blinding Light";
        this.ManaCost = 30;
        this.Target = PowerTarget.Unit;
        this.Type = PowerType.Cone;
        this.Damage = 20;
        this.Cooldown = 3;
        this.Range = 2;
        this.AOE = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
