using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BattleCanvas : MonoBehaviour {

	public static BattleCanvas S;
	public Sprite rattataFrontSprite;
	public Sprite pidgeyFrontSprite;
	public Sprite ratticateFrontSprite;
	public Sprite meowthFrontSprite;
	public Sprite pikachuFrontSprite;
	public Sprite jynxFrontSprite;
	public Sprite cloysterFrontSprite;
	public Sprite laprasFrontSprite;
	public Sprite articunoFrontSprite;
	public Sprite psyduckFrontSprite;
	public Sprite golduckFrontSprite;
	public Sprite jigglypuffFrontSprite;
	public Sprite oddishFrontSprite;
	public Sprite machopFrontSprite;
	public Sprite machokeFrontSprite;
	public Sprite rapidashFrontSprite;
	public Sprite goldeenFrontSprite;

	public bool ________ = false;
	public Sprite charmanderBackSprite;
	public Sprite bulbasaurBackSprite;
	public Sprite squirtleBackSprite;
	public Sprite rattataBackSprite;
	public Sprite pidgeyBackSprite;
	public Sprite pikachuBackSprite;
	
	void Start(){
		S = this;
		gameObject.SetActive (false);
	}

	public void SetEnemySprite(string pokemonName){
		GameObject pokemon = this.gameObject.transform.GetChild (1).gameObject;
		if (pokemonName == "RATTATA")
			pokemon.GetComponent<Image>().sprite = rattataFrontSprite;
		if (pokemonName == "RATTICATE")
			pokemon.GetComponent<Image>().sprite = ratticateFrontSprite;
		if (pokemonName == "PIDGEY")
			pokemon.GetComponent<Image>().sprite = pidgeyFrontSprite;
		if (pokemonName == "MEOWTH")
			pokemon.GetComponent<Image>().sprite = meowthFrontSprite;
		if (pokemonName == "PIKACHU")
			pokemon.GetComponent<Image>().sprite = pikachuFrontSprite;
		if (pokemonName == "JYNX")
			pokemon.GetComponent<Image>().sprite = jynxFrontSprite;
		if (pokemonName == "CLOYSTER")
			pokemon.GetComponent<Image>().sprite = cloysterFrontSprite;
		if (pokemonName == "LAPRAS")
			pokemon.GetComponent<Image>().sprite = laprasFrontSprite;
		if (pokemonName == "ARTICUNO")
			pokemon.GetComponent<Image>().sprite = articunoFrontSprite;
		if (pokemonName == "PSYDUCK")
			pokemon.GetComponent<Image>().sprite = psyduckFrontSprite;
		if (pokemonName == "GOLDUCK")
			pokemon.GetComponent<Image>().sprite = golduckFrontSprite;
		if (pokemonName == "JIGGLYPUFF")
			pokemon.GetComponent<Image>().sprite = jigglypuffFrontSprite;
		if (pokemonName == "ODDISH")
			pokemon.GetComponent<Image>().sprite = oddishFrontSprite;
		if (pokemonName == "MACHOP")
			pokemon.GetComponent<Image>().sprite = machopFrontSprite;
		if (pokemonName == "MACHOKE")
			pokemon.GetComponent<Image>().sprite = machokeFrontSprite;
		if (pokemonName == "RAPIDASH")
			pokemon.GetComponent<Image>().sprite = rapidashFrontSprite;
		if (pokemonName == "GOLDEEN")
			pokemon.GetComponent<Image>().sprite = goldeenFrontSprite;
	}

	public void SetPokemonSprite(string pokemonName){
		GameObject pokemon = this.gameObject.transform.GetChild (0).gameObject;
		if (pokemonName == "CHARMANDER")
			pokemon.GetComponent<Image>().sprite = charmanderBackSprite;
		if (pokemonName == "BULBASAUR")
			pokemon.GetComponent<Image>().sprite = bulbasaurBackSprite;
		if (pokemonName == "SQUIRTLE")
			pokemon.GetComponent<Image>().sprite = squirtleBackSprite;
		if (pokemonName == "RATTATA")
			pokemon.GetComponent<Image>().sprite = rattataBackSprite;
		if (pokemonName == "PIDGEY")
			pokemon.GetComponent<Image>().sprite = pidgeyBackSprite;
		if (pokemonName == "PIKACHU")
			pokemon.GetComponent<Image>().sprite = pikachuBackSprite;
	}
}

