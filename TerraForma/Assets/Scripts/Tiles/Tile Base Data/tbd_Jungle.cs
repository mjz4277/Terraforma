using UnityEngine;
using System.Collections;

public class tbd_Jungle : TileBaseData {

	public tbd_Jungle()
    {
        _material = Resources.Load<Material>("Materials/DarkGreen");

        this.Name = "Jungle";
        this.MoveCost = 2;
        this.VisionCost = 2;
    }
}
