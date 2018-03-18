using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_WinningCondition : MonoBehaviour {
	public Color targetAbility0;
	public Color targetAbility1;
	public Color targetAbility2;

	public int targetAbilityNum = 0;
	static int targetNum;

	static Color[] abilityList = new Color[3];

	// Use this for initialization
	void Start () {
		abilityList [0] = targetAbility0;
		abilityList [1] = targetAbility1;
		abilityList [2] = targetAbility2;

		targetNum = targetAbilityNum;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static Color[] GetTargetList(){
		Color[] targetAbilities = new Color[targetNum];

		for (int i = 0; i < targetNum; i++) {
			targetAbilities [i] = abilityList [i];
		}

		return targetAbilities;
	}
}
