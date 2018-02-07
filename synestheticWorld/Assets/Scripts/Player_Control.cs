using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	
	public float xSpeed = 1f;
	public float jumpHeight = 1f;
	public GameObject soundwavePrefab;
	public LayerMask myLayerMask;

	bool canJump = false;

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

		UpdateJumpState ();
		PlayerMove ();
		ShootProjectile ();
	}

	void UpdateJumpState(){
//		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, Mathf.Infinity, myLayerMask);

//		if (hit.collider.transform.parent.tag == "Floor") {
//		}

		if (myRigidbody.velocity.y == 0) {
			canJump = true;
		} else {
			canJump = false;
		}
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

}
