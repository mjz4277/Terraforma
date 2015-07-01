using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Electric_Crackle : Power {

	public p_Electric_Crackle()
    {
        _name = "Crackle";
        _manaCost = 50;
        e_target = PowerTarget.Unit;
        e_type = PowerType.Area;
        _damage = 0;
        _cooldown = 4;
        _range = 0;
        _aoe = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        base.UsePower(tiles, units);
    }
}
