using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Text : MonoBehaviour {//This script controls tutorial texts fading in and out

	//If player is close to the text area
	bool ifIn = false;


	Color textColor;
	TextMesh myTextMesh;


	// Use this for initialization
	void Start () {
		myTextMesh = GetComponent<TextMesh> ();
	}


	// Update is called once per frame
	void FixedUpdate () {
		
		//text color is always the same as player color
		textColor = GameObject.FindWithTag ("Player").GetComponent<SpriteRenderer> ().color;
		if (!Stage_Utilities.compareColorsLoose (textColor, Color.red)) {
			myTextMesh.color = new Color (textColor.r, textColor.g, textColor.b, myTextMesh.color.a);
		}


		//text fade in and out as player approaches or goes away
		if (ifIn) {
			FadeIn ();
		} else {
			FadeOut ();
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			ifIn = true;
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			ifIn = false;
		}
	}


	void FadeIn(){
		if (myTextMesh.color.a < 1f) {
			myTextMesh.color += new Color (0, 0, 0, 0.05f);
		}
	}


	void FadeOut(){
		if (myTextMesh.color.a > 0f) {
			myTextMesh.color -= new Color (0, 0, 0, 0.05f);
		}
	}
}
