using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public static UI instance;

	public PlayMusic musicScript;
	public ShowPanels panelsScript;

	void Awake()
	{
		if (UI.instance && UI.instance != this) {
			Destroy (gameObject);
			return;
		} else if (!UI.instance) {
			DontDestroyOnLoad (gameObject);
			UI.instance = this;
		}

		musicScript = GetComponent<PlayMusic> ();
		panelsScript = GetComponent<ShowPanels> ();
	}

	

}
