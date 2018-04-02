using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour {

	Animator myAnim;
	Rigidbody2D myRigidbody;


	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}


	// Update is called once per frame
	void Update () {
		if (myRigidbody.velocity.x != 0){
			MakeWalk ();
		}else if (myRigidbody.velocity.x == 0){
			MakeStop ();
		}
	}


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
}
