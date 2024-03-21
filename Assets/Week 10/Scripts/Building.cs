using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<GameObject> buildingParts = new List<GameObject>();
    public float interpolation;
    Coroutine builder; //The "Build" couroutine
    // Start is called before the first frame update
    void Start()
    {
        builder = StartCoroutine(build());
    }

    IEnumerator build()
    {
        //Sets all the buildings at a scale of 0
        for (int i = 0; i < buildingParts.Count; i++)
        {
            buildingParts[i].transform.localScale = new Vector3(1, 0, 1);
        }

        //Increases all building parts using interpolation
        for (int i = 0; i < buildingParts.Count; i++)
        {
            while (interpolation < 1)
            {
                interpolation += 2f * Time.deltaTime;
                buildingParts[i].transform.localScale = new Vector3(Mathf.Lerp(0, 1, interpolation), Mathf.Lerp(0, 1, interpolation*1.5f), 1);
                yield return null;
            }
            interpolation = 0;
        }
    }
}
