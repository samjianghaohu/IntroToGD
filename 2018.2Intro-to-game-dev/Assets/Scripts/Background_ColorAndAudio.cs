﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_ColorAndAudio : MonoBehaviour {//This scripts manages background color and audio

	//Declare color-audio groups
	public Color color0;
	public Color match0;
	public AudioClip clip0;
	
	public Color color1;
	public Color match1;
	public AudioClip clip1;
	
	public Color color2;
	public Color match2;
	public AudioClip clip2;
	
	public Color color3;
	public Color match3;
	public AudioClip clip3;


	GameObject player;
	GameObject moveBG;
	Camera cam;
	AudioSource bgmPlayer;
	AudioClip bgmClip;

	SpriteRenderer mySpriteRenderer;
	SpriteRenderer playerSprite;

	Dictionary<Color, Color> colorDict = new Dictionary<Color, Color> ();
	Dictionary<Color, AudioClip> audioDict = new Dictionary<Color, AudioClip> ();


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		moveBG = GameObject.FindWithTag ("MovingBG");
		cam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
		bgmPlayer = transform.parent.GetComponent<AudioSource> ();

		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		playerSprite = GameObject.FindWithTag ("Player").GetComponent<SpriteRenderer> ();


		//Intialize dictionaries
		InitializeDict ();
	}


	// Update is called once per frame
	void Update () {
		ChangeBGColor ();
		ChangeBGM ();
	}


	void InitializeDict(){
		colorDict.Add (color0, match0);
		colorDict.Add (color1, match1);
		colorDict.Add (color2, match2);
		colorDict.Add (color3, match3);

		audioDict.Add (color0, clip0);
		audioDict.Add (color1, clip1);
		audioDict.Add (color2, clip2);
		audioDict.Add (color3, clip3);
	}


	//Change background color according to player's color
	void ChangeBGColor(){
		if (!Stage_Utilities.compareColorsLoose (playerSprite.color, Color.red)) {
			moveBG.GetComponent<SpriteRenderer> ().color = playerSprite.color;


			//Get the matching color from the dictionary
			Color matchedColor;
			colorDict.TryGetValue (new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f), out matchedColor);
			matchedColor = new Color (matchedColor.r, matchedColor.g, matchedColor.b, 255);


			//Color the background with the matching color
			mySpriteRenderer.color = matchedColor;
			cam.backgroundColor = matchedColor;
		}
	}


	//Change background music according to player's color
	void ChangeBGM(){
		Debug.Log (bgmPlayer.isPlaying);

		//Get the matching audio clip from the dictionary if player isn't chaning color because of stun
		if (!player.GetComponent<Player_Control>().IfStunned()) {
			audioDict.TryGetValue (playerSprite.color, out bgmClip);
		}


		//Update clip when clip changes
		if (bgmPlayer.clip != bgmClip) {
			bgmPlayer.clip = bgmClip;
		}


		//Set playback to loop and play if the clip isn't null and the player isn't playing
		bgmPlayer.loop = true;

		if ((bgmPlayer.clip != null) && (bgmPlayer.isPlaying == false)) {
			bgmPlayer.Play ();
		}
	}
}
