using System;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeItem : MonoBehaviour
{

    public Item item;
    public static Action<Item> onGiveItem;
    public static Action<GameObject> onSendSelf;

    public string selfName;
    public List<string> selfDialogue;

    public bool hasExchanged = false;

    public GameObject self;


    private void Awake() {
        self = gameObject;
        

    }
    private void OnEnable() {
        PlayerActions.onExchangeItem += GiveItem;
    }

    private void OnDisable() {
        PlayerActions.onExchangeItem -= GiveItem;        
    }

    public void GiveItem() {
        if(!hasExchanged) {
            onGiveItem?.Invoke(item);  
            onSendSelf?.Invoke(self); 
            hasExchanged = true;
        }
        else {
            onSendSelf?.Invoke(self);  
        }
        
    }

}
