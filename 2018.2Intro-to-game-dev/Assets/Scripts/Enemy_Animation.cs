using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour {//This script controls enemy animation

	//Time between each blink
	public float timeUntilBlink;
	float blinkDelay;


	Animator myAnim;
	Rigidbody2D myRigidbody;


	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();


		//Initialize bink delay
		blinkDelay = timeUntilBlink;
	}


	// Update is called once per frame
	void Update () {
		if (myRigidbody.velocity.x != 0){
			MakeWalk ();
		}else if (myRigidbody.velocity.x == 0){
			MakeStop ();
		}


		//Blink every timeUntilBink
		blinkDelay -= Time.deltaTime;
		if (blinkDelay <= 0) {
			MakeBlink ();
			blinkDelay = timeUntilBlink;
		}
	}


	//Functions that set animation bools
	public void MakeWalk(){
		myAnim.SetBool ("IsWalking", true);
		myAnim.SetBool ("IsStopping", false);
	}


	public void MakeStop(){
		myAnim.SetBool("IsStopping", true);
		myAnim.SetBool ("IsWalking", false);
	}


	public void MakeWarn(){
		myAnim.SetBool ("IsWarning", true);
	}


	public void StopWarn(){
		myAnim.SetBool ("IsWarning", false);
	}


	public void MakeBlink(){
		myAnim.SetTrigger ("MakeBlink");
	}


	public void MakeAttack(){
		myAnim.SetTrigger("MakeAttack");
	}


	public void MakeStun(){
		myAnim.SetBool ("IsStunned", true);
	}


	public void StopStun(){
		myAnim.SetBool ("IsStunned", false);
	}
}
