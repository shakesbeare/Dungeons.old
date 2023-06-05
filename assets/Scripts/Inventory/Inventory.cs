using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static List<Item> itemList; // collectable items
    private static Spell[] spells; // permanent progression
    private static Weapon weapon;

    void Awake()
    {
        itemList = new List<Item>();
        spells = new Spell[2];
    }
}
