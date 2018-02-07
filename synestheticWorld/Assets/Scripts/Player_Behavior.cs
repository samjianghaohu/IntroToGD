using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour {
	static float playerHealth = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void takeDamage(float damage){
		playerHealth -= damage;

		if (playerHealth <= 0) {
			Debug.Log ("You Died!");
		}
	}

	public static float getHealth(){
		return playerHealth;
	}
}
