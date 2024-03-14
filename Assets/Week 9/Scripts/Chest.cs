using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChestType { Villager, Mechant, Archer }
public class Chest : MonoBehaviour
{
    public Animator animator;
    public ChestType whoCanOpen; //enum
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Villager>(out Villager villager)) //REMEMBER THIS LATER
        {
            if (villager.CanOpen() == whoCanOpen || whoCanOpen == ChestType.Villager)
            {
                animator.SetBool("IsOpened", true);
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("IsOpened", false);
    }
}
