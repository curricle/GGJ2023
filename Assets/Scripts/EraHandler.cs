using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraHandler : MonoBehaviour
{

    public static int eraIndex = 0;
    public static EraHandler Instance;

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable() {
        PlayerActions.onChangeEra += UpdateEraIndex;
    }

    private void OnDisable() {
        PlayerActions.onChangeEra -= UpdateEraIndex;
    }

    public void UpdateEraIndex() {
        if(eraIndex < 3) {
            eraIndex++;
        }
        if(eraIndex == 3) {
            eraIndex = 0;
        }
    }
}
