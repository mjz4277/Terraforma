using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Earth_EarthShatteringBlow : Power {

	public p_Earth_EarthShatteringBlow()
    {
        this.Name = "Earth-Shattering Blow";
        this.ManaCost = 50;
        this.Target = PowerTarget.Both;
        this.Type = PowerType.Area;
        this.Damage = 20;
        this.Cooldown = 4;
        this.Range = 1;
        this.AOE = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        Unit u_target = units[0];
        u_target.TakeDamage(_damage);
        Debug.Log("Unit [" + u_target.Name + "] took [" + _damage + "] damage");
        u_target.AddStatusEffect(new se_Immobilize("Earth-Shattering Blow", 1));
        Debug.Log("Unit [" + u_target.Name + "] is IMMOBILIZED");
        Tile t_target = u_target.CurrentTile;
        foreach(Tile t in t_target.AdjacentTiles)
        {
            t.ChangeTileBaseData("Rubble");
        }
        base.UsePower(tiles, units);
    }
}
