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
    public Canvas resourceCanvas; //the button that the player must click to gain the correspondant resource
    public TextMeshProUGUI resourceText; //the text on the resource button.
    public Resource myResource; //the building's resource
    public float resourceAmount; //the amount of resources currently created by this building

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myResource = Resource.Wood; //sets the standard resource to wood (to be overriden)
        timerEnd = 10; //the standard time it takes for the building to create a resource (to be overriden)
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
        builder = StartCoroutine(build()); //Start the "build" coroutine when clicking on the building
    }

    //Building animation Couroutine.
    //Largely similar to the week 10 building couroutine with slight tweaks/cleanup
    //It is also now called at a different time.
    protected virtual IEnumerator build()
    {
        if (hasBeenPlaced) yield break; //stops this build couroutine from working once its happened once 
        hasBeenPlaced = true; //Has been placed is set to true
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
    }
    //coroutine that produces resources
    protected virtual IEnumerator produce()
    {
        if (time < (timerEnd - 0.01)) //if the clock has not reached the timer end
        {
            time += Time.deltaTime * 1; //increase the Clock's time based on in-game seconds
            time = time % timerEnd; //then, turns this into a remainder based on the timerEnd value
            clock.value = time; //set clock.value to this time variable
            yield break; //Yield return 
        }
        resourceAmount += 1; //increase the resource amount by 1
        time = 0; //reset clock
        resourceCanvas.GameObject().SetActive(true); //sets the resource canvas to active 
        resourceText.text = "X " + resourceAmount; //increases the amount of resources in the textUI
    }
}
