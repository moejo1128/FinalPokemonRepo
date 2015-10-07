﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	public string moveName;
	public string moveType;
	public int maxPP;
	public int curPP;
	public string effectDialog;
	int movePower;
	bool isSpecial;

	
	public Move(string name, string type, string effect, int power, int pp, bool special){
		moveName = name;
		moveType = type;
		maxPP = pp;
		curPP = pp;
		effectDialog = effect;
		movePower = power;
		isSpecial = special;
	}

	// Dictionary of moves power and PP
	public static Move getMove(string name){
		if (name == "TACKLE") {
			return new Move ("TACKLE", "NORMAL", "", 5, 35, false);
		} else if (name == "POUND") {
			return new Move ("POUND", "NORMAL", "", 7, 35, false);
		} else if (name == "BODY SLAM") {
			return new Move ("BODY SLAM", "NORMAL", "", 8, 20, false);
		} else if (name == "GROWL") {
			return new Move ("GROWL", "NORMAL", "ATTACK fell!", 0, 40, false);
		} else if (name == "TAIL WHIP") {
			return new Move ("TAIL WHIP", "NORMAL", "DEFENSE fell!", 0, 40, false);
		} else if (name == "EMBER") {
			return new Move ("EMBER", "FIRE", "", 8, 25, true);
		} else if (name == "VINE WHIP") {
			return new Move ("VINE WHIP", "GRASS", "", 8, 25, true);
		} else if (name == "WATER GUN") {
			return new Move ("WATER GUN", "WATER", "", 8, 25, true);
		} else if (name == "BUBBLEBEAM") {
			return new Move ("BUBBLEBEAM", "WATER", "", 9, 20, true);
		} else if (name == "GUST") {
			return new Move ("GUST", "FLYING", "", 5, 35, false);
		} else if (name == "PECK") {
			return new Move ("GUST", "FLYING", "", 7, 35, false);
		} else if (name == "CONFUSION") {
			return new Move ("CONFUSION", "PSYCHIC", "", 7, 25, true);
		} else if (name == "PSYCHIC") {
			return new Move ("PSYCHIC", "PSYCHIC", "", 8, 15, true);
		} else if (name == "SCRATCH") {
			return new Move ("SCRATCH", "NORMAL", "", 5, 35, false);
		} else if (name == "THUNDERSHOCK"){
			return new Move ("THUNDERSHOCK", "ELECTRIC", "", 7, 25, true);
		} else if (name == "HYPER FANG"){
			return new Move ("HYPER FANG", "NORMAL", "", 8, 25, false);
		} else if (name == "KARATE CHOP"){
			return new Move ("KARATE CHOP", "NORMAL", "", 6, 25, false);
		} else if (name == "AURORA BEAM"){
			return new Move ("AURORA BEAM", "ICE", "", 7, 25, true);
		} else if (name == "ICE BEAM"){
			return new Move ("ICE BEAM", "ICE", "", 9, 10, true);
		} else if (name == "LEER"){
			return new Move ("LEER", "NORMAL", "DEFENSE fell", 0, 35, false);
		} else if (name == "LOW KICK"){
			return new Move ("LOW KICK", "FIGHTING", "", 8, 25, false);
		} else {
			return new Move ("STRUGGLE", "NORMAL", "", 1, 99, false);
		}
	}

	// Dictionary of move effects
	string applyMoveEffect(Pokemon attacker, Pokemon defender){
		string effect;
		effect = defender.pokemonNickname + "'s " + effectDialog;
		if (moveName == "GROWL") {
			if (defender.attack > 2) {
				print (defender.pokemonName + "'s BATTLE ATTACK fell by 2");
				defender.battleAttack -= 2;
			} else {
				print (defender.pokemonName + "'s ATTACK won't go any lower!");
				effect = defender.pokemonNickname + "'s ATTACK won't go any lower!";
			}
		} else if (moveName == "TAIL WHIP") {
			if (defender.defense > 2){
				print (defender.pokemonName + "'s BATTLE DEFENSE fell by 2");
				defender.battleDefense -= 2;
			} else {
				print (defender.pokemonName + "'s DEFENSE won't go any lower!");
				effect = defender.pokemonNickname + "'s DEFENSE won't go any lower!";
			}
		} else if (moveName == "LEER") {
			if (defender.defense > 3){
				print (defender.pokemonName + "'s BATTLE DEFENSE fell by 3");
				defender.battleDefense -= 3;
			} else {
				print (defender.pokemonName + "'s DEFENSE won't go any lower!");
				effect = defender.pokemonNickname + "'s DEFENSE won't go any lower!";
			}
		}
		return effect;
	}


	// Add in more of the type advantages if necessary
	float typeMultiplier(string type){
		float multiplier = 1;
		if (moveType == "FIRE") {
			if (type == "WATER" || type == "DRAGON" || type == "ROCK" || type == "FIRE"){
				multiplier /= 2;
			}
			if (type == "GRASS" || type == "BUG" || type == "ICE"){
				multiplier *= 2;
			}
		} else if (moveType == "WATER") {
			if (type == "WATER" || type == "DRAGON" || type == "GRASS"){
				multiplier /= 2;
			}
			if (type == "FIRE" || type == "GROUND" || type == "ROCK"){
				multiplier *= 2;
			}
		} else if (moveType == "GRASS") {
			if (type == "FIRE" || type == "GRASS" || type == "POISON" || type == "FLYING" || type == "BUG" || type == "DRAGON"){
				multiplier /= 2;
			}
			if (type == "WATER" || type == "GROUND" || type == "ROCK"){
				multiplier *= 2;
			}
		} else if (moveType == "BUG") {
			if (type == "GHOST" || type == "FIGHTING" || type == "FLYING" || type == "FIRE"){
				multiplier /= 2;
			}
			if (type == "GRASS" || type == "BUG" || type == "ICE"){
				multiplier *= 2;
			}
		} else if (moveType == "FLYING") {
			if (type == "ELECTRIC" || type == "ROCK"){
				multiplier /= 2;
			}
			if (type == "GRASS" || type == "BUG" || type == "FIGHTING"){
				multiplier *= 2;
			}
		} else if (moveType == "ROCK") {
			if (type == "FIGHTING" || type == "GROUND"){
				multiplier /= 2;
			}
			if (type == "FLYING" || type == "FIRE" || type == "BUG" || type == "ICE"){
				multiplier *= 2;
			}
		} else if (moveType == "GHOST") {
			if (type == "NORMAL" || type == "PSYCHIC"){
				multiplier = 0;
			}
			if (type == "GHOST"){
				multiplier *= 2;
			}
		} else if (moveType == "DRAGON") {
		} else if (moveType == "POISON") {
		} else if (moveType == "GROUND") {
		} else if (moveType == "FIGHTING") {
			if (type == "NORMAL" || type == "ROCK" || type == "ICE"){
				multiplier *= 2;
			}
			if (type == "FLYING" || type == "POISON" || type == "BUG" || type == "PSYCHIC"){
				multiplier /= 2;
			}
			if (type == "GHOST"){
				multiplier = 0;
			}
		} else if (moveType == "PSYCHIC") {
			if (type == "FIGHTING" || type == "POISON"){
				multiplier *= 2;
			}
			if (type == "PSYCHIC"){
				multiplier /= 2;
			}
		} else if (moveType == "ICE") {
			if (type == "FLYING" || type == "GROUND" || type == "GRASS" || type == "DRAGON"){
				multiplier *= 2;
			}
			if (type == "WATER" || type == "ICE"){
				multiplier /= 2;
			}
		} else if (moveType == "ELECTRIC") {
			if (type == "ELECTRIC" || type == "GRASS" || type == "DRAGON"){
				multiplier /= 2;
			}
			if (type == "WATER" || type == "FLYING"){
				multiplier *= 2;
			}
			if (type == "GROUND"){
				multiplier = 0;
			}
		} else if (moveType == "NORMAL") {
			if (type == "ROCK"){
				multiplier /= 2;
			}
			if (type == "GHOST"){
				multiplier = 0;
			}
		}
		return multiplier;
	}

	public int damageCalc(Pokemon attacker, Pokemon defender, List<string> attackMessage){
		float effectiveMovePower = (float) movePower;
		if (Random.value < 0.1) {
			effectiveMovePower *= 1.5f;
			attackMessage.Add ("Critical hit!");
		}
		float typeMultiple = typeMultiplier(defender.type1) * typeMultiplier(defender.type2);
		if (typeMultiple > 1){
			attackMessage.Add("It's super effective!");
		} else if (typeMultiple < 1){
			attackMessage.Add("It's not very effective...");
		}
		effectiveMovePower *= typeMultiple;
		print ("TypeMultiplier is " + typeMultiple.ToString ());
		print ("EffectiveMovePower is " + effectiveMovePower.ToString ());
		if (!isSpecial) {
			print ("ATT: " + attacker.battleAttack.ToString() + " DEF: " + defender.battleDefense.ToString());
			return attacker.battleAttack + Mathf.CeilToInt(effectiveMovePower) - defender.battleDefense;
		} else {
			print ("SPATT: " + attacker.battleAttack.ToString() + " SPDEF: " + defender.battleDefense.ToString());
			return attacker.battleSpecial + Mathf.CeilToInt(effectiveMovePower) - defender.battleSpecial;
		}
	}

	public List<string> doMove(Pokemon attacker, Pokemon defender, bool playerIsAttacking){
		curPP--;
		List<string> dialog = new List<string>();
		if (!playerIsAttacking) {
			dialog.Add("Enemy " + attacker.pokemonName + " used " + moveName + "!");
		} else {
			dialog.Add(attacker.pokemonNickname + " used " + moveName + "!");
		}
		// assume moves either have an effect or do damage but not both
		if (effectDialog != "") {
			dialog.Add(applyMoveEffect (attacker, defender));
		} else {
			defender.curHP -= Mathf.Max(1, damageCalc(attacker, defender, dialog));
			if (defender.curHP < 0)
				defender.curHP = 0;
			BattleScreen.S.refreshBattleInfo();
		}
		return dialog;
	}

}
