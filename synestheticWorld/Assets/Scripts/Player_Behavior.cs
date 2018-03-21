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

	void TakeDamage(float damage){
		playerHealth -= damage;

		if (playerHealth <= 0) {
			Debug.Log ("You Died!");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "EnemyProjectile") {
			if (Player_Control.IfStunned () == false) {
				int damage = other.GetComponent<Projectile_Behavior> ().GetPower ();
				TakeDamage (damage);

				if (other.transform.position.x - transform.position.x >= 0) {
					Player_Control.Stune (1);
				} else {
					Player_Control.Stune (-1);
				}
			}
			Destroy (other.gameObject);
		}
		if (other.tag == "Portal") {
			Player_Control.ifWin = CheckWinningCondition ();
		}
	}

	bool CheckWinningCondition(){
		int currentAbilityNum = Player_AbilityStack.GetAvailableAbilityNum ();
		List<Player_Ability> currentAbilityList = Player_AbilityStack.GetAbilityList ();

		Color[] targetAbilities = Stage_WinningCondition.GetTargetList ();

		int collectedTargetNum = 0;


		for (int i = 0; i < currentAbilityNum; i++) {
			for (int j = 0; j < targetAbilities.Length; j++) {
				if (Stage_Utilities.compareColorsLoose(currentAbilityList[i].abilityColor, targetAbilities [j])) {
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

	public static float getHealth(){
		return playerHealth;
	}

	public static void resetHealth(){
		playerHealth = 20;
	}
}
