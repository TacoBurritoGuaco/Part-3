using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Resource { Wood, Stone, Metal, Magic } //enum that represents the resources that can be produced by buildings
//This enum will be used by both the manager to determine how much of each resource the player has and the buildings to produce said resources
public class AssignmentManager : MonoBehaviour
{
    public TMP_Dropdown dropdown; //The dropdown UI Element
    public float[] numResourcesArray = { 0, 0, 0, 0 }; //array of the number of resources currently aquired by the player
    public Resource[] resourceArray = {Resource.Wood, Resource.Stone, Resource.Metal, Resource.Magic }; //a list of every possible resource type

    public List<TextMeshProUGUI> resourceList = new List<TextMeshProUGUI>(); //List of resource text to be updated
    public List<GameObject> structureList = new List<GameObject>(); //list of structures currently placed (NOT instantiated (this will be important later))
    public static GameObject currentStructure; //the currently instantiated structure (using the dropdownUI)
    
    //update uses the list of text and updates their text to match how many resources the player has
    public void Update()
    {
        //For every text in the list, and by proxy, every resource
        for (int i = 0; i < resourceList.Count; i++)
        {
            resourceList[i].text = "X " + numResourcesArray[i]; //Updates the corresponding text using NumResourcesArray
        }
    }
    //the static function that selects the current structure highlighted in the dropdown
    //based in large part to how we did this in the week 9 (10?) characterControl script
    public static void SelectStructure(GameObject structure)
    {
        //If the currentStructure is not null and it has NOT been placed
        if (currentStructure != null && !currentStructure.GetComponent<Structure>().hasBeenPlaced)
        {
            currentStructure.GetComponent<Structure>().destroyThis(); //destroy the currently instantiated structure
        }
        currentStructure = Instantiate(structure); //sets currentStructure to an instance of the structure prefab
    }
    public void SwitchDropdown(Int32 value)
    {
        SelectStructure(structureList[value]); //start the "select structure" function
    }
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
