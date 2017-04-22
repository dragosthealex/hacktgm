using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameObject instance;

	public GameObject player;
	public GameObject env;
	public List<GameObject> chunks;

	public Chunk chunkPrefab;

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
			env = GameObject.FindGameObjectWithTag ("Env");
			chunks = new List<GameObject> ();
			int start_platform = Random.Range (0, 5);
			player.transform.position = new Vector3 (1, 4 * start_platform + 2);
			// Generate first chunk
			Chunk chunk = Instantiate(chunkPrefab);
			chunk.transform.parent = env.transform;
			chunk.generate (new List<int>(){start_platform});
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
