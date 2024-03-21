using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI selectUI;
    public List<Villager> villagerList = new List<Villager>();
    float villagerSelected;
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

    public void changeCharacter(Int32 value)
    {
        SetSelectedVillager(villagerList[value]);
        villagerSelected = value;
    }

    public void characterScale(Single value)
    {
        villagerList[(int)villagerSelected].transform.localScale = Vector3.Lerp(new Vector3 (0.5f, 0.5f, 0.5f), new Vector3(2, 2, 2), value);
    }
    ////void Update()
    ////{
    //    if (!SelectedVillager.IsUnityNull())
    //    //{
    //        selectUI.text = SelectedVillager.name;
    //    }
    //}
}
