using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_ColorAndAudio : MonoBehaviour {
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

	public GameObject camera;
	public GameObject player;
	public GameObject moveBG;

	Camera cam;
	AudioSource bgmPlayer;
	Dictionary<Color, Color> colorDict = new Dictionary<Color, Color> ();
	Dictionary<Color, AudioClip> audioDict = new Dictionary<Color, AudioClip> ();
	SpriteRenderer mySpriteRenderer;
	SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {
		cam = camera.GetComponent<Camera> ();
		bgmPlayer = transform.parent.GetComponent<AudioSource> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		playerSprite = player.GetComponent<SpriteRenderer> ();

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
		
	void ChangeBGColor(){
		if (Stage_Utilities.compareColorsLoose (playerSprite.color, Color.red) == false) {
			moveBG.GetComponent<SpriteRenderer> ().color = playerSprite.color;

			Color matchedColor;
			colorDict.TryGetValue (playerSprite.color, out matchedColor);
			matchedColor = new Color (matchedColor.r, matchedColor.g, matchedColor.b, 255);
			mySpriteRenderer.color = matchedColor;
			cam.backgroundColor = matchedColor;
		}
	}

	void ChangeBGM(){
		if (Stage_Utilities.compareColorsLoose (playerSprite.color, Color.red) == false) {
			
			AudioClip bgmClip;
			audioDict.TryGetValue (playerSprite.color, out bgmClip);

			if (bgmPlayer.clip != bgmClip) {
				bgmPlayer.clip = bgmClip;
				bgmPlayer.loop = true;

				if (bgmPlayer.isPlaying == false) {
					bgmPlayer.Play ();
				}
			}
		}
	}
}
