using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stonepin : Structure
{
    //Override of start to set all necessary changes to the Stonepin Structure
    protected override void Start()
    {
        base.Start(); //Base of start (yipeee)
        myResource = Resource.Stone; //the stone resource
        timerEnd = 15; //increase the time it takes for stone to be gained
    }
}
