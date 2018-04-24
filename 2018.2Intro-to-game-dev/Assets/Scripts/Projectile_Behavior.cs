using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour {//This scripts defines bullet behaviors

	//Basic attributes of a bullet
	public float projectileSpeed;
	public int projectilePower;

	public GameObject bulletGhostPrefab;
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
		CreateGhosts ();
		ProjectileDisappear ();
	}


	//Define which direction the bullet flies based on if the sprite is flipped
	void ProjectileFly(){
		if (mySpriteRenderer.flipX == false) {
			transform.position += projectileSpeed * Vector3.right;
		} else {
			transform.position += projectileSpeed * Vector3.left;
		}
	}


	void CreateGhosts(){
		GameObject ghostBaby = Instantiate (bulletGhostPrefab);
		Vector3 ghostPos = this.transform.position;
		Sprite ghostSprite = this.GetComponent<SpriteRenderer> ().sprite;
		Color ghostColor = this.GetComponent<SpriteRenderer> ().color;

		ghostBaby.transform.position = ghostPos;
		ghostBaby.transform.localScale = transform.localScale;
		ghostBaby.GetComponent<SpriteRenderer> ().sprite = ghostSprite;
		ghostBaby.GetComponent<SpriteRenderer> ().color = new Color(ghostColor.r, ghostColor.g, ghostColor.b, 0.15f);
		ghostBaby.GetComponent<SpriteRenderer> ().flipX = mySpriteRenderer.flipX;
	}


	//Bullet disappears when flying out of camera
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
