using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Control : MonoBehaviour {
	
	public float xSpeed = 1f;
	public float jumpHeight = 1f;
	public float shootDelay = 1f;
	public int maxBulletNum = 1;
	public int nextStage;
	public GameObject bulletPrefab;
	public GameObject portal;
	public LayerMask myLayerMask;

	public static bool ifWin = false;
	public AudioClip attack;

	static int bulletNum = 0;
	static int incomingDirect = 0;
	static bool ifStunned = false;
	static float timeOfStunned;
	static Color prevColor;
	static SpriteRenderer mySpriteRenderer;

	int currentAbility = -1;
	float sDelay = 0;
	GameObject projectilePrefab;
	Rigidbody2D myRigidbody;
	SpriteRenderer eyeSpriteRenderer;
	AudioSource attackPlayer;

	[SerializeField] Player_SoundControl soundController;

	// Use this for initialization
	void Start () {
		projectilePrefab = bulletPrefab;
		projectilePrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);

		myRigidbody = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		eyeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer> ();

		attackPlayer = transform.GetChild(2).GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (ifWin == true) {
			Destroy (myRigidbody);
			transform.position = Vector2.MoveTowards (transform.position, portal.transform.position, 2f * Time.deltaTime);

			if (transform.position == portal.transform.position) {
				ResetGameParas ();
				SceneManager.LoadScene (nextStage);
			}
		} else if (ifStunned == true) {
			if (incomingDirect == 1) {
				mySpriteRenderer.flipX = false;
				eyeSpriteRenderer.flipX = false;
				transform.position += 0.04f * Vector3.left;
			} else {
				mySpriteRenderer.flipX = true;
				eyeSpriteRenderer.flipX = true;
				transform.position += 0.04f * Vector3.right;
			}
			mySpriteRenderer.color = Color.red;
			timeOfStunned -= Time.deltaTime;

			if (timeOfStunned <= 0) {
				mySpriteRenderer.color = prevColor;
				ifStunned = false;
			}
		} else {
			myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

			PlayerMove ();
			ShootProjectile ();
			SwitchForm ();
		}


		if (transform.position.y < (Stage_Info.getDownBorder() - 2) || Player_Behavior.getHealth () <= 0) {
			ResetGameParas ();
			SceneManager.LoadScene ("stageFail");
		}

	}

	void PlayerMove(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, Mathf.Infinity, myLayerMask);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = true;
			eyeSpriteRenderer.flipX = true;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = false;
			eyeSpriteRenderer.flipX = false;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (hit != null && hit.collider != null && hit.collider.transform.parent.tag == "Floor") {
				if (hit.distance <= 0.6f) {
					myRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
					soundController.PlayJumpSound ();
				}
			}
		}
	}

	void ShootProjectile(){
		if (Input.GetKeyDown (KeyCode.X)) {
			if (bulletNum < maxBulletNum && sDelay <= 0) {
				GameObject newSoundwaveObj = Instantiate (projectilePrefab);
				if (mySpriteRenderer.flipX == false) {
					newSoundwaveObj.transform.position = transform.position + Vector3.right;
				} else {
					newSoundwaveObj.GetComponent<SpriteRenderer> ().flipX = true;
					newSoundwaveObj.transform.position = transform.position + Vector3.left;
				}

				attackPlayer.clip = attack;
				attackPlayer.loop = false;
				attackPlayer.Play ();

				bulletNum += 1;
				sDelay = shootDelay;
			}
		}

		sDelay -= Time.deltaTime;

	}

	void SwitchForm(){
		int abilityNum = Player_AbilityStack.GetAvailableAbilityNum ();	
		List<Player_Ability> abilityList = Player_AbilityStack.GetAbilityList ();

		if (Input.GetKeyDown (KeyCode.Z)) {
			mySpriteRenderer.color = new Color (1, 1, 1);
			projectilePrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			if (abilityNum >= 1) {
				mySpriteRenderer.color = abilityList [0].abilityColor;
				projectilePrefab.GetComponent<SpriteRenderer> ().color = abilityList [0].abilityColor;
			}
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			if (abilityNum >= 2) {
				mySpriteRenderer.color = abilityList [1].abilityColor;
				projectilePrefab.GetComponent<SpriteRenderer> ().color = abilityList [1].abilityColor;
			}
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			if (abilityNum >= 3) {
				mySpriteRenderer.color = abilityList [2].abilityColor;
				projectilePrefab.GetComponent<SpriteRenderer> ().color = abilityList [2].abilityColor;
			}
		}
//		if (currentAbility < 0) {
//			mySpriteRenderer.color = new Color (1, 1, 1);
//			projectilePrefab = soundwavePrefab;
//		} else {
//			mySpriteRenderer.color = abilityList [currentAbility].abilityColor;
//			projectilePrefab = abilityList [currentAbility].projectilePrefab;
//		}
//			
//		if (Input.GetKeyDown (KeyCode.A)) {
//			if (currentAbility > -1) {
//				currentAbility -= 1;
//			}
//		}
//		if (Input.GetKeyDown (KeyCode.D)) {
//			if (currentAbility < (abilityNum - 1)) {
//				currentAbility += 1;
//			}
//		}
	}

	void ResetGameParas(){
		Player_AbilityStack.resetAbilities ();
		Player_Behavior.resetHealth ();
		ifWin = false;
		mySpriteRenderer.color = new Color (1, 1, 1);
		projectilePrefab.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
	}



	public static void Stune(int damgeDirect){
		prevColor = mySpriteRenderer.color;
		timeOfStunned = 0.2f;
		ifStunned = true;
		incomingDirect = damgeDirect;
	}

	public static bool IfStunned(){
		return ifStunned;
	}

	public static void decreaseBulletNum(){
		bulletNum -= 1;
	}

}
