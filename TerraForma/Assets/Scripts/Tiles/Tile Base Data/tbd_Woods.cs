using UnityEngine;
using System.Collections;

public class tbd_Woods : TileBaseData {

	public tbd_Woods()
    {
        _material = Resources.Load<Material>("Materials/Green");

        this.Name = "Woods";
        this.MoveCost = 1;
        this.VisionCost = 2;
    }
}
