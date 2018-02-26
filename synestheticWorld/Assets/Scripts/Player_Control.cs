using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Control : MonoBehaviour {
	
	public float xSpeed = 1f;
	public float jumpHeight = 1f;
	public float shootDelay = 1f;
	public int maxBulletNum = 1;
	public GameObject soundwavePrefab;
	public GameObject portal;
	public LayerMask myLayerMask;

	public static bool ifWin = false;

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

	// Use this for initialization
	void Start () {
		projectilePrefab = soundwavePrefab;
		myRigidbody = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ifWin == true) {
			Destroy (myRigidbody);
			transform.position = Vector2.MoveTowards (transform.position, portal.transform.position, 2f * Time.deltaTime);
			if (transform.position == portal.transform.position) {
				SceneManager.LoadScene ("gameClear");
			}
		} else if (ifStunned == true) {
			if (incomingDirect == 1) {
				mySpriteRenderer.flipX = false;
				transform.position += 0.04f * Vector3.left;
			} else {
				mySpriteRenderer.flipX = true;
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
	}

	void PlayerMove(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, Mathf.Infinity, myLayerMask);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = true;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = false;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (hit != null && hit.collider != null && hit.collider.transform.parent.tag == "Floor") {
				if (hit.distance <= 0.6f) {
					myRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
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

				bulletNum += 1;
				sDelay = shootDelay;
			}
		}

		sDelay -= Time.deltaTime;

	}

	void SwitchForm(){
		int abilityNum = Player_AbilityStack.GetAvailableAbilityNum ();	
		List<Player_Ability> abilityList = Player_AbilityStack.GetAbilityList ();

		if (currentAbility < 0) {
			mySpriteRenderer.color = new Color (1, 1, 1);
			projectilePrefab = soundwavePrefab;
		} else {
			mySpriteRenderer.color = abilityList [currentAbility].abilityColor;
			projectilePrefab = abilityList [currentAbility].projectilePrefab;
		}
			
		if (Input.GetKeyDown (KeyCode.A)) {
			if (currentAbility > -1) {
				currentAbility -= 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			if (currentAbility < (abilityNum - 1)) {
				currentAbility += 1;
			}
		}
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
