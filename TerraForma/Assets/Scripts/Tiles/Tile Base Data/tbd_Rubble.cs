using UnityEngine;
using System.Collections;

public class tbd_Rubble : TileBaseData {

	public tbd_Rubble()
    {
        _material = Resources.Load<Material>("Materials/Tan");

        this.Name = "Rubble";
        this.MoveCost = 2;
        this.VisionCost = 1;
    }
}
