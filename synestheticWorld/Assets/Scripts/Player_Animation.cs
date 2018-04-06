using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour {//This scripts control player animations

	//delay for blinking
	public float timeUntilBlink;


	//counter for blinking delay
	float blinkDelay;

	Animator myAnim;
	Rigidbody2D myRigidbody;

	[SerializeField] Player_Control myControl;


	// Use this for initialization
	void Start () {
		blinkDelay = timeUntilBlink;
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}


	// Update is called once per frame
	void Update () {
		if ((myRigidbody != null) && (myRigidbody.velocity.x != 0) && (myRigidbody.velocity.y == 0) && (myControl.IfStunned () == false)) {
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


	//functions that set animation bools
	public void MakeWalk(){
		myAnim.SetBool ("IsJumping", false);
		myAnim.SetBool ("IsFalling", false);
		myAnim.SetBool ("IsWalking", true);
		myAnim.SetBool ("IsStopping", false);
		myAnim.SetBool ("IsBreathing", false);
	}


	public void MakeStop(){
		Debug.Log ("Stop");
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
		myAnim.SetBool ("IsAttacking", true);
	}


	public void StopAttack(){
		myAnim.SetBool ("IsAttacking", false);
	}
}
