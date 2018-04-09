using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour {//This scripts determines player behaviors (damage, health, portal)

	public GameObject stage;

	int playerHealth = 20;
	Player_Control myControl;
	Player_AbilityStack myAStack;


	// Use this for initialization
	void Start () {
		myControl = GetComponent<Player_Control> ();
		myAStack = GetComponent<Player_AbilityStack> ();
	}


	// Update is called once per frame
	void Update () {
		
	}


	void TakeDamage(int damage){
		playerHealth -= damage;
	}


	void OnTriggerEnter2D(Collider2D other){

		//When hit by enemy projectile or touched by enemy
		if (other.tag == "EnemyProjectile" || other.tag == "Enemy") {
			if (myControl.IfStunned () == false) {
				int damage;

				if (other.tag == "Enemy") {
					damage = 1;
				} else {
					damage = other.GetComponent<Projectile_Behavior> ().GetPower ();
				}
				TakeDamage (damage);

				if (other.transform.position.x - transform.position.x >= 0) {
					myControl.Stune (1);
				} else {
					myControl.Stune (-1);
				}
			}

			if (other.tag == "EnemyProjectile") {
				Destroy (other.gameObject);
			}
		}


		//When reach portal
		if (other.tag == "Portal") {
			if (CheckWinningCondition ()){
				myControl.Win ();
			}
		}
	}


	bool CheckWinningCondition(){

		//Get current abilities
		int currentAbilityNum = myAStack.GetAvailableAbilityNum ();
		List<Player_Ability> currentAbilityList = myAStack.GetAbilityList ();


		//Get target abilities
		Color[] targetAbilities = stage.GetComponent<Stage_WinningCondition> ().GetTargetList ();


		//See if current abilities match target abilities
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


	public int getHealth(){
		return playerHealth;
	}


	public void resetHealth(){
		playerHealth = 20;
	}
}
