using UnityEngine;
using System.Collections;

public class GiantHole : MonoBehaviour {

	public Vector3 doorPos;
	public static GiantHole S;

	void Start(){
		S = this;
		gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			print ("Entered door. Move to (" + doorPos.x + ", " + doorPos.y + ")");
			Player.S.MoveThroughDoor(doorPos);
		}
	}
}
