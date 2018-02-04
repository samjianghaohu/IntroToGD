using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	int PATROL = 0;
	int FIRE = 1;
	int state;
	float timeUntilTurnBack;
	float timeUntilNextShoot;

	public float howLongUntilTurnBack;
	public float howLongUntilNextShoot;
	public float moveSpeed;
	public GameObject player;
	public GameObject noisewavePrefab;

	SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		state = PATROL;

		timeUntilTurnBack = howLongUntilTurnBack;
		timeUntilNextShoot = 0;

		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
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

	}

	void CheckPlayer(){
		if ((transform.position.x >= player.transform.position.x && mySpriteRenderer.flipX == false) || (transform.position.x < player.transform.position.x && mySpriteRenderer.flipX == true)) {
			RaycastCheck ();
		} else {
			if (state != PATROL) {
				state = PATROL;
			}
		}

	}

	void RaycastCheck(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, (player.transform.position - transform.position), Mathf.Infinity);

		if (hit.collider.gameObject.tag == "Player") {
			if (hit.distance <= 7.2f) {
				if (state != FIRE) {
					state = FIRE;
				}
			} else {
				if (state != PATROL) {
					state = PATROL;
				}
			}
		} else {
			if (state != PATROL) {
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

}
