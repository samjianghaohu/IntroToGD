using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour {
	public GameObject player;
	//public GameObject stageInfo;
	public float startX;
	public float startY;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (startX, startY, -10);
	}

	// Update is called once per frame
	void Update () {
		if (player.transform.position.x >= Stage_Info.getLeftBorder() + 9 && player.transform.position.x <= Stage_Info.getRightBorder() - 9) {
			float xPos = player.transform.position.x;
			transform.position = new Vector3 (xPos, transform.position.y, -10);
		} else if (player.transform.position.x < Stage_Info.getLeftBorder() + 9) {
			transform.position = new Vector3 (Stage_Info.getLeftBorder() + 9, transform.position.y, -10);
		} else {
			transform.position = new Vector3 (Stage_Info.getRightBorder() - 9, transform.position.y, -10);
		}

		if (player.transform.position.y >= Stage_Info.getDownBorder() + 5 && player.transform.position.y <= Stage_Info.getUpBorder() - 5) {
			float yPos = player.transform.position.y;
			transform.position = new Vector3 (transform.position.x, yPos, -10);
		} else if (player.transform.position.y < Stage_Info.getDownBorder() + 5) {
			transform.position = new Vector3 (transform.position.x, Stage_Info.getDownBorder() + 5, -10);
		} else {
			transform.position = new Vector3 (transform.position.x, Stage_Info.getUpBorder() - 5, -10);
		}
	}
}