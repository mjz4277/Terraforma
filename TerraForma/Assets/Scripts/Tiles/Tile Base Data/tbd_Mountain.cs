using UnityEngine;
using System.Collections;

public class tbd_Mountain : TileBaseData  {

	public tbd_Mountain()
    {
        _material = Resources.Load<Material>("Materials/Brown");

        this.Name = "Mountain";
        this.MoveCost = 10;
        this.VisionCost = 10;
    }
}
