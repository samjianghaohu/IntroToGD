using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {
	public float howLongUntilTurnBack;
	public float howLongUntilNextShoot;
	public float moveSpeed;
	public GameObject player;
	public GameObject noisewavePrefab;
	public LayerMask myLayerMask;
	public Sprite weekPoint;

	public static GameObject weakPoint;

	int PATROL = 0;
	int FIRE = 1;
	int STUNNED = 2;
	int state;
	int prevState;

	float timeUntilTurnBack;
	float timeUntilNextShoot;
	float timeOfStunned;
	float myHealth = 3;

	SpriteRenderer mySpriteRenderer;
	Color prevColor;

	// Use this for initialization
	void Start () {
		state = PATROL;

		timeUntilTurnBack = howLongUntilTurnBack;
		timeUntilNextShoot = 0;

		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckPlayer ();

		if (state == PATROL) {
			MoveAround ();
		}
		if (state == FIRE) {
			timeUntilNextShoot -= Time.deltaTime;
			if (timeUntilNextShoot <= 0) {
				ShootProjectile ();
				timeUntilNextShoot = howLongUntilNextShoot;
			}
		}
		if (state == STUNNED) {
			mySpriteRenderer.color = Color.white;
			timeOfStunned -= Time.deltaTime;

			if (timeOfStunned <= 0) {
				mySpriteRenderer.color = prevColor;
				state = prevState;
			}

		}

	}

	void CheckPlayer(){
		if ((transform.position.x >= player.transform.position.x && mySpriteRenderer.flipX == false) || (transform.position.x < player.transform.position.x && mySpriteRenderer.flipX == true)) {
			RaycastCheck ();
		} else {
			if (state == FIRE) {
				state = PATROL;
			}
		}

	}

	void RaycastCheck(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, (player.transform.position - transform.position), Mathf.Infinity, myLayerMask);
		if (hit != null && hit.collider != null && hit.collider.gameObject.tag == "Player") {
			if (hit.distance <= 7.2f) {
				if (state == PATROL) {
					state = FIRE;
				}
			} else {
				if (state == FIRE) {
					state = PATROL;
				}
			}
		} else {
			if (state == FIRE) {
				state = PATROL;
			}
		}
	}

	void MoveAround (){
		transform.position += moveSpeed * Vector3.left;

		timeUntilTurnBack -= Time.deltaTime;
		if (timeUntilTurnBack < 0) {
			moveSpeed *= -1;
			timeUntilTurnBack = howLongUntilTurnBack;
		}

		if (moveSpeed < 0) {
			mySpriteRenderer.flipX = true;
		} else {
			mySpriteRenderer.flipX = false;
		}

	}

	void ShootProjectile (){
		GameObject newNoisewaveObj = Instantiate (noisewavePrefab);
		if (mySpriteRenderer.flipX == true) {
			newNoisewaveObj.transform.position = transform.position + Vector3.right;
		} else {
			newNoisewaveObj.GetComponent<SpriteRenderer> ().flipX = true;
			newNoisewaveObj.transform.position = transform.position + Vector3.left;
		}
	}

	void TakeDamage(float damage){
		myHealth -= damage;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerProjectile") {
			if (other.GetComponent<SpriteRenderer> ().sprite == weekPoint) {
				if (state != STUNNED) {
					
					TakeDamage (1);
					if (myHealth <= 0) {
						Destroy (this.gameObject);
					} else {
						timeOfStunned = 0.2f;
						prevColor = mySpriteRenderer.color;
						prevState = state;
						state = STUNNED;
					}

				}
				Destroy (other.gameObject);
			}
		}
	}

}
