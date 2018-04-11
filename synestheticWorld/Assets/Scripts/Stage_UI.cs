using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_UI : MonoBehaviour {//This script draws UI on stage

	//Ability icons and health bar
	public Image ability1;
	public Image ability2;
	public Image ability3;
	public Image health;
	public GameObject player;


	Image[] abilities = new Image[3];


	int abilityNum = 0;
	List<Player_Ability> abilityList;


	// Use this for initialization
	void Start () {

		//Initialize health circle
		health.fillAmount = 1;


		//Initialize ability orb array
		abilities [0] = ability1;
		abilities [1] = ability2;
		abilities [2] = ability3;
	}


	// Update is called once per frame
	void Update () {

		//Get player's current abilities
		abilityNum = player.GetComponent<Player_AbilityStack>().GetAvailableAbilityNum ();
		abilityList = player.GetComponent<Player_AbilityStack>().GetAbilityList ();


		UpdateAbilityIcons ();
		UpdateHealthIcons ();
	}


	void UpdateHealthIcons(){
		int healthNum = player.GetComponent<Player_Behavior> ().getHealth ();
		float healthAmount = healthNum / 20f;

		health.fillAmount = healthAmount;


		if (healthAmount <= 0.3f) {
			health.color = Color.red;
		} else {
			health.color = Color.white;
		}
	}


	void UpdateAbilityIcons (){
		for (int i = 0; i < abilityNum; i++) {
			abilities [i].color = abilityList [i].abilityColor;
		}
	}
}
