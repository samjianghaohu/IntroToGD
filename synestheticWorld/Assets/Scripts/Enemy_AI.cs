﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {//This script defines enemy behaviors.

	//Time thresholds for different actions/states
	public float howLongUntilTurnBack;
	public float howLongUntilNextShoot;
	public float howLongUntilSleep;
	public float howLongUntilAwake;
	public float howLongToStayWarning;
	public float dyingTimer;


	//Basic attributes of the enemy
	public float moveSpeed;
	public float myHealth;


	public GameObject bulletPrefab;
	public GameObject explodePrefab;
	public LayerMask myLayerMask;
	public Color weakColor;


	//Current and pervious state trackers
	int PATROL = 0;
	int WARNING = 1;
	int FIRE = 2;
	int STUNNED = 3;
	int state;
	int prevState;


	//Time counter for actions/states
	float timeUntilTurnBack;
	float timeUntilNextShoot;
	float timeUntilSleep;
	float timeUntilAwake;
	float timeOfStunned;
	float timeOfWarning;


	GameObject player;
	SpriteRenderer mySpriteRenderer;
	SpriteRenderer eyeSpriteRenderer;
	SpriteRenderer attackSpriteRenderer;
	Rigidbody2D myRigidbody;
	Color prevColor;


	//Accessing other components on enemy
	[SerializeField] Enemy_SoundControl soundController;
	[SerializeField] Enemy_Animation animController;


	// Use this for initialization
	void Start () {

		//Enemy starts with patrolling
		state = PATROL;


		//Initialize time counters
		timeUntilTurnBack = howLongUntilTurnBack;
		timeUntilNextShoot = 0;
		timeUntilSleep = howLongUntilSleep;
		timeUntilAwake = 0;
		timeOfWarning = howLongToStayWarning;


		//Initialize components
		player = GameObject.FindWithTag("Player");

		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		eyeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer> ();
		attackSpriteRenderer = transform.GetChild (2).GetComponent<SpriteRenderer> ();
		attackSpriteRenderer.color = weakColor;

		myRigidbody = GetComponent<Rigidbody2D> ();

	}


	// Update is called once per frame
	void FixedUpdate () {

		//Wait until waking up when it's sleeping
		if (timeUntilAwake > 0) {
			timeUntilAwake -= Time.deltaTime;
		}
		if (timeUntilAwake <= 0) {//Only able to see player when it's awake
			CheckPlayer ();
		}
			

		//What to do when it's patrolling
		if (state == PATROL) {
			timeUntilSleep = howLongUntilSleep;
			MoveAround ();
		}


		//What to do when it's warning the player
		if (state == WARNING) {
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			animController.MakeWarn ();

			timeOfWarning -= Time.deltaTime;

			//Exit warning and go to fire
			if (timeOfWarning <= 0) {
				animController.StopWarn ();
				state = FIRE;
				timeOfWarning = howLongToStayWarning;
			}
		}
		if (state != WARNING) {//Turn off warning animation and reset warning time when it's not warning
			animController.StopWarn ();
			timeOfWarning = howLongToStayWarning;
		}


		//What to do when it's supposed to shoot
		if (state == FIRE) {
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			timeUntilSleep -= Time.deltaTime;
			timeUntilNextShoot -= Time.deltaTime;

			if (timeUntilNextShoot <= 0) {
				ShootProjectile ();
				timeUntilNextShoot = howLongUntilNextShoot;
			}

			if (timeUntilSleep <= 0) {//If it stays in shooting for too long, go back to patrolling and sleep
				state = PATROL;
				timeUntilAwake = howLongUntilAwake;
			}
		}
		if (state != FIRE) {//Reset shooting delay when it quits shooting
			timeUntilNextShoot = 0;
		}


		//What to do when it's stunned
		if (state == STUNNED) {
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			mySpriteRenderer.color = Color.red;
			timeOfStunned -= Time.deltaTime;

			if (timeOfStunned <= 0) {//Go back to previous state and normal color and reset stun time when it quits stun
				mySpriteRenderer.color = prevColor;
				timeOfStunned = 0.2f;
				state = prevState;
			}
		}


		//Update the direction of attack sprite
		attackSpriteRenderer.flipX = mySpriteRenderer.flipX;

	}


	//Check if player is in front or behind
	void CheckPlayer(){
		if ((transform.position.x >= player.transform.position.x && mySpriteRenderer.flipX) || (transform.position.x < player.transform.position.x && !mySpriteRenderer.flipX)) {
			RaycastCheck ();
		} else {
			if (state != PATROL) {
				state = PATROL;
			}
		}

	}


	//If player is in front, check if it's blocked by floors or close enough
	void RaycastCheck(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, (player.transform.position - transform.position), Mathf.Infinity, myLayerMask);

		if (hit != null && hit.collider != null && hit.collider.gameObject.tag == "Player") {
			if (hit.distance <= 7.2f) {
				if (state == PATROL) {
					state = WARNING;
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


	//Move around when patrolling
	void MoveAround (){
		myRigidbody.velocity = new Vector2 (-moveSpeed, myRigidbody.velocity.y);

		timeUntilTurnBack -= Time.deltaTime;
		if (timeUntilTurnBack < 0) {//Turn back
			moveSpeed *= -1;
			timeUntilTurnBack = howLongUntilTurnBack;
		}


		//Flip enemy sprite when moving in different directions
		if (moveSpeed < 0) {
			mySpriteRenderer.flipX = false;
			eyeSpriteRenderer.flipX = false;
		} else {
			mySpriteRenderer.flipX = true;
			eyeSpriteRenderer.flipX = true;
		}

	}


	//Shoot bullets when firing
	void ShootProjectile (){

		//Trigger attack animation
		animController.MakeAttack ();


		//Instantiate a bullet
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


	//Explode process
	void Explode(){

		//Enemy body disappear
		mySpriteRenderer.color = new Color (1, 1, 1, 0);


		//Instantiate explode effect
		GameObject explodeObject = Instantiate (explodePrefab, this.transform.position, Quaternion.Euler (new Vector3 (-90, 0, 0))) as GameObject;
		ParticleSystem explode = explodeObject.GetComponent<ParticleSystem> ();
		explode.startColor = weakColor;
		explode.Play ();


		//Destory effect and enemy itself
		Destroy (explodeObject, 1f);
		Destroy (this.gameObject);
	}


	//When shot by a bullet
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerProjectile") {

			//When hit by the right bullet
			if (Stage_Utilities.compareColorsLoose (weakColor, Color.white) || Stage_Utilities.compareColorsLoose (other.GetComponent<SpriteRenderer> ().color, weakColor)) {
				if (state != STUNNED) {
					soundController.PlayResponseSound (1);
						
					int damage = other.GetComponent<Projectile_Behavior> ().GetPower ();
					TakeDamage (damage);


					//Explode if health <= 0 after taking damage
					if (myHealth <= 0) {
						Explode ();
					
					//Stunned if not dead after taking damage
					} else {
						if (!Stage_Utilities.compareColorsLoose (mySpriteRenderer.color, Color.red)) {
							prevColor = mySpriteRenderer.color;
						}
						if (state != STUNNED) {
							prevState = state;
						}
						state = STUNNED;
					}

				}

				player.GetComponent<Player_Control>().decreaseBulletNum ();
				Destroy (other.gameObject);

			
			//Play nasty chord when hit by the wrong bullet
			} else {
				soundController.PlayResponseSound (0);
			}
		}
	}

}
