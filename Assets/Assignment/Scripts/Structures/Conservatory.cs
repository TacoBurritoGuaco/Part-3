using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conservatory : Structure
{
    //Override of start to set all necessary changes to the Conservatory Structure
    protected override void Start()
    {
        base.Start(); //Base of start (yipeee)
        myResource = Resource.Magic; //the magic (sparkle) resource
        timerEnd = 50; //increase the time it takes for magic to be gained by a TON
    }
}
