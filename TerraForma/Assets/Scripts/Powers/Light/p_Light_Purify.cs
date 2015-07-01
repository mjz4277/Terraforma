using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Light_Purify : Power {

	public p_Light_Purify()
    {
        this.Name = "Purify";
        this.ManaCost = 40;
        this.Target = PowerTarget.Tile;
        this.Type = PowerType.Burst;
        this.Damage = 0;
        this.Cooldown = 4;
        this.Range = 0;
        this.AOE = 2;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        foreach(Tile t in tiles)
        {
            t.ChangeTileBaseData("Default");
        }
        base.UsePower(tiles, units);
    }
}
