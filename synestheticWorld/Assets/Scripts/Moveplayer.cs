using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveplayer : MonoBehaviour {
	
	public float xSpeed = 1f;
	public float jumpHeight = 1f;
	public GameObject soundwavePrefab;

	bool canJump = false;

	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
		}
		if (Input.GetKey(KeyCode.C)) {
			if (canJump == true) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpHeight);
				canJump = false;
			}
		}

		if (myRigidbody.velocity.y == 0) {
			canJump = true;
		} else {
			canJump = false;
		}

		shootProjectile ();
	}

	void shootProjectile(){
		if (Input.GetKeyDown(KeyCode.X)) {
			GameObject newSoundwaveObj = Instantiate(soundwavePrefab);
			newSoundwaveObj.transform.position = transform.position + Vector3.right;
		}
	}

}
