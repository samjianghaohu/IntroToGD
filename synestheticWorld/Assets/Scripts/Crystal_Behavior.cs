﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Behavior : MonoBehaviour {
	bool ifAbsorbing = false;
	float alpha = 1f;
	SpriteRenderer mySpriteRender;

	// Use this for initialization
	void Start () {
		mySpriteRender = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		mySpriteRender.color = new Color (mySpriteRender.color.r, mySpriteRender.color.g, mySpriteRender.color.b, alpha);

		if (ifAbsorbing == false && alpha < 1f) {
			FadeIn ();
		}

		if ((alpha - 0) <= 0.01f) {
			Player_AbilityStack.AddAbility (this.gameObject);
			Destroy (this.gameObject);
		}

		Debug.Log (ifAbsorbing);
	}

	void FadeOut(){
		alpha -= 0.015f;
	}

	void FadeIn(){
		alpha += 0.015f;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			ifAbsorbing = true;
			FadeOut ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			ifAbsorbing = false;
		}
	}
}
