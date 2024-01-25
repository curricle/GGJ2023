using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    public static Action<GameObject, bool> canInteract;
    public Item item;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            canInteract?.Invoke(gameObject, true);
            Debug.Log($"Current gameobject collider: {gameObject}");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        canInteract?.Invoke(gameObject, false);
    }
}
