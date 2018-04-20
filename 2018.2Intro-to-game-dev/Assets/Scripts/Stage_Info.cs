using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Info : MonoBehaviour {//This script contains stage border positions
	
	public float stageLeftBorder;
	public float stageRightBorder;
	public float stageUpBorder;
	public float stageDownBorder;
	public int nextStage;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//functions that get each border positions
	public float GetLeftBorder(){
		return stageLeftBorder;
	}


	public float GetRightBorder(){
		return stageRightBorder;
	}


	public float GetUpBorder(){
		return stageUpBorder;
	}


	public float GetDownBorder(){
		return stageDownBorder;
	}


	public int GetNextStage(){
		return nextStage;
	}
}
