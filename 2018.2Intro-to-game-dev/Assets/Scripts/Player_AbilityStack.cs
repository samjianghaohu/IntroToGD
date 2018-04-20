using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AbilityStack : MonoBehaviour {//This script defines player's ability stack

	//Declare ability pairs
	public GameObject crystal0;
	public Color a_color0;

	public GameObject crystal1;
	public Color a_color1;

	public GameObject crystal2;
	public Color a_color2;


	int availableAbilityNum = 0;
	Dictionary<GameObject, Player_Ability> paDict = new Dictionary<GameObject, Player_Ability>();
	List<Player_Ability> availableAbilityList = new List<Player_Ability>();


	// Use this for initialization
	void Start () {
		InitializePaDict ();
	}


	// Update is called once per frame
	void Update () {
	}


	//Initialize crystal dictionary
	void InitializePaDict(){
		paDict.Add (crystal0, new Player_Ability (a_color0));
		paDict.Add (crystal1, new Player_Ability (a_color1));
		paDict.Add (crystal2, new Player_Ability (a_color2));
	}


	public void AddAbility(GameObject newCrystal){
		availableAbilityList.Add (paDict [newCrystal]);
		availableAbilityNum += 1;
	}


	public List<Player_Ability> GetAbilityList(){
		return availableAbilityList;
	}


	public int GetAvailableAbilityNum(){
		return availableAbilityNum;
	}


	public void resetAbilities(){
		paDict = new Dictionary<GameObject, Player_Ability> ();
		availableAbilityList = new List<Player_Ability>();
		availableAbilityNum = 0;
	}
}
