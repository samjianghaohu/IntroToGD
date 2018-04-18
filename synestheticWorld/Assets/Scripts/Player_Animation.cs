using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour {//This script controls player animations

	//Delay for blinking
	public float timeUntilBlink;


	//Counter for blinking delay
	float blinkDelay;

	Animator myAnim;
	Rigidbody2D myRigidbody;

	[SerializeField] Player_Control myControl;


	// Use this for initialization
	void Start () {
		blinkDelay = timeUntilBlink;
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();


		//Initialize animation bools
		myAnim.SetBool ("IsJumping", false);
		myAnim.SetBool ("IsFalling", false);
		myAnim.SetBool ("IsWalking", false);
		myAnim.SetBool ("IsStopping", false);
		myAnim.SetBool ("IsBreathing", false);
		myAnim.SetBool ("IsBlinking", false);
		myAnim.SetBool ("IsAttacking", false);
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log (myRigidbody.velocity.y);

		if ((myRigidbody != null) && (myRigidbody.velocity.x != 0) && (myRigidbody.velocity.y == 0) && !myControl.IfStunned ()) {
			MakeWalk ();
		}else if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow)) {
			MakeStop ();
		}else if ((myRigidbody != null) && (myRigidbody.velocity.y > 0)) {
			MakeJump ();
		}else if ((myRigidbody != null) && (myRigidbody.velocity.y < 0)) {
			MakeFall ();
		}else if ((myRigidbody != null) && (myRigidbody.velocity.x == 0) && (myRigidbody.velocity.y == 0)){
			MakeBlink ();
		}
	}


	//Functions that set animation bools
	public void MakeWalk(){
		myAnim.SetBool ("IsJumping", false);
		myAnim.SetBool ("IsFalling", false);
		myAnim.SetBool ("IsWalking", true);
		myAnim.SetBool ("IsStopping", false);
		myAnim.SetBool ("IsBreathing", false);
	}


	public void MakeStop(){
		myAnim.SetBool ("IsStopping", true);
		myAnim.SetBool ("IsJumping", false);
		myAnim.SetBool ("IsFalling", false);
		myAnim.SetBool ("IsWalking", false);
		myAnim.SetBool ("IsBreathing", false);
	}


	public void MakeBlink(){
		blinkDelay -= Time.deltaTime;

		if (blinkDelay <= 0) {
			myAnim.SetBool ("IsBlinking", true);
			blinkDelay = timeUntilBlink;
		} else {
			myAnim.SetBool ("IsBlinking", false);
		}

		myAnim.SetBool ("IsBreathing", true);
		myAnim.SetBool ("IsJumping", false);
		myAnim.SetBool ("IsFalling", false);
		myAnim.SetBool ("IsWalking", false);
		myAnim.SetBool ("IsStopping", false);
	}


	public void MakeJump(){
		myAnim.SetBool ("IsJumping", true);
		myAnim.SetBool ("IsFalling", false);
		myAnim.SetBool ("IsWalking", false);
		myAnim.SetBool ("IsStopping", false);
		myAnim.SetBool ("IsBreathing", false);
	}


	public void MakeFall(){
		myAnim.SetBool ("IsJumping", false);
		myAnim.SetBool ("IsFalling", true);
		myAnim.SetBool ("IsWalking", false);
		myAnim.SetBool ("IsStopping", false);
		myAnim.SetBool ("IsBreathing", false);
	}


	public void MakeAttack(){
		//myAnim.SetBool ("IsAttacking", true);
		myAnim.SetTrigger("MakeAttack");
	}

}
