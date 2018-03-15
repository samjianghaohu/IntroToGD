using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour {
	int STILL = 0;
	int WALK = 1;
	int state = 0;

	Animator myAnim;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myRigidbody.velocity.x != 0 && state != WALK){
			myAnim.SetTrigger ("MakeWalk");
			state = WALK;
		}else if (myRigidbody.velocity.x == 0 && state != STILL){
			myAnim.SetTrigger("MakeStop");
			state = STILL;
		}
	}
}
