using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AbilityStack : MonoBehaviour {
	public GameObject crystal0;
	//public Sprite a_icon0;
	public Color a_color0;
	//public GameObject a_projectile0;

	public GameObject crystal1;
	//public Sprite a_icon1;
	public Color a_color1;
	//public GameObject a_projectile1;

	public GameObject crystal2;
	//public Sprite a_icon2;
	public Color a_color2;
	//public GameObject a_projectile2;

	static int availableAbilityNum = 0;
	static Dictionary<GameObject, Player_Ability> paDict = new Dictionary<GameObject, Player_Ability>();
	static List<Player_Ability> availableAbilityList = new List<Player_Ability>();

	// Use this for initialization
	void Start () {
		InitializePaDict ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void InitializePaDict(){
		paDict.Add (crystal0, new Player_Ability (a_color0));
		paDict.Add (crystal1, new Player_Ability (a_color1));
		paDict.Add (crystal2, new Player_Ability (a_color2));
	}
		


	public static void AddAbility(GameObject newCrystal){
		availableAbilityList.Add (paDict [newCrystal]);
		availableAbilityNum += 1;
	}

	public static List<Player_Ability> GetAbilityList(){
		return availableAbilityList;
	}

	public static int GetAvailableAbilityNum(){
		return availableAbilityNum;
	}

	public static void resetAbilities(){
		paDict = new Dictionary<GameObject, Player_Ability> ();
		availableAbilityList = new List<Player_Ability>();
		availableAbilityNum = 0;
	}
}
