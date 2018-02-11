using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Behavior : MonoBehaviour {
	static float alpah = 1f;
	SpriteRenderer mySpriteRender;

	// Use this for initialization
	void Start () {
		mySpriteRender = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		mySpriteRender.color = new Color (mySpriteRender.color.r, mySpriteRender.color.g, mySpriteRender.color.b, alpah);
	}

	public static int fadeOut(){
		if (Mathf.Abs (alpah - 0) <= 0.01f) {
			return 1;
		} else {
			alpah -= 0.01f;
			return 0;
		}
	}
}
