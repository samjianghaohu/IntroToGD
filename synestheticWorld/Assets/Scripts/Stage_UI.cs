using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_UI : MonoBehaviour {
	public Text playerHealth;
	public Image ability1;
	public Image ability2;
	public Image ability3;

	Image[] abilities = new Image[3];

	int abilityNum = 0;
	List<Player_Ability> abilityList;

	// Use this for initialization
	void Start () {
		abilities [0] = ability1;
		abilities [1] = ability2;
		abilities [2] = ability3;
	}
	
	// Update is called once per frame
	void Update () {
		abilityNum = Player_AbilityStack.GetAvailableAbilityNum ();
		abilityList = Player_AbilityStack.GetAbilityList ();

		UpdateAbilityIcons ();
		UpdateHealth ();
	}

	void UpdateHealth(){
		playerHealth.text = "Health: " + Player_Behavior.getHealth();
	}

	void UpdateAbilityIcons (){
		for (int i = 0; i < abilityNum; i++) {
			abilities [i].sprite = abilityList [i].abilitySprite;
			abilities [i].color = abilityList [i].abilityColor;
		}
	}
}
