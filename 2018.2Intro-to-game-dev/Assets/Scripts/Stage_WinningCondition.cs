using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_WinningCondition : MonoBehaviour {//This script contains winning condition of a stage

	//All possible target abilities
	public Color targetAbility0;
	public Color targetAbility1;
	public Color targetAbility2;

	public int targetAbilityNum = 0;


	Color[] abilityList = new Color[3];


	// Use this for initialization
	void Start () {
		abilityList [0] = targetAbility0;
		abilityList [1] = targetAbility1;
		abilityList [2] = targetAbility2;
	}


	// Update is called once per frame
	void Update () {
		
	}


	public Color[] GetTargetList(){
		Color[] targetAbilities = new Color[targetAbilityNum];

		for (int i = 0; i < targetAbilityNum; i++) {
			targetAbilities [i] = abilityList [i];
		}

		return targetAbilities;
	}
}
