using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject player;
	public GameObject env;
	public GameObject bg;
	public List<GameObject> chunks;
	public GameObject bg1Prefab;
	public GameObject bg2Prefab;
	public GameObject bg3Prefab;
	// Sentiments
	public double happy;
	public double sad;
	public double angry;
	public double neutral;
	public double surprised;

	public Chunk chunkPrefab;

	void Awake () {
		// Don't destroy stuff
		if (GameManager.instance && GameManager.instance != this) {
			Destroy (gameObject);
			return;
		} else if (!GameManager.instance) {
			DontDestroyOnLoad (gameObject);
			GameManager.instance = this;
		}
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneFinishedLoading;
	}

	void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
		// If scene is ingame, get player
		if (scene.buildIndex == 1) {
			player = GameObject.FindGameObjectWithTag ("Player");
			env = GameObject.FindGameObjectWithTag ("Env");
			chunks = new List<GameObject> ();
			int start_platform = Random.Range (0, 6);
			player.transform.position = new Vector3 (1, 4 * start_platform + 2);
			// Generate first chunk
			Chunk chunk = Instantiate(chunkPrefab);
			chunk.transform.parent = env.transform;
			chunk.generate (new List<int>(){start_platform});
			chunks.Add (chunk.gameObject);
			// Generate rest of them up to 5
			for (int i = 1; i < 5; i++) {
				chunk = Instantiate (chunkPrefab);
				chunk.transform.parent = env.transform;
				chunk.transform.localPosition = new Vector3(Chunk.WIDTH * 2 * i, 0);
				chunk.generate (new List<int> (){ start_platform });
				chunks.Add (chunk.gameObject);
			}
			// Generate two bg chunks
			GameObject bg = GameObject.FindGameObjectWithTag ("Background");
			Transform bg1 = bg.transform.GetChild (0);
			Transform bg2 = bg.transform.GetChild (1);
			Transform bg3 = bg.transform.GetChild (2);
			GameObject cur;
			for (int i = 1; i < 3; i++) {
				int coordX = i * 122 + 33;
				instantiateBgChunk (bg1Prefab, bg1, coordX);
				instantiateBgChunk (bg2Prefab, bg2, coordX);
				instantiateBgChunk (bg3Prefab, bg3, coordX);
			}
		}
	}

	private void instantiateBgChunk(GameObject prefab, Transform theParent, float coordX) {
		GameObject cur = Instantiate (prefab);
		cur.transform.parent = theParent;
		cur.transform.position = new Vector3(coordX, 5);
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex != 1) {
			return;
		}
		// Check if player is in second chunk
		if (player.transform.position.x > 
			chunks [0].transform.position.x + 
			Chunk.WIDTH * 2 + 10) {
			// Delete first chunk and generate sixth
			GameObject toDelete = chunks[0];
			chunks.Remove (toDelete);
			Destroy (toDelete);
			Chunk chunk = Instantiate(chunkPrefab);
			chunk.transform.parent = env.transform;
			// Set its position to after last chunk
			GameObject last = chunks [chunks.Count - 1];
			chunk.transform.localPosition = new Vector3 (last.transform.position.x + Chunk.WIDTH * 2, 0);
			// TODO: Use nice algorithm with blocks
			chunk.generate (new List<int>(){5});
			chunks.Add (chunk.gameObject);
		}
		// Do same stuff with bg
		GameObject bg = GameObject.FindGameObjectWithTag ("Background");
		Transform bg1 = bg.transform.GetChild (0);
		Transform bg2 = bg.transform.GetChild (1);
		Transform bg3 = bg.transform.GetChild (2);
		if (player.transform.position.x > bg1.GetChild (1).transform.position.x + 10) {
			Transform last = bg1.GetChild (bg1.childCount - 1);
			instantiateBgChunk (bg1Prefab, bg1, last.position.x + 122);
			// Destroy prev
			Destroy(bg1.GetChild(0).gameObject);
		}
		if (player.transform.position.x > bg2.GetChild (1).transform.position.x + 10) {
			Transform last = bg2.GetChild (bg2.childCount - 1);
			instantiateBgChunk (bg2Prefab, bg2, last.position.x + 122);
			// Destroy prev
			Destroy(bg2.GetChild(0).gameObject);
		}
		if (player.transform.position.x > bg3.GetChild (1).transform.position.x + 10) {
			Transform last = bg3.GetChild (bg3.childCount - 1);
			instantiateBgChunk (bg3Prefab, bg3, last.position.x + 122);
			// Destroy prev
			Destroy(bg3.GetChild(0).gameObject);
		}
			
	}
}
