using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_Earth_Pillar : Power {

	public p_Earth_Pillar()
    {
        this.Name = "Pillar";
        this.ManaCost = 30;
        this.Target = PowerTarget.Tile;
        this.Type = PowerType.Area;
        this.Damage = 0;
        this.Cooldown = 3;
        this.Range = 2;
        this.AOE = 0;
    }

    public override void UsePower(List<Tile> tiles, List<Unit> units)
    {
        foreach (Tile t in tiles)
        {
            t.ChangeTileBaseData("Mountain");
        }
        base.UsePower(tiles, units);
    }
}
