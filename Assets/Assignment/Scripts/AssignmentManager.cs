using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource { Wood, Stone, Metal, Magic } //enum that represents the resources that can be produced by buildings
//This enum will be used by both the manager to determine how much of each resource the player has and the buildings to produce said resources
public class AssignmentManager : MonoBehaviour
{
    public float[] numResourcesArray = { 0, 0, 0, 0 }; //array of the number of resources currently aquired by the player
    public Resource[] resourceArray = { Resource.Wood, Resource.Stone, Resource.Metal, Resource.Magic }; //The list of resources, correspondant with the num array

    public List<Structure> structureList = new List<Structure>(); //list of structures currently placed (NOT instantiated (this will be important later))
    public static Structure currentStructure; //the currently instantiated structure (using the dropdownUI)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
