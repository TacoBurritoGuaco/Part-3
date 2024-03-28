using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public List<GameObject> buildingParts = new List<GameObject>(); //list of building parts (sprites)
    public bool hasBeenPlaced = false; //has this structure been placed
    public float interpolation; //interpolation value
    Coroutine builder; //The "Build" couroutine
    Coroutine production; //the Couroutine that handles resource production
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buildingParts.Count; i++)
        {
            buildingParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); //make the building parts transparent upon being initialized
        }
    }

    public void Update()
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
    public virtual void OnMouseDown() {
        builder = StartCoroutine(build()); //Start the "build" coroutine when clicking on the building
    }

    //Building animation Couroutine.
    //Largely similar to the week 10 building couroutine with slight tweaks/cleanup
    //It is also now called at a different time.
    IEnumerator build()
    {
        if (hasBeenPlaced) yield break; //stops this build couroutine from working once its happened once 
        hasBeenPlaced = true; //Has been placed is set to true
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
    IEnumerator produce()
    {
        //this is used for testing for now
        print("hello!");
        yield return null;
    }
}
