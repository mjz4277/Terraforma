using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Fire_Ignite : Power {

	public p_Fire_Ignite()
    {
        this.Name = "Ignite";
        this.ManaCost = 30;
        this.Target = PowerTarget.Unit;
        this.Type = PowerType.Area;
        this.Damage = 10;
        this.Cooldown = 2;
        this.Range = 2;
        this.AOE = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        Unit u_target = units[0];
        if (u_target.Team != _owner.Team)
        {
            u_target.TakeDamage(_damage);
            u_target.AddStatusEffect(new se_DOT("Ignite", 2, 5.0f));
            Debug.Log("Unit [" + units[0].Name + "] took [" + _damage + "] damage");
            Debug.Log("Unit [" + units[0].Name + "] TAKING [" + 5.0f + "] DOT");
            base.UsePower(tiles, units);
        }
    }
}
