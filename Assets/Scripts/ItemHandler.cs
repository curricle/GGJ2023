using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHandler : MonoBehaviour
{
    public static Action<Item> onItemPickup;
    public Item currentItem;

    public int itemID;

    public GameObject currentGameObject;

    private void Awake() {
        /*if(!currentItem.isDestroyed) {
            DestroyCurrentObject(currentGameObject);
        }
        else {
            ResetDestroyable();
        }*/
    }

    private void OnEnable() {
        GameManager.onGameRestarted += ResetDestroyable;
        UIHandler.onRestartButtonPressed += TestFunc;
    }

    private void OnDisable() {
        GameManager.onGameRestarted -= ResetDestroyable;
        UIHandler.onRestartButtonPressed -= TestFunc;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Debug.Log("Entered collider2d for item");
            onItemPickup?.Invoke(currentItem);
            DestroyCurrentObject(currentGameObject);
        }
    }

    private void DestroyCurrentObject(GameObject currentObject) {
        currentItem.isDestroyed = true;
        currentObject.SetActive(false);
    }

    //these aren't being called and i think it's because the new scene is loading before they can fire but it's definitely an ordering issue
    private void ResetDestroyable() {
        Debug.Log("I'm resetting");
        currentItem.isDestroyed = false;
        currentGameObject.SetActive(true);
        }

    private void TestFunc() {
        Debug.Log("Test working");
    }
}
