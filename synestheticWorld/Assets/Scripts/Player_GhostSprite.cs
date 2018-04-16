using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GhostSprite : MonoBehaviour { //Creates the ghost of the character 


	SpriteRenderer ghost;
	float timer = 0.2f; 


	// Use this for initialization
	void Start () {
		ghost = GetComponent<SpriteRenderer>();


		transform.position = Player_Control.Instance.transform.position;  
		transform.localScale = Player_Control.Instance.transform.localScale;

		ghost.sprite = Player_Control.Instance.mySpriteRenderer.sprite; 
		ghost.color = new Vector4 (50, 50, 50, 0.2f);
	

	}


	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Destroy (gameObject);
		
		}
	}
}
