using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpModifier : MonoBehaviour {
	public float fallMultiplier;
	public float lowJumpMultiplier;

	Rigidbody2D myrigidbody;
	// Use this for initialization
	void Start () {
		myrigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (myrigidbody.velocity.y < 0) {
			myrigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if (myrigidbody.velocity.y > 0 && !Input.GetKey (KeyCode.C)) {
			myrigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
	}
}
