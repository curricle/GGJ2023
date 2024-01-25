using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomCakeSelector : MonoBehaviour
{
    public List<Sprite> cakeSprites;

    private void Awake() {

        gameObject.GetComponent<Image>().sprite = cakeSprites[UnityEngine.Random.Range(0, cakeSprites.Count)];
    }
}
