using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapsHandler : MonoBehaviour
{
    public List<GameObject> mapsList;
    public List<GameObject> groceryMapsList;

    private void OnEnable() {
        PlayerActions.onChangeEra += OnSetEra;
        GameManager.onSetEra += OnSetEra;
    }

    private void OnDisable() {
        PlayerActions.onChangeEra -= OnSetEra;
        GameManager.onSetEra -= OnSetEra;
    }

      private void OnSetEra() {

        if(!GameManager.isInStore) {
            for(int i = 0; i < mapsList.Count; i++) {
                groceryMapsList[i].SetActive(false);
                mapsList[i].SetActive(false);
            }

            mapsList[EraHandler.eraIndex].SetActive(true);
            Debug.Log($"Current map: {mapsList[EraHandler.eraIndex]}, {EraHandler.eraIndex}");
        }
        else {
            for(int i = 0; i < groceryMapsList.Count; i++) {
                groceryMapsList[i].SetActive(false);
                mapsList[i].SetActive(false);
            }

            groceryMapsList[EraHandler.eraIndex].SetActive(true);
        }
        
    }

}
