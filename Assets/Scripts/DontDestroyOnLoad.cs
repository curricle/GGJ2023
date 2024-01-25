using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public List<GameObject> dontDestroyThese;
    private void Awake() {
        foreach(GameObject gObject in dontDestroyThese) {
            DontDestroyOnLoad(gObject);
        }    
    }
}
