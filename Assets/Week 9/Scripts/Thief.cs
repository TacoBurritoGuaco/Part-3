using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Villager
{
    public GameObject knifePrefab;
    public Transform spawnPoint;
    public float delay = 0.1f;
    protected override void Attack()
    {
        destination = transform.position; //arbitrary change, but illustrates the point
        base.Attack();
        //spawns two knives with a delay between them
        Invoke("knifeSpawn", delay);
        Invoke("knifeSpawn", delay + 0.2f);
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            speed = 10f;
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void knifeSpawn()
    {
        Instantiate(knifePrefab, spawnPoint.position, spawnPoint.rotation);
        speed = 3;
    }
    public override ChestType CanOpen()
    {
        return ChestType.Thief;
    } 
}
