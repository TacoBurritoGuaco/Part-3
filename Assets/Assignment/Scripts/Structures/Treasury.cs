using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasury : Structure
{
    protected override void Start()
    {
        base.Start(); //Base of start (yipeee)
        TreasureRandomizer(); //Randomizes the resource it produces upon being instantiated
        timerEnd = 30; //increase the time it takes for the random resource to be gained
    }
    //Function that upon being called randomizes the resource that this building produces
    private void TreasureRandomizer()
    {
        int randomValue = Random.Range(0, 100); //randomizes this value when called
        if (randomValue >= 0 && randomValue < 41) //becomes wood when landing a value between 0 and 40
        {
            myResource = Resource.Wood;
        }
        if (randomValue >= 41 && randomValue < 71) //becomes stone when landing a value between 41 and 70
        {
            myResource = Resource.Stone;
        }
        if (randomValue >= 71 && randomValue < 96)
        { //Becomes metal when landing a value between 71 and 95
            myResource = Resource.Metal;
        }
        if (randomValue >= 96 && randomValue <= 100) //If randomValue is between 96 and 100
        {
            myResource = Resource.Metal;
        }
    }
    //override of CallClick to override the treasury's resource whenever it is called
    protected override void callClick()
    {
        TreasureRandomizer(); //randomizes the resource currently dispensed by the treasury afterwards
        base.callClick(); //uses the base of the structure 
    }
}
