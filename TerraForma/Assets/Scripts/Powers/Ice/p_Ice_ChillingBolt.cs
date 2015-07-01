using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Ice_ChillingBolt : Power {

	public p_Ice_ChillingBolt()
    {
        this.Name = "Chilling Bolt";
        this.ManaCost = 40;
        this.Target = PowerTarget.Unit;
        this.Type = PowerType.Area;
        this.Damage = 10;
        this.Cooldown = 3;
        this.Range = 3;
        this.AOE = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        Unit u_target = units[0];
        if (u_target.Team != _owner.Team)
        {
            u_target.TakeDamage(_damage);
            u_target.AddStatusEffect(new se_Slow("Chilling Bolt", 2, 1));
            Debug.Log("Unit [" + units[0].Name + "] took [" + _damage + "] damage");
            Debug.Log("Unit [" + units[0].Name + "] SLOWED");
            base.UsePower(tiles, units);
        }
    }
}
