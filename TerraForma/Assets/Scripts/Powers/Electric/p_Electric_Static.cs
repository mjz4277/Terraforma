using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Electric_Static : Power {

	public p_Electric_Static()
    {
        this.Name = "Static";
        this.ManaCost = 40;
        this.Target = PowerTarget.Unit;
        this.Type = PowerType.Area;
        this.Damage = 5;
        this.Cooldown = 4;
        this.Range = 3;
        this.AOE = 1;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        foreach (Unit u in units)
        {
            if (u.Team != _owner.Team)
            {
                u.TakeDamage(_damage);
                Debug.Log("Unit [" + u.Name + "] took [" + _damage + "] damage");
                u.AddStatusEffect(new se_Silence("Static", 1));
                Debug.Log("Unit [" + u.Name + "] is SILENCED");
            }
        }

        base.UsePower(tiles, units);
    }
}
