using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Electric_Thunderbolt : Power {

	public p_Electric_Thunderbolt()
    {
        _name = "Thunderbolt";
        _manaCost = 20;
        e_target = PowerTarget.Unit;
        e_type = PowerType.Area;
        _damage = 20;
        _cooldown = 3;
        _range = 2;
        _aoe = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        Unit u_target = units[0];
        if(u_target.Team != _owner.Team)
        {
            u_target.TakeDamage(_damage);
            Debug.Log("Unit [" + units[0].Name + "] took [" + _damage + "] damage");
            base.UsePower(tiles, units);
        }
    }
}
