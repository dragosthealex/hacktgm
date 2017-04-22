using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public GameObject[] Obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;

    // Use this for initialization
    void Start () {
        Debug.Log("Spawwwn");
        Spawn();
    }

    void Spawn() {
        Debug.Log("Helloo");
        Instantiate(Obj[Random.Range(0, Obj.Length)], transform.position,
                Quaternion.identity);
        Invoke ("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
