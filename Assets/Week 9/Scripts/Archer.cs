using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Villager
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public float delay = 0.5f;

    protected override void Attack()
    {
        destination = transform.position; //arbitrary change, but illustrates the point
        base.Attack();
        Invoke("arrowSpawn", delay);
    }

    void arrowSpawn()
    {
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
