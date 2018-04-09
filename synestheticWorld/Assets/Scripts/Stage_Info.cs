using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Info : MonoBehaviour {//This script contains stage border positions
	
	public float stageLeftBorder;
	public float stageRightBorder;
	public float stageUpBorder;
	public float stageDownBorder;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//functions that get each border positions
	public float getLeftBorder(){
		return stageLeftBorder;
	}

	public float getRightBorder(){
		return stageRightBorder;
	}

	public float getUpBorder(){
		return stageUpBorder;
	}

	public float getDownBorder(){
		return stageDownBorder;
	}
}
