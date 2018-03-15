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
		UpdateState ();


		if (state == BREATHE){
			MakeBlink ();
		}
		if (state == WALK) {
			//myAnim.SetTrigger ("MakeWalk");
		}
		if (state == STOP) {
			//myAnim.SetTrigger ("MakeStop");
		}
	}

	void UpdateState (){
//		if (myRigidbody.velocity == new Vector2 (0, 0) && state = BREATHE) {
//			state = BREATHE;
//		} else {
//			blinkDelay = timeUntilBlink;
//		}
		//if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
		if ((myRigidbody != null) && (myRigidbody.velocity.x != 0) && (Player_Control.IfStunned() == false)){
			//state = WALK;
			myAnim.SetTrigger ("MakeWalk");
		} else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
			//state = STOP;
			myAnim.SetTrigger ("MakeStop");
		} else {
			state = BREATHE;
		}

		if (state != BREATHE) {
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
