using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpModifier : MonoBehaviour {//This scripts modifies jump

	//Different modifiers used in different jumps (tap/hold)
	public float fallMultiplier;
	public float lowJumpMultiplier;


	Rigidbody2D myrigidbody;


	// Use this for initialization
	void Start () {
		myrigidbody = GetComponent<Rigidbody2D> ();
	}


	// Update is called once per frame
	void FixedUpdate () {
		if (myrigidbody != null) {
			if (myrigidbody.velocity.y < 0) {//fall slower/jump higher when you hold jump key
				myrigidbody.gravityScale = fallMultiplier;
			} else if (myrigidbody.velocity.y > 0 && !Input.GetKey (KeyCode.Space)) {//fall faster when you tap jump key
				myrigidbody.gravityScale = lowJumpMultiplier;
			} else {
				myrigidbody.gravityScale = 1f;
			}
		}
	}
}
