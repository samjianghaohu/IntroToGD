using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_WinningCondition : MonoBehaviour {
	public GameObject targetAbility0;
	public GameObject targetAbility1;
	public GameObject targetAbility2;

	public int targetAbilityNum = 0;
	static int targetNum;

	static GameObject[] abilityList = new GameObject[3];

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

	public static GameObject[] GetTargetList(){
		GameObject[] targetAbilities = new GameObject[targetNum];

		for (int i = 0; i < targetNum; i++) {
			targetAbilities [i] = abilityList [i];
		}

		return targetAbilities;
	}
}
