using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_UI : MonoBehaviour {
	public Text playerHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		playerHealth.text = "Health: " + Player_Behavior.getHealth();
	}
}
