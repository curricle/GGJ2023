using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    //Delegates
    public static Action<bool> onAllItemsAcquired;
    public static Action<int> onScoreUpdate;

    public static Action<Item> onItemAddedToInventory;
    public static Action<Inventory> onQueryInventory;
    
    //bools
    private bool hasFruit;
    private bool hasMilk;
    private bool hasEggs;
    private bool hasWater;
    private bool hasFlour;
    private bool hasSugar;
     private bool allItemsAcquired;

    //ints
    private int totalScore = 0;

    //misc
    public Inventory inventory;

    private void OnEnable() {
        ItemHandler.onItemPickup += AddItemToInventory;
        ExchangeItem.onGiveItem += AddItemToInventory;
        Stove.onStoveInteract += AssessRequirements;
        GameManager.onGameRestarted += ResetInventory;
    }

    private void OnDisable() {
        ItemHandler.onItemPickup -= AddItemToInventory;
        ExchangeItem.onGiveItem -= AddItemToInventory;
        Stove.onStoveInteract -= AssessRequirements;
        GameManager.onGameRestarted -= ResetInventory;
    }

    private void AddItemToInventory(Item item) {

        Debug.Log("we're adding the item to the inventory");

        var hasItem = false;

        foreach(Item inventoryItem in inventory.inventoryItems) {
            if(inventoryItem == item) {
                hasItem = true;
                break;
            }
        }

        if(!hasItem) {
            inventory.AddItem(item);

                if(item.type == Item.Type.FRUIT) {
                    hasFruit = true;
                }
                if(item.type == Item.Type.MILK) {
                    hasMilk = true;
                }
                if(item.type == Item.Type.EGGS) {
                    hasEggs = true;
                }
                if(item.type == Item.Type.FLOUR) {
                    hasFlour = true;
                }
                if(item.type == Item.Type.WATER) {
                    hasWater = true;
                }
        }

        CheckScore();     
        onItemAddedToInventory?.Invoke(item);
        onQueryInventory?.Invoke(inventory);
        
    }

    public void CheckInventory() {

        for(int i = 0; i < inventory.inventoryItems.Count; i++) {
            if(inventory.inventoryItems[i].type == Item.Type.EGGS) {
                hasEggs = true;
            }
            if(inventory.inventoryItems[i].type == Item.Type.WATER) {
                hasWater = true;
            }
            if(inventory.inventoryItems[i].type == Item.Type.FLOUR) {
                hasFlour = true;
            }
            if(inventory.inventoryItems[i].type == Item.Type.MILK) {
                hasMilk = true;
            }
            if(inventory.inventoryItems[i].type == Item.Type.FRUIT) {
                hasFruit = true;
            }
            if(inventory.inventoryItems[i].type == Item.Type.SUGAR) {
                hasSugar = true;
            }
        }
    }

    public void AssessRequirements() {

        CheckInventory();

        if(hasFruit && hasMilk && hasEggs && hasFlour && hasWater && hasSugar) {
            allItemsAcquired = true;
            CheckScore();
        }
        else {
            allItemsAcquired = false;
        }

        onAllItemsAcquired?.Invoke(allItemsAcquired);

    }

    public void CheckScore() {

        totalScore = 0;

        for(int i = 0; i < inventory.inventoryItems.Count; i++) {
            totalScore += inventory.inventoryItems[i].scoreValue;
        }
    
        onScoreUpdate?.Invoke(totalScore);
    }

    private void ResetInventory() {
        inventory.inventoryItems = new List<Item>();
        CheckScore();
    }
}
