using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	
	public float xSpeed = 1f;
	public float jumpHeight = 1f;
	public GameObject soundwavePrefab;

	bool canJump = false;
	static float playerHealth = 20;

	SpriteRenderer mySpriteRenderer;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);
		Debug.Log (playerHealth);
		PlayerMove ();
		ShootProjectile ();
	}

	void PlayerMove(){
		if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = true;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = false;
		}
		if (Input.GetKey(KeyCode.C)) {
			if (canJump == true) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpHeight);
				//myRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
				canJump = false;
			}
		}

		if (myRigidbody.velocity.y == 0) {
			canJump = true;
		} else {
			canJump = false;
		}
	}

	void ShootProjectile(){
		if (Input.GetKeyDown (KeyCode.X)) {
			GameObject newSoundwaveObj = Instantiate (soundwavePrefab);
			if (mySpriteRenderer.flipX == false) {
				newSoundwaveObj.transform.position = transform.position + Vector3.right;
			} else {
				newSoundwaveObj.GetComponent<SpriteRenderer> ().flipX = true;
				newSoundwaveObj.transform.position = transform.position + Vector3.left;
			}
		}
	}

	public static void takeDamage(float damage){
		playerHealth -= damage;

		if (playerHealth <= 0) {
			Debug.Log ("You Died!");
		}
	}

}
