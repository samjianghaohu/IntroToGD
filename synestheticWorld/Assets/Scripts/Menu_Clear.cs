﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Clear : MonoBehaviour {
	public int nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return)) {
			SceneManager.LoadScene (nextScene);
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}