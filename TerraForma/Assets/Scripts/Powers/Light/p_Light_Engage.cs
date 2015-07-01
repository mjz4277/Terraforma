using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Light_Engage : Power {

	public p_Light_Engage()
    {
        this.Name = "Engage";
        this.ManaCost = 30;
        this.Target = PowerTarget.Unit;
        this.Type = PowerType.Area;
        this.Damage = 20;
        this.Cooldown = 2;
        this.Range = 1;
        this.AOE = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        Unit u_target = units[0];
        if (u_target.Team != _owner.Team)
        {
            u_target.TakeDamage(_damage);
            u_target.AddStatusEffect(new se_Immobilize("Engage", 1));
            _owner.AddStatusEffect(new se_Immobilize("Engage", 2));
            Debug.Log("Unit [" + units[0].Name + "] took [" + _damage + "] damage");
            Debug.Log("Unit [" + units[0].Name + "] IMMOBILIZED");
            Debug.Log("Unit [" + _owner.Name + "] IMMOBILIZED");
            base.UsePower(tiles, units);
        }
    }
}
