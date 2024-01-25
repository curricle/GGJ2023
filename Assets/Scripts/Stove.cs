using System;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public static Action onStoveInteract;
    public bool canBake;

    private void Awake() {
        canBake = true;
    }

    private void OnEnable() {
        PlayerActions.onBakeCake += BakeCake;
    }

    private void OnDisable() {
        PlayerActions.onBakeCake -= BakeCake;
    }

    private void BakeCake() {
        if(canBake) {
            Debug.Log("this stove can bake!");
            onStoveInteract?.Invoke();
        }
    }

}
