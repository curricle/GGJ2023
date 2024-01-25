using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject {

    public enum Type { MILK, EGGS, WATER, FRUIT, FLOUR, STOVE, SUGAR, STORAGE }
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public GameObject itemGameObject;
    public Transform itemTransform;
    public int scoreValue;

    public int itemID;

    public bool isDestroyed;

    public Type type;
    
}

