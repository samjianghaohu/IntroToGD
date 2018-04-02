﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour {
	public float projectileSpeed;
	public int projectilePower;

	GameObject player;
	GameObject camera;
	SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		camera = GameObject.FindWithTag ("MainCamera");
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ProjectileFly ();
		ProjectileDisappear ();
	}

	void ProjectileFly(){
		if (mySpriteRenderer.flipX == false) {
			transform.position += projectileSpeed * Vector3.right;
		} else {
			transform.position += projectileSpeed * Vector3.left;
		}
	}

	void ProjectileDisappear(){
		if ((camera != null) && (transform.position.x > (camera.transform.position.x + 9) || transform.position.x < (camera.transform.position.x - 9))) {
			if (this.tag == "PlayerProjectile") {
				player.GetComponent<Player_Control> ().decreaseBulletNum ();
			}

			Destroy (this.gameObject);
		}
	}

	public int GetPower(){
		return projectilePower;
	}
}
