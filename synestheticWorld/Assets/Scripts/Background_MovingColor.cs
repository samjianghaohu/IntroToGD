using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_MovingColor : MonoBehaviour {//This script moves the color block behind the bg image.

	public float movingSpeed;
	public GameObject camera;


	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {
		if (transform.position.x >= (camera.transform.position.x + 12)) {
			transform.position = new Vector3 ((camera.transform.position.x - 12), transform.position.y, transform.position.z);
		} else {
			transform.position += movingSpeed * Vector3.right;
		}
	}
}
