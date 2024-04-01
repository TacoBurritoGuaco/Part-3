using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Structure : MonoBehaviour
{
    //variables related to before-placing buildings
    public List<GameObject> buildingParts = new List<GameObject>(); //list of building parts (sprites)
    public bool hasBeenPlaced = false; //has this structure been placed
    public float interpolation; //interpolation value
    Coroutine builder; //The "Build" couroutine

    //Variables related to resource production
    //Time
    Coroutine production; //the Couroutine that handles resource production
    public Slider clock; //The "clock" element found above the building
    public float timerEnd; //a float that determines both the length of time for the clock
    //as well as when the clock will end and the building will create a resource.
    public float time; //the time on the clock
    //Resource
    public TextMeshProUGUI resourceText; //the text on the resource button.
    public Resource myResource; //the building's resource
    public float resourceAmount; //the amount of resources currently created by this building

    //The assignment controller, which collects resources
    public GameObject manager; //the object the manager script is attached to

    //The variables related to a building's building cost
    Coroutine resourceCheck; //the couroutine in charge of checking if the resource requirements have been met
    public Canvas costCanvas; //the canvas displaying the cost of this building
    public List<Resource> costType = new List<Resource>(); //a list of resource types this building costs
    public List<float> amount = new List<float>(); //the amount of each resource needed to meet the building's cost
    //NOTE: these will be setup in the prefabs of the buildings

    //Animation variables
    public Animator anim; //the structure's animator controller

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myResource = Resource.Wood; //sets the standard resource to wood (to be overriden)
        timerEnd = 10; //the standard time it takes for the building to create a resource (to be overriden)
        anim = GetComponent<Animator>(); //gets the animator controller
        manager = GameObject.Find("Manager"); //sets this to the manager in the hiarchy
        for (int i = 0; i < buildingParts.Count; i++)
        {
            buildingParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); //make the building parts transparent upon being initialized
        }
    }
    protected virtual void Update()
    {
        if (!hasBeenPlaced) //if the builder couroutine has not been set yet
        {
            Vector3 tempVector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition); //saves the mouseposition to a temporary vector
            tempVector3.z = 0; //sets that vector's z to 0. This prevents the object from disappearing.
            gameObject.transform.position = tempVector3; //have the object follow the mouse position
        } else {
            production = StartCoroutine(produce()); //starts the couroutine that produces resources
        }
    }
    //OnMouseDown is called whenever the object is clicked
    //This is specifically very important because clicking on the object handles every aspect of structures
    protected virtual void OnMouseDown() {
        resourceCheck = StartCoroutine(cost()); //check if the cost of the building is met
    }

    //When the button is clicked!
    protected virtual void callClick()
    {
        manager.SendMessage("resourceGain", this); //calls upon the "resource gain" function of the manager
        resourceAmount = 0; //resets the amount of resource back to 0
        resourceText.text = "X " + resourceAmount; //updates textGUI to reflect the previous reset
    }
    //Function that returns the amount of the resource this building produces
    public float reapResource()
    {
        return resourceAmount; //return the amount of the resource
    }
    //Function that returns the type of resource this building produces
    public Resource resourceType()
    {
        return myResource; //return the resource type
    }

    protected virtual IEnumerator cost()
    {
        float successNumber = 0; //the number of times the resource has been substracted
        //tempList that gets the assignment manager resource array
        Resource[] tempResourceList = manager.GetComponent<AssignmentManager>().resourceArray;
        //tempList that gets the float assignment manager array
        float[] tempFloatList = manager.GetComponent<AssignmentManager>().numResourcesArray;

        //for each costType of the structure
        for (int i = 0; i < costType.Count; i++)
        {
            //for each number in the numsResourceArray
            //Also correspondant with each resource type
            for (int j = 0; j < tempResourceList.Length; j++)
            {
                if (tempResourceList[j] == costType[i])
                { //identify if the corresponding resource in the resource array matches a needed resource
                    if (tempFloatList[j] >= amount[i])
                    { //once identified, check if the corresponding amount (float) is equal to or greater than the currently available player resources
                        tempFloatList[j] -= amount[i]; //substract this much from tempFloatList
                        successNumber++; //increase number of succeses by 1
                    }
                }
            }
        }
        //if the number of succeses is equal to the amount of resources available
        if (successNumber == 2)
        {
            builder = StartCoroutine(build()); //Start the "build" coroutine when clicking on the building
        }
        else
        {
            yield return null; //return null (do NOT build)
        }
        
    }

    //Building animation Couroutine.
    //Largely similar to the week 10 building couroutine with slight tweaks/cleanup
    //It is also now called at a different time.
    protected virtual IEnumerator build()
    {
        if (hasBeenPlaced) yield break; //stops this build couroutine from working once its happened once 
        hasBeenPlaced = true; //Has been placed is set to true
        costCanvas.GameObject().SetActive(false); //set the cost UI to false
        clock.GameObject().SetActive(true); //Set the clock to active
        //Sets all the buildings at a scale of 0
        for (int i = 0; i < buildingParts.Count; i++)
        {
            buildingParts[i].transform.localScale = new Vector3(1, 0, 1);
            buildingParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f); //make the building parts opaque upon starting building
        }

        //Increases all building parts using interpolation
        for (int i = 0; i < buildingParts.Count; i++)
        {
            while (interpolation < 1)
            {
                interpolation += 2f * Time.deltaTime;
                buildingParts[i].transform.localScale = new Vector3(Mathf.Lerp(0, 1, interpolation), Mathf.Lerp(0, 1, interpolation * 1.5f), 1);
                yield return null;
            }
            interpolation = 0;
        }
        anim.SetTrigger("productionStart"); //activates the structures' "activated" animation
    }
    //coroutine that produces resources
    protected virtual IEnumerator produce()
    {
        if (time < (timerEnd - 0.01)) //if the clock has not reached the timer end
        {
            time += Time.deltaTime * 1; //increase the Clock's time based on in-game seconds
            time = time % timerEnd; //then, turns this into a remainder based on the timerEnd value
            clock.value = time; //set clock.value to this time variable
            yield break; //Yield break
        }
        resourceAmount += 1; //increase the resource amount by 1
        time = 0; //reset clock
        resourceText.text = "X " + resourceAmount; //increases the amount of resources in the textUI
    }
}
