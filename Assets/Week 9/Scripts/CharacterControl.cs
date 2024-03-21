using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public TextMeshProUGUI selectUI;
    public static CharacterControl intance; //A variable that is a reference to a particular object of this class
    //Because it is a static variable, it allows us to speak with it in a static function
    public static Villager SelectedVillager { get; private set; }

    public void Start()
    {
        intance = this;
    }
    //Must be set up in start 

    public static void SetSelectedVillager(Villager villager)
    {
        if(SelectedVillager != null)
        {
            SelectedVillager.Selected(false);
        }
        SelectedVillager = villager;
        SelectedVillager.Selected(true);
        intance.selectUI.text = SelectedVillager.name;
    }

    ////void Update()
    ////{
    //    if (!SelectedVillager.IsUnityNull())
    //    //{
    //        selectUI.text = SelectedVillager.name;
    //    }
    //}
}
