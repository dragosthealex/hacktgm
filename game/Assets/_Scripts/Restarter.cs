using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restarter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
			
			StartCoroutine(Die());
        }
    }

	IEnumerator Die()
	{
		float len = UI.instance.musicScript.playDeath ();
		GameManager.instance.isDead = true;
		UI.instance.panelsScript.showDeathPanel ();
		Camera.main.GetComponent<Camera2DFollow> ().enabled = false;
		Destroy (GameManager.instance.player);
		yield return new WaitForSeconds(len + 0.2f);
		UI.instance.panelsScript.hideDeathPanel ();
		SceneManager.LoadScene(1);
	}
}
