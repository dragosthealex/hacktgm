using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

	public static List<string> types;

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
			GameObject platform = Instantiate (platformPrefab, Vector3.zero, Quaternion.identity);
			platform.transform.parent = gameObject.transform;
			break;
		default:
			break;
		}
	}
}
