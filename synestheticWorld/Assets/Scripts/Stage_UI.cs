using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_UI : MonoBehaviour {//This script draws UI on stage
	public Image ability1;
	public Image ability2;
	public Image ability3;
	public Image health;


	Image[] abilities = new Image[3];


	int abilityNum = 0;
	List<Player_Ability> abilityList;


	// Use this for initialization
	void Start () {

		//initialize ability orb array
		abilities [0] = ability1;
		abilities [1] = ability2;
		abilities [2] = ability3;
	}


	// Update is called once per frame
	void Update () {

		//get player's current abilities
		abilityNum = Player_AbilityStack.GetAvailableAbilityNum ();
		abilityList = Player_AbilityStack.GetAbilityList ();


		UpdateAbilityIcons ();
		UpdateHealthIcons ();
	}


	void UpdateHealthIcons(){
		int healthNum = Player_Behavior.getHealth ();


		for (int i = 0; i < 20; i++) {
			Color healthColor = health.transform.GetChild (i).GetComponent<Image> ().color;

			if (i < healthNum) {
				healthColor = new Color (healthColor.r, healthColor.g, healthColor.b, 255);
			} else {
				healthColor = new Color (healthColor.r, healthColor.g, healthColor.b, 0);
			}

			health.transform.GetChild (i).GetComponent<Image> ().color = healthColor;
		}
	}


	void UpdateAbilityIcons (){
		for (int i = 0; i < abilityNum; i++) {
			abilities [i].color = abilityList [i].abilityColor;
		}
	}
}
