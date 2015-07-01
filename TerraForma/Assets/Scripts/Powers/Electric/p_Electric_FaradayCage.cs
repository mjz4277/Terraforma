using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Electric_FaradayCage : Power {

	public p_Electric_FaradayCage()
    {
        _name = "Faraday Cage";
        _manaCost = 100;
        e_target = PowerTarget.Unit;
        e_type = PowerType.Burst;
        _damage = 20;
        _cooldown = 8;
        _aoe = 4;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        foreach (Unit u in units)
        {
            if (u.Team != _owner.Team)
            {
                u.TakeDamage(_damage);
                Debug.Log("Unit [" + u.Name + "] took [" + _damage + "] damage");
            }
        }

        base.UsePower(tiles, units);
    }
}
