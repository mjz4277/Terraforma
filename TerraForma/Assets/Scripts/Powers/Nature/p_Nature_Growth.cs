using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Nature_Growth : Power {

	public p_Nature_Growth()
    {
        this.Name = "Growth";
        this.ManaCost = 40;
        this.Target = PowerTarget.Tile;
        this.Type = PowerType.Area;
        this.Damage = 0;
        this.Cooldown = 4;
        this.Range = 3;
        this.AOE = 1;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        foreach(Tile t in tiles)
        {
            t.ChangeTileBaseData("Woods");
        }
        base.UsePower(tiles, units);
    }
}
