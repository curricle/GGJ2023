using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //Delegates
    public static Action onChangeEra;
    public static Action onBakeCake;
    public static Action onExchangeItem;
    public static Action<bool> onGamePaused;

    //bools
    private bool canInteract;

    //misc
    private GameObject currentInteractObject;
    public Player player;

    private void OnEnable() {
        InteractHandler.canInteract += onCanInteract;
    }

    private void OnDisable() {
        InteractHandler.canInteract -= onCanInteract;
    }

    private void OnPause() {
        Debug.Log("Pause button pressed");
        onGamePaused?.Invoke(!GameManager.isGamePaused);
    }

    private void OnChangeEra() {
        onChangeEra?.Invoke();
    }

    private void onCanInteract(GameObject interactObject, bool canPlayerInteract) {
        canInteract = canPlayerInteract;
        currentInteractObject = interactObject;

        if(!canPlayerInteract) {
            currentInteractObject = null;
        }
    }

    private void OnInteract() {
        if(canInteract) {
            if(currentInteractObject.GetComponent<InteractHandler>().item) {
                if(currentInteractObject.GetComponent<InteractHandler>().item.type == Item.Type.STOVE) {
                    onBakeCake?.Invoke();
                }
            }
    
            if(currentInteractObject.GetComponent<ExchangeItem>()) {
                onExchangeItem?.Invoke();
            }            
            
        }
    }
}
