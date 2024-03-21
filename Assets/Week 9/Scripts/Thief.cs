using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Villager
{
    public GameObject knifePrefab;
    public Transform spawnPoint;
    public float delay = 0.1f; //from week 9

    //From week 10
    Coroutine dashing; //Couroutines are both objects and functions!
    float dashSpeed = 10;

    protected override void Attack()
    {
        if (dashing != null)
        {
            StopCoroutine(dashing);
        }
        dashing = StartCoroutine(Dash()); //Starts the couroutine!
    }
    IEnumerator Dash()
    {
        //Dash towards the mouse
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        speed = dashSpeed;
        while( speed > 3 ) //While this condition is true (Speed is greater than the base speed)
        {
            yield return null; //Come back next frame and see if this condition is true or not
        }

        //Perform base attack animation
        base.Attack();

        //Instantiate knives
        yield return new WaitForSeconds(0.1f); //Waits a certain amount of time to perform the rest of the couroutine
        Instantiate(knifePrefab, spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(0.2f); //Waits a certain amount of time to perform the rest of the couroutine
        Instantiate(knifePrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public override ChestType CanOpen()
    {
        return ChestType.Thief;
    }

    //Not necessary for my version of the project, however, it is still a very useful thing to keep in mind, *specially* for my systems project
    //public override string ToString()
    //{
    //    return "Thief!";
    //}
}
