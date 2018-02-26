using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour {
	public float projectileSpeed;

	Vector2 originalPos;
	SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
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
		if (Mathf.Abs(transform.position.x - originalPos.x) >= 18) {
			if (this.tag == "PlayerProjectile") {
				Player_Control.decreaseBulletNum ();
			}

			Destroy (this.gameObject);
		}
	}
}
