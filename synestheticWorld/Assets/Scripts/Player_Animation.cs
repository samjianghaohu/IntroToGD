using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour {
	public float timeUntilBlink;

	int STILL = 0;
	int MOVE = 1;
	int state = 0;

	float blinkDelay;

	Animator myAnim;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		blinkDelay = timeUntilBlink;
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateState ();

		if (state == STILL){
			MakeBlink ();
		}
	}

	void UpdateState (){
		if (myRigidbody.velocity == new Vector2 (0, 0)) {
			state = STILL;
		} else {
			state = MOVE;
			blinkDelay = timeUntilBlink;
		}
	}

	void MakeBlink(){
		blinkDelay -= Time.deltaTime;

		if (blinkDelay <= 0) {
			myAnim.SetTrigger ("MakeBlink");
			blinkDelay = timeUntilBlink;
		}
	}
}
