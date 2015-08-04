using UnityEngine;
using System.Collections;

public class tbd_DeepWater : TileBaseData {

	public tbd_DeepWater()
    {
        _material = Resources.Load<Material>("Materials/DarkBlue");

        this.Name = "Deep Water";
        this.MoveCost = 10;
        this.VisionCost = 1;
    }
}
