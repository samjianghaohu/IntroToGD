using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SoundControl : MonoBehaviour {//This script controls enemy audio

	//Audio clips to be played
	public AudioClip attack;
	public AudioClip right;
	public AudioClip wrong;

	AudioSource attackPlayer;
	AudioSource responsePlayer;


	// Use this for initialization
	void Start () {
		attackPlayer = transform.GetChild(1).GetComponent<AudioSource> ();
		responsePlayer = transform.GetChild(3).GetComponent<AudioSource> ();
	}


	// Update is called once per frame
	void Update () {
		
	}


	public void PlayAttackSound(){
		attackPlayer.clip = attack;
		attackPlayer.loop = false;
		attackPlayer.Play ();
	}


	public void PlayResponseSound(int rightOrWrong){
		if (rightOrWrong == 0) {
			responsePlayer.clip = wrong;
		} else {
			responsePlayer.clip = right;
		}

		responsePlayer.loop = false;
		responsePlayer.Play ();
	}
}
