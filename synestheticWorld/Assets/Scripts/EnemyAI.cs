using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	int PATROL = 0;
	int FIRE = 1;
	int state;
	float timeUntilTurnBack;

	public float howLongUntilTurnBack;
	public float speed;
	public GameObject player;

	SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		state = PATROL;
		timeUntilTurnBack = howLongUntilTurnBack;

		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckPlayer ();

		if (state == PATROL) {
			Debug.Log ("I'm patroling.");
			MoveAround ();
		}
		if (state == FIRE) {
			Debug.Log ("I see you!");
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
		transform.position += speed * Vector3.left;

		timeUntilTurnBack -= Time.deltaTime;
		if (timeUntilTurnBack < 0) {
			speed *= -1;
			timeUntilTurnBack = howLongUntilTurnBack;
		}

		if (speed < 0) {
			mySpriteRenderer.flipX = true;
		} else {
			mySpriteRenderer.flipX = false;
		}

	}

}
