using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GhostSprite : MonoBehaviour {


	SpriteRenderer sprite;
	float timer = 0.2f; 


	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();


		transform.position = Player_Control.Instance.transform.position;  
		transform.localScale = Player_Control.Instance.transform.localScale;

		sprite.sprite = Player_Control.Instance.mySpriteRenderer.sprite; 
		sprite.color = new Vector4 (50, 50, 50, 0.2f);
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Destroy (gameObject);
		
		}
	}
}
