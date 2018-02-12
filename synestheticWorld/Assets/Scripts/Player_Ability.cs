using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Ability {

	public Sprite abilitySprite;
	public Color abilityColor;
	public GameObject projectilePrefab;

	public Player_Ability(Sprite newIcon, Color newColor, GameObject newProjectile){
		abilitySprite = newIcon;
		abilityColor = newColor;
		projectilePrefab = newProjectile;
	}

}
