using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour {//This script moves the main camera

	//Starting coordinate of camera
	public float startX;
	public float startY;

	public GameObject player;
	public GameObject stage;

	Stage_Info stageInfo;


	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (startX, startY, -10);

		stageInfo = stage.GetComponent<Stage_Info> ();
	}


	// Update is called once per frame
	void Update () {

		//Move camera horizontally according to player's x coordiniate
		if (player.transform.position.x >= stageInfo.GetLeftBorder() + 9 && player.transform.position.x <= stageInfo.GetRightBorder() - 9) {
			float xPos = player.transform.position.x;
			transform.position = new Vector3 (xPos, transform.position.y, -10);
		} else if (player.transform.position.x < stageInfo.GetLeftBorder() + 9) {
			transform.position = new Vector3 (stageInfo.GetLeftBorder() + 9, transform.position.y, -10);
		} else {
			transform.position = new Vector3 (stageInfo.GetRightBorder() - 9, transform.position.y, -10);
		}


		//Move camera vertically according to player's y coordiniate
		if (player.transform.position.y >= stageInfo.GetDownBorder() + 5 && player.transform.position.y <= stageInfo.GetUpBorder() - 5) {
			float yPos = player.transform.position.y;
			transform.position = new Vector3 (transform.position.x, yPos, -10);
		} else if (player.transform.position.y < stageInfo.GetDownBorder() + 5) {
			transform.position = new Vector3 (transform.position.x, stageInfo.GetDownBorder() + 5, -10);
		} else {
			transform.position = new Vector3 (transform.position.x, stageInfo.GetUpBorder() - 5, -10);
		}
	}
}