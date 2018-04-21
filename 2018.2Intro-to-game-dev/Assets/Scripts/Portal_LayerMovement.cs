using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_LayerMovement : MonoBehaviour {//This script defines movements of a portal layer

	//Declare relevent parameters
	public float rotateDirect;
	public float rotateSpeed;

	bool isActivated;


	// Use this for initialization
	void Start () {
		isActivated = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		//Portal starts rotating when activated or otherwise stays still
		if (isActivated) {
			transform.eulerAngles += new Vector3 (0, 0, rotateDirect * rotateSpeed);
		} else {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}
	}

	public void Activate(){
		isActivated = true;
	}
}
