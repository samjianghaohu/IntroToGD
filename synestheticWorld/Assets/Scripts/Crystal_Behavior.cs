using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Behavior : MonoBehaviour {//This script controls crystal behavior

	public GameObject absorbPrefab;

	bool ifAbsorbing = false;
	float alpha = 1f;


	GameObject absorbObject;
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


		//The crystal fades out and triggers absorbing effects when player touches it
		if (ifAbsorbing) {
			FadeOut ();

	
			//Update the position of absorb effect with player's position
			absorbObject.transform.position = player.transform.position;
		}


		//If player leaves the crystal before it's completely absorbed, the crystal fades back in
		if (!ifAbsorbing && alpha < 1f) {
			FadeIn ();
		}


		//Destroy the crystal when it's completely absorbed by player
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


			//Instantiate absorb particle effects if player just starts to touch the crystal
			if (!ifAbsorbing) {
				absorbObject = Instantiate(absorbPrefab, player.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0))) as GameObject;
				ParticleSystem absorb = absorbObject.GetComponent<ParticleSystem> ();
				absorb.startColor = mySpriteRender.color;
				absorb.Play ();
			}

			//Update states
			ifAbsorbing = true;
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			Destroy (absorbObject);
			ifAbsorbing = false;
		}
	}
}
