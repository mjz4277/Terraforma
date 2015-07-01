using UnityEngine;
using System.Collections;

public class tbd_Water : TileBaseData {

	public tbd_Water()
    {
        _material = Resources.Load<Material>("Materials/LightBlue");

        this.Name = "Water";
        this.MoveCost = 2;
        this.VisionCost = 1;
    }
}
