using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;     

public class PostProcessingHandler : MonoBehaviour
{

    public List<PostProcessProfile> effectsList;
    private PostProcessVolume postProcessVolume;
    private PostProcessProfile currentProfile;

    private void Awake() {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile = currentProfile;
    }

    private void OnEnable() {
        PlayerActions.onChangeEra += OnSetEra;
        GameManager.onSetEra += OnSetEra;
    }

    private void OnDisable() {
        PlayerActions.onChangeEra -= OnSetEra;
        GameManager.onSetEra -= OnSetEra;
    }

    private void OnSetEra() { 
        currentProfile = effectsList[EraHandler.eraIndex];
        postProcessVolume.profile = currentProfile;
        Debug.Log($"Era set! {currentProfile}, {EraHandler.eraIndex}");
    }
}
