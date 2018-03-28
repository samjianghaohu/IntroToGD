using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SoundControl : MonoBehaviour {

	public AudioClip walkClip;
	public AudioClip jumpClip;

	int NONE = 0;
	int WALK = 1;
	int JUMP = 2;
	int mpCurrentClip = 0;

	AudioSource movePlayer;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		movePlayer = transform.GetChild(1).GetComponent<AudioSource> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Player_Control.IfStunned () == false) && (myRigidbody.velocity.x != 0) && (myRigidbody.velocity.y == 0)) {
			PlayWalkSound ();
		} else {
			StopWalkSound ();
		}
//		} else if (Input.GetKeyDown (KeyCode.Space) && (myRigidbody.velocity.y == 0)) {
//			Debug.Log ("Jump");
//			movePlayer.clip = jumpClip;
//			movePlayer.loop = false;
//			movePlayer.Play ();
//		} else {
//			movePlayer.Stop ();
//		}

	}

	public void PlayWalkSound(){
		movePlayer.clip = walkClip;
		movePlayer.loop = true;

		if (movePlayer.isPlaying == false) {
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
//		if (mpCurrentClip != JUMP && movePlayer.isPlaying) {
//			movePlayer.Stop ();
//		}

		movePlayer.clip = jumpClip;
		movePlayer.loop = false;
		movePlayer.Play ();

		mpCurrentClip = JUMP;
	}

}
