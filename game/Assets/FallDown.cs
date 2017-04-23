using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {

	// Use this for initialization
	public Collider2D coll;
	public Animator anim;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{	
			anim.SetBool ("IsDead", true);
			StartCoroutine(LemmeJump());

			}
	}
	IEnumerator LemmeJump(){
		yield return new WaitForSeconds (0.5f);
		Destroy (transform.parent.gameObject);
	}
}
