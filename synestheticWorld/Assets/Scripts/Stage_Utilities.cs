using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Utilities : MonoBehaviour {//This script contains helper functions

	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
		
	}
		

	public static bool compareColorsLoose(Color color1, Color color2) {
		if (Mathf.Abs (color1.r - color2.r) < 0.01f && Mathf.Abs (color1.g - color2.g) < 0.01f && Mathf.Abs (color1.b - color2.b) < 0.01f) {
			return true;
		}
		return false;
	}
		
}
