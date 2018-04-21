using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Parent : MonoBehaviour {//This script keeps track of whether or not the portal is activated

	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {


	}

	public void ActivatePortal(){

		transform.GetChild (0).GetComponent<Portal_LayerMovement> ().Activate ();
		transform.GetChild (1).GetComponent<Portal_LayerMovement> ().Activate ();
		transform.GetChild (2).GetComponent<Portal_LayerMovement> ().Activate ();
	}
}
