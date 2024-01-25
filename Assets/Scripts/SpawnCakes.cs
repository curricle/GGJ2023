using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCakes : MonoBehaviour
{
    public Transform cakePrefab;
    public GameObject cakeLayoutGroup;

    public Transform spawnExit;
    public Transform spawnEnter;

    public float speed = 1f;

    private void Update() {
        var step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, spawnExit.position, step);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other == spawnExit.GetComponent<Collider2D>()) {
            Instantiate(cakePrefab, spawnEnter.position, cakePrefab.rotation);
        }
        
    }
}
