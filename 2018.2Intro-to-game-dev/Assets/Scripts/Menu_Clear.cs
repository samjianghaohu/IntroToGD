using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Clear : MonoBehaviour {//This script loads next scene

	//Keep track of whether or not player has failed to reach the current stage
	public static int failedScene = 0;
	public static bool ifFailed = false;


	//Normal next stage
	public int nextScene;

	int sceneToGo;


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return)) {
			if (ifFailed) {
				sceneToGo = failedScene;
			} else {
				sceneToGo = nextScene;
			}

			failedScene = 0;
			ifFailed = false;
			SceneManager.LoadScene (sceneToGo);
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
