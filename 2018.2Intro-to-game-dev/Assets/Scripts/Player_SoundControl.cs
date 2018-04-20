using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SoundControl : MonoBehaviour {//This script controls sound of the player/main character.

	//Clips to be played
	public AudioClip walkClip;
	public AudioClip jumpClip;
	public AudioClip attackClip;


	//States tracking movePlayer's current clip
	int NONE = 0;
	int WALK = 1;
	int JUMP = 2;
	int mpCurrentClip;


	AudioSource movePlayer;
	AudioSource attackPlayer;
	Rigidbody2D myRigidbody;
	Player_Control myControl;


	// Use this for initialization
	void Start () {
		movePlayer = transform.GetChild(1).GetComponent<AudioSource> ();
		attackPlayer = transform.GetChild(2).GetComponent<AudioSource> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		myControl = GetComponent<Player_Control> ();

		mpCurrentClip = NONE;
	}


	// Update is called once per frame
	void Update () {

		//Determining walk state
		if (myRigidbody != null) {
			if (!myControl.IfStunned () && (myRigidbody.velocity.x != 0) && (myRigidbody.velocity.y == 0)) {
				PlayWalkSound ();
			} else {
				StopWalkSound ();
			}
		}
	}


	//Functions that play corresponding sound clips
	public void PlayWalkSound(){
		movePlayer.clip = walkClip;
		movePlayer.loop = true;

		if (!movePlayer.isPlaying) {
			movePlayer.Play ();
		}

		mpCurrentClip = WALK;
	}


	public void StopWalkSound(){
		if ((mpCurrentClip == WALK) && movePlayer.isPlaying) {
			movePlayer.Stop ();
		}
	}


	public void PlayJumpSound(){
		movePlayer.clip = jumpClip;
		movePlayer.loop = false;
		movePlayer.Play ();

		mpCurrentClip = JUMP;
	}

	public void PlayAttackSound(){
		attackPlayer.clip = attackClip;
		attackPlayer.loop = false;
		attackPlayer.Play ();
	}

}
