using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell : MonoBehaviour {

	public static List<string> types = new List<string>(){"platform", 
		                                                  "empty",
														  "spikes",
	                                                      "fallPlatform"};

	public string type;
	public GameObject content;

	public GameObject platformPrefab;

	public void Awake() {
		type = "empty";
		drawType ();
	}

	public void setType(string newType) {
		type = newType;
		drawType ();
	}

	public void drawType() {
		switch (type) {
		case("empty"):
			// pass
			if (content) {
				Destroy (content);
			}
			break;
		case("platform"):
			// Draw platform
			GameObject platform = Instantiate (platformPrefab);
			platform.transform.parent = gameObject.transform;
			platform.transform.localPosition = Vector3.zero;
			content = platform;
			break;
		default:
			break;
		}
	}
}
