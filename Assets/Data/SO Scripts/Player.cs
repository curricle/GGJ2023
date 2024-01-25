using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Player", order = 0)]
public class Player : Character {

    public int playerScore;

    private void OnEnable() {
        InventoryHandler.onScoreUpdate += UpdatePlayerScore;
    }

    private void OnDisable() {
        InventoryHandler.onScoreUpdate -= UpdatePlayerScore;
    }

    public void UpdatePlayerScore(int score) {
        playerScore = score;
    }

    public void ResetPlayerScore() {
        playerScore = 0;
    }
}