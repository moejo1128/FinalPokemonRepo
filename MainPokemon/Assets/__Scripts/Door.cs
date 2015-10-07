using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Vector3 doorPos;

	
	void OnCollisionEnter(Collision coll){
		if (gameObject.name == "GymDoor" || gameObject.name == "GymExit") {
			Main.S.resetBoulder1 = true;
			Main.S.resetBoulder2 = true;
		}
		if (coll.gameObject.tag == "Player") {
			Player.S.MoveThroughDoor(doorPos);
		}
	}
}
