using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

	public string itemName;
	public int itemQuantity;
	public bool isUseableInBattle;
	public bool isUseableInWorld;
	public bool isTossable;
	bool itemExists = false;
	public AudioSource source;
	
	public string[] speech;

	void Awake(){
		source = GetComponent<AudioSource> ();

	}

	public void PlayDialog(){
		foreach(Item i in Player.S.itemPack){
			if (itemName == i.itemName){
				i.itemQuantity += itemQuantity;
				itemExists = true;
			}
		}

		if(!itemExists){
			Player.S.itemPack.Add(this);

		}

		print (speech[0]);
		Dialog.S.gameObject.SetActive (true);
		Color noAlpha = GameObject.Find ("DialogBackground").GetComponent<GUITexture> ().color;
		noAlpha.a = 255;
		GameObject.Find ("DialogBackground").GetComponent<GUITexture> ().color = noAlpha;
		speech [0] = "RED found " + itemName + "!";
		Dialog.S.ShowMessage (speech);
		source.Play ();
		gameObject.SetActive (false);
	}	

	public void useItem(){
		if (itemName == "POTION") {
			Menu_Pokemon.S.usingPotion = true;
			Menu_Pokemon.S.showPokemonMenu();
		}
		if (itemName == "POKEBALL") {
			if (BattleScreen.S.isTrainerBattle){
				List<string> cannot = new List<string>();
				cannot.Add ("Can't catch another trainer's Pokemon!");
				Dialog.S.ShowMessage(cannot);
			} else {
				itemQuantity--;
				float rand = Random.value;
				float hpRatio = BattleScreen.S.enemyPokemon.curHP / BattleScreen.S.enemyPokemon.maxHP;
				if (rand * BattleScreen.S.enemyPokemon.level * hpRatio < 0.5){
					List<string> catching = new List<string>();
					catching.Add ("...");
					if (Player.S.party.Count >= 6){
						catching.Add ("Success! " + BattleScreen.S.enemyPokemon.pokemonName + " was sent to the PC!");
					} else {
						catching.Add ("Success! " + BattleScreen.S.enemyPokemon.pokemonName + " was added to the party!");
					}
					Player.S.party.Add(BattleScreen.S.enemyPokemon);
					Dialog.S.ShowMessage(catching);
					BattleScreen.S.closeOnKeyDownX = 1 - catching.Count;
				} else {
					List<string> catching = new List<string>();
					catching.Add ("...");
					catching.Add ("Shoot! It was so close!");
					Dialog.S.ShowMessage(catching);
				}
			}
		}
	}

}
