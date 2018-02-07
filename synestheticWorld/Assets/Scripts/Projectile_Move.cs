using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Move : MonoBehaviour {
	public float projectileSpeed;
	public GameObject camera;

	SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
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
//		if (transform.position.x > camera.transform.position.x + 10 || transform.position.x < camera.transform.position.x - 10 || transform.position.y > camera.transform.position.y + 6 || transform.position.y < camera.transform.position.y - 6) {
//			Destroy (this.gameObject);
//		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.gameObject);
		if (this.tag == "PlayerProjectile" && other.gameObject.tag == "Enemy") {
			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}
		if (this.tag == "EnemyProjectile" && other.gameObject.tag == "Player") {
			Player_Control.takeDamage (2);
			Destroy (this.gameObject);
		}
	}
}
