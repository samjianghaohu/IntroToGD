using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour {
	public float timeUntilBlink;

	int BREATHE = 0;
	int WALK = 1;
	int STOP = 2;
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
		if ((myRigidbody != null) && (myRigidbody.velocity.x != 0) && (Player_Control.IfStunned() == false)){
			myAnim.SetBool ("IsWalking", true);
			myAnim.SetBool ("IsStopping", false);
		} else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
			myAnim.SetBool ("IsWalking", false);
			myAnim.SetBool ("IsStopping", true);
		} else {
			MakeBlink ();
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
