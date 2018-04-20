using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Ghost : MonoBehaviour { //This script defines "ghosts" of a projectile 

	//How long a "ghost" can exist
	float timer = 0.1f; 


	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	//Each "ghost" gets destoryed after "timer" amount of time 
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Destroy (this.gameObject);
		
		}
	}
}
