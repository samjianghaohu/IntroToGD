using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Info : MonoBehaviour {
	public float stageLeftBorder = -1;
	public float stageRightBorder = 1;
	public float stageUpBorder = 1;
	public float stageDownBorder = -1;

	static float leftBorder;
	static float rightBorder;
	static float upBorder;
	static float downBorder;

	// Use this for initialization
	void Start () {
		leftBorder = stageLeftBorder;
		rightBorder = stageRightBorder;
		upBorder = stageUpBorder;
		downBorder = stageDownBorder;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public static float getLeftBorder(){
		return leftBorder;
	}

	public static float getRightBorder(){
		return rightBorder;
	}

	public static float getUpBorder(){
		return upBorder;
	}

	public static float getDownBorder(){
		return downBorder;
	}
}
