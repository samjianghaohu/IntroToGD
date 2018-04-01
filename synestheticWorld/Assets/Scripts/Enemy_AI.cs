using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {//This script moves enemies and controls its behaviors.

	//Time thresholds for different actions/states
	public float howLongUntilTurnBack;
	public float howLongUntilNextShoot;
	public float howLongUntilSleep;
	public float howLongUntilAwake;


	//Basic attributes of the enemy
	public float moveSpeed;
	public float myHealth;


	public GameObject player;
	public GameObject bulletPrefab;
	public LayerMask myLayerMask;
	public Color weakColor;

	public static GameObject weakPoint;


	//Current and pervious state trackers
	int PATROL = 0;
	int FIRE = 1;
	int STUNNED = 2;
	int state;
	int prevState;


	//Time counter for actions/states
	float timeUntilTurnBack;
	float timeUntilNextShoot;
	float timeUntilSleep;
	float timeUntilAwake;
	float timeOfStunned;


	SpriteRenderer mySpriteRenderer;
	SpriteRenderer eyeSpriteRenderer;
	Rigidbody2D myRigidbody;
	Color prevColor;

	[SerializeField] Enemy_SoundControl soundController;


	// Use this for initialization
	void Start () {

		//Enemy starts with patrolling
		state = PATROL;


		//Initialize time counters
		timeUntilTurnBack = howLongUntilTurnBack;
		timeUntilNextShoot = 0;
		timeUntilSleep = howLongUntilSleep;
		timeUntilAwake = 0;


		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		eyeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer> ();
		myRigidbody = GetComponent<Rigidbody2D> ();

	}


	// Update is called once per frame
	void FixedUpdate () {

		//What to do when it's sleeping
		if (timeUntilAwake > 0) {
			timeUntilAwake -= Time.deltaTime;
		}
		if (timeUntilAwake <= 0) {
			CheckPlayer ();
		}


		//What to do when it's patrolling
		if (state == PATROL) {
			timeUntilSleep = howLongUntilSleep;
			MoveAround ();
		}
		if (state == FIRE) {
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			timeUntilSleep -= Time.deltaTime;
			timeUntilNextShoot -= Time.deltaTime;

			if (timeUntilNextShoot <= 0) {
				ShootProjectile ();
				timeUntilNextShoot = howLongUntilNextShoot;
			}

			if (timeUntilSleep <= 0) {
				state = PATROL;
				timeUntilAwake = howLongUntilAwake;
			}
		}
		if (state == STUNNED) {
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			mySpriteRenderer.color = Color.red;
			timeOfStunned -= Time.deltaTime;

			if (timeOfStunned <= 0) {
				mySpriteRenderer.color = prevColor;
				state = prevState;
			}

		}

	}

	void CheckPlayer(){
		if ((transform.position.x >= player.transform.position.x && mySpriteRenderer.flipX == true) || (transform.position.x < player.transform.position.x && mySpriteRenderer.flipX == false)) {
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
		myRigidbody.velocity = new Vector2 (-moveSpeed, myRigidbody.velocity.y);

		timeUntilTurnBack -= Time.deltaTime;
		if (timeUntilTurnBack < 0) {
			moveSpeed *= -1;
			timeUntilTurnBack = howLongUntilTurnBack;
		}

		if (moveSpeed < 0) {
			mySpriteRenderer.flipX = false;
			eyeSpriteRenderer.flipX = false;
		} else {
			mySpriteRenderer.flipX = true;
			eyeSpriteRenderer.flipX = true;
		}

	}

	void ShootProjectile (){
		GameObject newBulletObj = Instantiate (bulletPrefab);

		if (mySpriteRenderer.flipX == false) {
			newBulletObj.transform.position = transform.position + Vector3.right;
		} else {
			newBulletObj.GetComponent<SpriteRenderer> ().flipX = true;
			newBulletObj.transform.position = transform.position + Vector3.left;
		}

		soundController.PlayAttackSound ();
	}

	void TakeDamage(float damage){
		myHealth -= damage;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerProjectile") {
			if (Stage_Utilities.compareColorsLoose (weakColor, Color.white) || Stage_Utilities.compareColorsLoose (other.GetComponent<SpriteRenderer> ().color, weakColor)) {
				if (state != STUNNED) {
					soundController.PlayResponseSound (1);
						
					int damage = other.GetComponent<Projectile_Behavior> ().GetPower ();
					TakeDamage (damage);

					if (myHealth <= 0) {
						Destroy (this.gameObject);
					} else {
						timeOfStunned = 0.2f;
						prevColor = mySpriteRenderer.color;
						prevState = state;
						state = STUNNED;
					}

				}

				Player_Control.decreaseBulletNum ();
				Destroy (other.gameObject);
			} else {
				soundController.PlayResponseSound (0);
			}
		}
	}

}
