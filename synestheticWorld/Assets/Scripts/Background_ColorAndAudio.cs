using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_ColorAndAudio : MonoBehaviour {
	public Color color0;
	public Color match0;
	
	public Color color1;
	public Color match1;
	
	public Color color2;
	public Color match2;
	
	public Color color3;
	public Color match3;

	public GameObject camera;
	public GameObject player;
	public GameObject moveBG;

	Camera cam;
	Dictionary<Color, Color> colorDict = new Dictionary<Color, Color> ();
	SpriteRenderer mySpriteRenderer;
	SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {
		cam = camera.GetComponent<Camera> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		playerSprite = player.GetComponent<SpriteRenderer> ();

		InitializeDict ();
	}
	
	// Update is called once per frame
	void Update () {
		ChangeBGColor ();
	}

	void InitializeDict(){
		colorDict.Add (color0, match0);
		colorDict.Add (color1, match1);
		colorDict.Add (color2, match2);
		colorDict.Add (color3, match3);
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
}
