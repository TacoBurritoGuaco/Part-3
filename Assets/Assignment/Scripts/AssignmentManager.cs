using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource { Wood, Stone, Metal, Magic } //enum that represents the resources that can be produced by buildings
//This enum will be used by both the manager to determine how much of each resource the player has and the buildings to produce said resources
public class AssignmentManager : MonoBehaviour
{
    public float[] numResourcesArray = { 0, 0, 0, 0 }; //array of the number of resources currently aquired by the player
    public Resource[] resourceArray = {Resource.Wood, Resource.Stone, Resource.Metal, Resource.Magic }; //a list of every possible resource type

    public List<Structure> structureList = new List<Structure>(); //list of structures currently placed (NOT instantiated (this will be important later))
    public static Structure currentStructure; //the currently instantiated structure (using the dropdownUI)

    //The function which adds an amount of resource of each type to the manager
    public void resourceGain(Structure structure) //takes in a structure
    {
        //Switch statement that uses the given structure's structure type to determine how much of each resource said structure should give the player
        //Once it has determined which resource it is taking in, add the "reapResource" float to the corresponding spot in the float array.
        switch (structure.resourceType()) { 
            case Resource.Wood:
                numResourcesArray[0] += structure.reapResource();
                break;
            case Resource.Stone:
                numResourcesArray[1] += structure.reapResource();
                break;
            case Resource.Metal:
                numResourcesArray[2] += structure.reapResource();
                break;
            case Resource.Magic:
                numResourcesArray[3] += structure.reapResource();
                break;
        }
    } 
}
