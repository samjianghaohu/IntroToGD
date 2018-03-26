using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SoundControl : MonoBehaviour {

	public AudioClip walk;
	public AudioClip jump;

	AudioSource movePlayer;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		movePlayer = transform.GetChild(1).GetComponent<AudioSource> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myRigidbody != null) {
			if ((Player_Control.IfStunned () == false) && (myRigidbody.velocity.x != 0) && (myRigidbody.velocity.y == 0)) {
				movePlayer.clip = walk;
				movePlayer.loop = true;

				if (movePlayer.isPlaying == false) {
					movePlayer.Play ();
				}
			} else {
				if ((movePlayer.clip == walk) && movePlayer.isPlaying) {
					movePlayer.Stop ();
				}
			}

			if ((Input.GetKeyDown (KeyCode.Space)) && (myRigidbody.velocity.y == 0)) {
				movePlayer.clip = jump;
				movePlayer.loop = false;
				movePlayer.Play ();
			}
		}
	}
}
