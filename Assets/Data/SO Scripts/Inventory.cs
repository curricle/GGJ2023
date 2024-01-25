using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public List<Item> inventoryItems = new List<Item>();

    public void AddItem(Item item) {
        
        inventoryItems.Add(item);
        
      }
}