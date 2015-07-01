using UnityEngine;
using System.Collections;

public class tbd_Default : TileBaseData {

	public tbd_Default()
    {
        _material = Resources.Load<Material>("Materials/White");

        this.Name = "Default";
        this.MoveCost = 1;
        this.VisionCost = 1;
    }
}
