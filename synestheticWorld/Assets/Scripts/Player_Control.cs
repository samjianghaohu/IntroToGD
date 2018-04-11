using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Control : MonoBehaviour {//This script defines player control and some releveant behaviors

	//Declare relevent attributes and objects
	public float xSpeed;
	public float jumpHeight;
	public float shootDelay;
	public int maxBulletNum;
	public int nextStage;


	public GameObject bulletPrefab;
	public GameObject portal;
	public GameObject stage;
	public LayerMask myLayerMask;


	//Relevant parameters
	int bulletNum = 0;
	int incomingDirect = 0;

	float sDelay = 0f;
	float timeOfStunned = 0f;

	bool ifStunned = false;
	bool ifWin = false;


	Color prevColor;
	Rigidbody2D myRigidbody;
	SpriteRenderer mySpriteRenderer;
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

		if (ifWin) {//Move towards portal center if winning condition is met
			Destroy (myRigidbody);
			soundController.enabled = false;
			myAnimation.enabled = false;
			transform.position = Vector2.MoveTowards (transform.position, portal.transform.position, 2f * Time.deltaTime);

			if (transform.position == portal.transform.position) {
				SceneManager.LoadScene (nextStage);
			}


		} else if (ifStunned) {//Knock back and turn red when stunned
			if (incomingDirect == 1) {
				mySpriteRenderer.flipX = false;
				eyeSpriteRenderer.flipX = false;
				attackSpriteRenderer.flipX = false;
				transform.position += 0.04f * Vector3.left;
			} else {
				mySpriteRenderer.flipX = true;
				eyeSpriteRenderer.flipX = true;
				attackSpriteRenderer.flipX = true;
				transform.position += 0.04f * Vector3.right;
			}
			mySpriteRenderer.color = Color.red;
			timeOfStunned -= Time.deltaTime;

			if (timeOfStunned <= 0) {
				mySpriteRenderer.color = prevColor;
				ifStunned = false;
			}


		} else {//Normal condition
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			PlayerMove ();
			ShootProjectile ();
			SwitchForm ();
		}


		//Conditions for game over
		if (transform.position.y < (stage.GetComponent<Stage_Info>().getDownBorder() - 2) || myBehavior.getHealth () <= 0) {
			SceneManager.LoadScene ("stageFail");
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
				}
			}
		}
	}


	//Shoot bullets
	void ShootProjectile(){
		if (Input.GetKeyDown (KeyCode.Space)) {


			//Shoot bullets when it hasn't reached maximum bullet number on screen
			//and not during the mandatory delay between each shoot
			if (bulletNum < maxBulletNum && sDelay <= 0) {

				//Turn on attack animation
				myAnimation.MakeAttack ();


				//Instantiate bullets, position and flip depending on which direction player's shooting
				GameObject newSoundwaveObj = Instantiate (bulletPrefab);
				if (mySpriteRenderer.flipX == false) {
					newSoundwaveObj.transform.position = transform.position + Vector3.right;
				} else {
					newSoundwaveObj.GetComponent<SpriteRenderer> ().flipX = true;
					newSoundwaveObj.transform.position = transform.position + Vector3.left;
				}


				//Play attack audio
				soundController.PlayAttackSound ();

				bulletNum += 1;
				sDelay = shootDelay;
			}


		} else {//Turn off attack animation when not shooting
			myAnimation.StopAttack ();
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


				} else {//Switch back to original if currently using it
					mySpriteRenderer.color = new Color (1, 1, 1);
					attackSpriteRenderer.color = new Color (1, 1, 1);
					bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
				}
			}
		}


		if (Input.GetKeyDown (KeyCode.X)) {
			if (abilityNum >= 2) {//Only able to use the second ability when ability number is greater than 2
				if (!Stage_Utilities.compareColorsLoose (mySpriteRenderer.color, abilityList [1].abilityColor)) {
					mySpriteRenderer.color = abilityList [1].abilityColor;
					attackSpriteRenderer.color = abilityList [1].abilityColor;
					bulletPrefab.GetComponent<SpriteRenderer> ().color = abilityList [1].abilityColor;
				} else {
					mySpriteRenderer.color = new Color (1, 1, 1);
					attackSpriteRenderer.color = new Color (1, 1, 1);
					bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
				}
			}
		}


		if (Input.GetKeyDown (KeyCode.C)) {
			if (abilityNum >= 3) {//Only able to use the third ability when ability number is greater than 3
				if (!Stage_Utilities.compareColorsLoose (mySpriteRenderer.color, abilityList [2].abilityColor)) {
					mySpriteRenderer.color = abilityList [2].abilityColor;
					attackSpriteRenderer.color = abilityList [2].abilityColor;
					bulletPrefab.GetComponent<SpriteRenderer> ().color = abilityList [2].abilityColor;
				} else {
					mySpriteRenderer.color = new Color (1, 1, 1);
					attackSpriteRenderer.color = new Color (1, 1, 1);
					bulletPrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
				}
			}
		}

	}


	public void Stune(int damgeDirect){
		prevColor = mySpriteRenderer.color;
		timeOfStunned = 0.2f;
		ifStunned = true;
		incomingDirect = damgeDirect;
	}


	public bool IfStunned(){
		return ifStunned;
	}


	public void decreaseBulletNum(){
		bulletNum -= 1;
	}


	public void Win(){
		ifWin = true;
	}

}
