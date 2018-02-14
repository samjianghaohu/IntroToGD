﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour {
	
	public float xSpeed = 1f;
	public float jumpHeight = 1f;
	public GameObject soundwavePrefab;
	public LayerMask myLayerMask;

	GameObject projectilePrefab;
	SpriteRenderer mySpriteRenderer;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		projectilePrefab = soundwavePrefab;
		myRigidbody = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = new Vector2 (0, myRigidbody.velocity.y);
			
		PlayerMove ();
		ShootProjectile ();
		SwitchForm ();
	}

	void PlayerMove(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, Mathf.Infinity, myLayerMask);

		Debug.Log (hit.collider.transform.parent.tag);
		Debug.Log (hit.distance);

		if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = true;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			mySpriteRenderer.flipX = false;
		}
		if (Input.GetKey(KeyCode.C)) {
			if (hit.collider.transform.parent.tag == "Floor") {
				if (hit.distance <= 0.6f) {
					myRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
				}
			}
		}
	}

	void ShootProjectile(){
		if (Input.GetKeyDown (KeyCode.X)) {
			GameObject newSoundwaveObj = Instantiate (projectilePrefab);
			if (mySpriteRenderer.flipX == false) {
				newSoundwaveObj.transform.position = transform.position + Vector3.right;
			} else {
				newSoundwaveObj.GetComponent<SpriteRenderer> ().flipX = true;
				newSoundwaveObj.transform.position = transform.position + Vector3.left;
			}
		}
	}

	void SwitchForm(){
		int abilityNum = Player_AbilityStack.GetAvaliableAbilityNum ();
		List<Player_Ability> abilityList = Player_AbilityStack.GetAbilityList ();

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			mySpriteRenderer.color = new Color(1, 1, 1);
			projectilePrefab = soundwavePrefab;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if (abilityNum >= 1) {
				mySpriteRenderer.color = abilityList [0].abilityColor;
				projectilePrefab = abilityList [0].projectilePrefab;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if (abilityNum >= 2) {
				mySpriteRenderer.color = abilityList [1].abilityColor;
				projectilePrefab = abilityList [1].projectilePrefab;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			if (abilityNum >= 3) {
				mySpriteRenderer.color = abilityList [1].abilityColor;
				projectilePrefab = abilityList [1].projectilePrefab;
			}
		}
	}

}
