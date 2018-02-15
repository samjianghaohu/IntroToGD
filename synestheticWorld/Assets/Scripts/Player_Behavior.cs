using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour {
	static float playerHealth = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void takeDamage(float damage){
		playerHealth -= damage;

		if (playerHealth <= 0) {
			Debug.Log ("You Died!");
		}
	}

	public static float getHealth(){
		return playerHealth;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "EnemyProjectile") {
			takeDamage (2);
			Destroy (other.gameObject);
		}
		if (other.tag == "Portal") {
			Player_Control.ifWin = CheckWinningCondition ();
		}
	}

	bool CheckWinningCondition(){
		int currentAbilityNum = Player_AbilityStack.GetAvailableAbilityNum ();
		List<Player_Ability> currentAbilityList = Player_AbilityStack.GetAbilityList ();

		GameObject[] targetAbilities = Stage_WinningCondition.GetTargetList ();

		int collectedTargetNum = 0;


		for (int i = 0; i < currentAbilityNum; i++) {
			for (int j = 0; j < targetAbilities.Length; j++) {
				if (currentAbilityList[i].projectilePrefab == targetAbilities [j]) {
					collectedTargetNum += 1;
					break;
				}
			}
		}

		if (collectedTargetNum == targetAbilities.Length) {
			return true;
		} else {
			return false;
		}
	}
}
