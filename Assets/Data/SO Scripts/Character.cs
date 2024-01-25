using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]

public class Character : ScriptableObject {
    
    public enum CharacterType { PLAYER, NPC }

    public Inventory characterInventory;
    public Sprite characterSprite;

    public string characterName;

    public CharacterType characterType;

    public GameObject characterPrefab;

}