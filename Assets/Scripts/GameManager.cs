using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Delegates
    public static Action<int> onGameWon;
    public static Action onGameRestarted;

    public static Action<Item> onMissingIngredient;
    public static Action onSetEra;

    //bools
    public static bool isGamePaused = false;
    public static bool isInStore = false;

    //misc
    public List<GameObject> mapsList;
    public List<GameObject> groceryMapsList;
    private Player player;
    public static GameManager instance;

    private void Awake() {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerActions>().player;
    }

    private void OnEnable() {
        UIHandler.onExitButtonPressed += QuitGame;
        UIHandler.onRestartButtonPressed += RestartGame;

        PlayerActions.onGamePaused += PauseGame;
        UIHandler.onResumeButtonPressed += PauseGame;

        EntranceExitHandler.onEnterExitBuilding += ChangeScene;

        InventoryHandler.onAllItemsAcquired += AssessRequirements;
        SceneManager.sceneLoaded += SetEraOnSceneLoad;

    }

    private void OnDisable() {
        UIHandler.onExitButtonPressed -= QuitGame;
        UIHandler.onRestartButtonPressed -= RestartGame;

        PlayerActions.onGamePaused -= PauseGame;
        UIHandler.onResumeButtonPressed -= PauseGame;

        EntranceExitHandler.onEnterExitBuilding -= ChangeScene; 

        InventoryHandler.onAllItemsAcquired -= AssessRequirements;
        SceneManager.sceneLoaded -= SetEraOnSceneLoad;
        
    }

    private void PauseGame(bool isPaused) {
        if(isPaused) {
            isGamePaused = true;
            Time.timeScale = 0;
        }
        else {
           isGamePaused = false;
            Time.timeScale = 1; 
        }
        
    }

    private void SetEraOnSceneLoad(Scene scene, LoadSceneMode mode) {
        onSetEra?.Invoke();
    }

    public void AssessRequirements(bool canWin) {

        if(canWin) {
            onGameWon?.Invoke(player.playerScore);
        }
        else {
            onMissingIngredient?.Invoke(null);
        }
    }

    private void ChangeScene(int buildingToSceneID) {
        if(buildingToSceneID == 0) {
            SceneManager.LoadScene(1);
            isInStore = false;
        }
        if(buildingToSceneID == 1) {
            SceneManager.LoadScene(2);
        }
        if(buildingToSceneID == 2) {
            isInStore = true;
            onSetEra.Invoke();
        }
        if(buildingToSceneID == 3) {
            isInStore = false;
            onSetEra.Invoke();
        }
    }

    public void RestartGame() {
        Debug.Log("we're restarting");
        onGameRestarted?.Invoke();
        StartCoroutine(OnLoadScene(1));
    }

    IEnumerator OnLoadScene(int sceneIndex) {

        //Play scene transition animation. get the animator. i guess
        yield return new WaitForSeconds(0);

        SceneManager.LoadScene(sceneIndex);
    }


    public void QuitGame() {
        onGameRestarted?.Invoke();
        Application.Quit();
    }

}
