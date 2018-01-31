using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
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
		if (player.transform.position.x >= StageInfo.stageLeftBoarder + 9 && player.transform.position.x <= StageInfo.stageRightBoarder - 9) {
			float xPos = player.transform.position.x;
			transform.position = new Vector3 (xPos, transform.position.y, -10);
		} else if (player.transform.position.x < StageInfo.stageLeftBoarder + 9) {
			transform.position = new Vector3 (StageInfo.stageLeftBoarder + 9, transform.position.y, -10);
		} else {
			transform.position = new Vector3 (StageInfo.stageRightBoarder - 9, transform.position.y, -10);
		}

		if (player.transform.position.y >= StageInfo.stageDownBoarder + 5 && player.transform.position.x <= StageInfo.stageUpBoarder - 5) {
			float yPos = player.transform.position.y;
			transform.position = new Vector3 (transform.position.x, yPos, -10);
		} else if (player.transform.position.y < StageInfo.stageDownBoarder + 5) {
			transform.position = new Vector3 (transform.position.x, StageInfo.stageDownBoarder + 5, -10);
		} else {
			transform.position = new Vector3 (transform.position.x, StageInfo.stageUpBoarder - 5, -10);
		}
	}
}