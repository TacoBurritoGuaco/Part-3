using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conservatory : Structure
{
    //Override of start to set all necessary changes to the Stonepin Structure
    protected override void Start()
    {
        base.Start(); //Base of start (yipeee)
        myResource = Resource.Magic; //the stone resource
        timerEnd = 60; //increase the time it takes for stone to be gained
    }
}
