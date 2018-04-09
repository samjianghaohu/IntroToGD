using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Behavior : MonoBehaviour {//This script controls crystal behavior

	bool ifAbsorbing = false;
	float alpha = 1f;

	GameObject player;
	SpriteRenderer mySpriteRender;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		mySpriteRender = GetComponent<SpriteRenderer> ();
	}


	// Update is called once per frame
	void Update () {
		mySpriteRender.color = new Color (mySpriteRender.color.r, mySpriteRender.color.g, mySpriteRender.color.b, alpha);

		if (ifAbsorbing) {
			FadeOut ();
		}
		if (!ifAbsorbing && alpha < 1f) {
			FadeIn ();
		}
		if ((alpha - 0) <= 0.01f) {
			player.GetComponent<Player_AbilityStack>().AddAbility (this.gameObject);
			Destroy (this.gameObject);
		}
				
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
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			ifAbsorbing = false;
		}
	}
}
