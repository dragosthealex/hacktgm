using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameObject instance;

	public GameObject player;

	void Awake () {
		// Don't destroy stuff
		if (GameManager.instance && GameManager.instance != this) {
			Destroy (gameObject);
		} else if (!GameManager.instance) {
			DontDestroyOnLoad (gameObject);
			GameManager.instance = gameObject;
		}

		// If scene is ingame, get player
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
