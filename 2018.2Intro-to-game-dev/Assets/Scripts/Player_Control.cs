﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Security.AccessControl;

public class Player_Control : MonoBehaviour {//This script defines player control and some releveant behaviors

	//Declare relevent attributes and objects
	public float xSpeed;
	public float jumpHeight;
	public float shootDelay;
	public float dyingTime;
	public float timeOfStunned;
	public int maxBulletNum;


	public GameObject bulletPrefab;
	public GameObject portal;
	public GameObject stage;
	public LayerMask myLayerMask;

	public GameObject dustPuff;
	private ParticleSystem dustParticle;


	//time counters
	float sDelay = 0f;
	float stunTimer = 0f;
	float dyingTimer = 1f;

	bool ifStunned = false;
	bool ifWin = false;
	bool ifDead = false;


	Color prevColor;
	Rigidbody2D myRigidbody;
	SpriteRenderer mySpriteRenderer; //DASHA ∆ to public 
	SpriteRenderer eyeSpriteRenderer;
	SpriteRenderer attackSpriteRenderer; 

	Player_SoundControl soundController;
	Player_Behavior myBehavior;
	Player_Animation myAnimation;
	Player_AbilityStack myAStack;


	// Use this for initialization
	void Start () {
		
		//Initialize components
		myRigidbody = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		eyeSpriteRenderer = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		attackSpriteRenderer = transform.GetChild (3).GetComponent<SpriteRenderer> ();

		soundController = GetComponent<Player_SoundControl> ();
		myBehavior = GetComponent<Player_Behavior> ();
		myAnimation = GetComponent<Player_Animation> ();
		myAStack = GetComponent<Player_AbilityStack> ();


		//Initialize player and bullet sprites
		InitializeSprites ();
	}


	// Update is called once per frame
	void Update () {

		//Move towards portal center if winning condition is met
		if (ifWin) {
			Destroy (myRigidbody);
			soundController.enabled = false;
			myAnimation.enabled = false;
			transform.position = Vector2.MoveTowards (transform.position, portal.transform.position, 2f * Time.deltaTime);

			if (transform.position == portal.transform.position) {
				int nextStage = stage.GetComponent<Stage_Info> ().GetNextStage ();
				SceneManager.LoadScene (nextStage);
			}


		} else if (ifStunned) {
			
			//Trigger stun animation when stunned
			myAnimation.MakeStun ();

			stunTimer -= Time.deltaTime;


			//Color goes back to normal after half of the stun time, otherwise being red
			if (stunTimer <= (timeOfStunned / 2f)) {
				mySpriteRenderer.color = prevColor;
			} else {
				mySpriteRenderer.color = Color.red;
			}


			//Stop stun animation and quit stun state when timer's zero
			if (stunTimer <= 0) {
				myAnimation.StopStun ();
				ifStunned = false;
			}


		} else if (!ifDead) {//Normal condition
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			PlayerMove ();
			ShootProjectile ();
			SwitchForm ();
		}


		//Conditions for game over
		if (transform.position.y < (stage.GetComponent<Stage_Info>().GetDownBorder() - 2) || myBehavior.getHealth () <= 0) {
			ifDead = true;


			//Record failing state and the current stage to come back to
			Menu_Clear.ifFailed = true;
			Menu_Clear.failedScene = stage.GetComponent<Stage_Info> ().nextStage - 1;


			//Player turns black when dying
			Color dyingColor = Color.Lerp(Color.black, mySpriteRenderer.color, dyingTimer);
			mySpriteRenderer.color = dyingColor;
			eyeSpriteRenderer.color = dyingColor;
			dyingTimer -= Time.deltaTime / dyingTime;

			if (dyingTimer <= 0) {
				SceneManager.LoadScene ("stageFail");
			}
		}

	}


	public void InitializeSprites (){
		mySpriteRenderer.flipX = false;
		mySpriteRenderer.color = new Color (1, 1, 1);

		eyeSpriteRenderer.flipX = false;
		eyeSpriteRenderer.color = new Color (1, 1, 1);

		attackSpriteRenderer.flipX = false;
		attackSpriteRenderer.color = new Color (1, 1, 1);

		bulletPrefab.GetComponent<SpriteRenderer> ().flipX = false;
		bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
	}


	//Using left arrow, right arrow, up arrow for moving and jumping
	void PlayerMove(){

		//Raycast towards ground
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, Mathf.Infinity, myLayerMask);


		//Move left and right
		if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = true;
			eyeSpriteRenderer.flipX = true;
			attackSpriteRenderer.flipX = true;

		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = false;
			eyeSpriteRenderer.flipX = false;
			attackSpriteRenderer.flipX = false;

		}



		//Jump, only doable when player's on the ground)
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (hit != null && hit.collider != null && hit.collider.transform.parent.tag == "Floor") {
				if (hit.distance <= 0.6f) {
					myRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
					soundController.PlayJumpSound ();


					//Instantiate dust Particles
					GameObject dustObject = Instantiate(dustPuff, (this.transform.position + 0.4f * Vector3.down), Quaternion.Euler(new Vector3(-90, 0, 0))) as GameObject;
					ParticleSystem dust = dustObject.GetComponent<ParticleSystem> ();
					dust.startColor = mySpriteRenderer.color;
					dust.Play ();

					Destroy (dustObject, 1f);

				}
			}
		}
	}


	//Shoot bullets
	void ShootProjectile(){
		GameObject[] bulletArray = GameObject.FindGameObjectsWithTag ("PlayerProjectile");
		int bulletNum = bulletArray.Length;

		if (Input.GetKeyDown (KeyCode.Space)) {


			//Shoot bullets when it hasn't reached maximum bullet number on screen
			//and not during the mandatory delay between each shoot
			if (bulletNum < maxBulletNum && sDelay <= 0) {

				//Trigger attack animation
				myAnimation.MakeAttack ();

				float yPosOffset = Random.Range (-0.2f, 0.2f);

				//Instantiate bullets, position and flip depending on which direction player's shooting
				GameObject newSoundwaveObj = Instantiate (bulletPrefab);
				if (mySpriteRenderer.flipX == false) {
					newSoundwaveObj.transform.position = transform.position + Vector3.right + yPosOffset * Vector3.up;
				} else {
					newSoundwaveObj.GetComponent<SpriteRenderer> ().flipX = true;
					newSoundwaveObj.transform.position = transform.position + Vector3.left + yPosOffset * Vector3.up;
				}


				//Play attack audio
				soundController.PlayAttackSound ();

				bulletNum += 1;
				sDelay = shootDelay;
			}


		}

		sDelay -= Time.deltaTime;

	}


	//Swtich abilities
	void SwitchForm(){

		//Get current ability list and ability number
		int abilityNum = myAStack.GetAvailableAbilityNum ();	
		List<Player_Ability> abilityList = myAStack.GetAbilityList ();



		if (Input.GetKeyDown (KeyCode.Z)) {
			if (abilityNum >= 1) {//Only able to use the first ability when ability number is greater than 1

				//Switch to the ability if not currently using it
				if (!Stage_Utilities.compareColorsLoose (mySpriteRenderer.color, abilityList [0].abilityColor)) {
					mySpriteRenderer.color = abilityList [0].abilityColor;
					attackSpriteRenderer.color = abilityList [0].abilityColor;
					bulletPrefab.GetComponent<SpriteRenderer> ().color = abilityList [0].abilityColor;
					dustPuff.GetComponent<ParticleSystem> ().startColor = abilityList [0].abilityColor;


				} else {//Switch back to original if currently using it
					mySpriteRenderer.color = new Color (1, 1, 1);
					attackSpriteRenderer.color = new Color (1, 1, 1);
					bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
					dustPuff.GetComponent<ParticleSystem> ().startColor = new Color (1, 1, 1);
				}
			}
		}


		if (Input.GetKeyDown (KeyCode.X)) {
			if (abilityNum >= 2) {//Only able to use the second ability when ability number is greater than 2
				if (!Stage_Utilities.compareColorsLoose (mySpriteRenderer.color, abilityList [1].abilityColor)) {
					mySpriteRenderer.color = abilityList [1].abilityColor;
					attackSpriteRenderer.color = abilityList [1].abilityColor;
					bulletPrefab.GetComponent<SpriteRenderer> ().color = abilityList [1].abilityColor;
					dustPuff.GetComponent<ParticleSystem> ().startColor = abilityList [1].abilityColor;
				} else {
					mySpriteRenderer.color = new Color (1, 1, 1);
					attackSpriteRenderer.color = new Color (1, 1, 1);
					bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
					dustPuff.GetComponent<ParticleSystem> ().startColor = new Color (1, 1, 1);
				}
			}
		}


		if (Input.GetKeyDown (KeyCode.C)) {
			if (abilityNum >= 3) {//Only able to use the third ability when ability number is greater than 3
				if (!Stage_Utilities.compareColorsLoose (mySpriteRenderer.color, abilityList [2].abilityColor)) {
					mySpriteRenderer.color = abilityList [2].abilityColor;
					attackSpriteRenderer.color = abilityList [2].abilityColor;
					bulletPrefab.GetComponent<SpriteRenderer> ().color = abilityList [2].abilityColor;
					dustPuff.GetComponent<ParticleSystem> ().startColor = abilityList [2].abilityColor;
				} else {
					mySpriteRenderer.color = new Color (1, 1, 1);
					attackSpriteRenderer.color = new Color (1, 1, 1);
					bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
					dustPuff.GetComponent<ParticleSystem> ().startColor = new Color (1, 1, 1);
				}
			}
		}

	}


	public void Stune(int damageDirect){

		//Store the color player will return back to after stun
		prevColor = mySpriteRenderer.color;


		//Knock player back depending on bullet direction
		if (damageDirect == 1) {
			mySpriteRenderer.flipX = false;
			eyeSpriteRenderer.flipX = false;
			attackSpriteRenderer.flipX = false;
			myRigidbody.AddForce(Vector2.left * 0.8f, ForceMode2D.Impulse);
		} else {
			mySpriteRenderer.flipX = true;
			eyeSpriteRenderer.flipX = true;
			attackSpriteRenderer.flipX = true;
			myRigidbody.AddForce(Vector2.right * 0.8f, ForceMode2D.Impulse);		
		}


		//Set stun timer and set stun state
		stunTimer = timeOfStunned;
		ifStunned = true;
	}


	public bool IfStunned(){
		return ifStunned;
	}


	public bool IsDead(){
		return ifDead;
	}


	public void Win(){
		ifWin = true;
	}

}
