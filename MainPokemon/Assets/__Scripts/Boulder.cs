using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boulder : MonoBehaviour {
	
	public string[] speech;
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public RaycastHit hitInfo;
	public bool moving = false;
	public Vector3 targetPos;
	public Direction direction;
	public Vector3 moveVec = Vector3.zero;
	public float moveSpeed = 3;
	public bool onIce = false;
	public bool isBoulder1;

	bool needsActivation = false;
	bool reactivated = false;
	
	//public GameObject exMark;
	public SpriteRenderer sprend;
	
	// Use this for initialization
	void Start () {
		sprend = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void moveUp(){
		direction = Direction.up;
		moving = true;
		sprend.sprite = upSprite;
		moveVec = Vector3.zero;
		if (transform.position.y + 1 == Mathf.Floor(Player.S.transform.position.y)) {
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile", "Boulder"}) )){
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		moveVec = Vector3.up;
		targetPos = pos + moveVec;
	}
	
	public void moveDown() {
		direction = Direction.down;
		moving = true;
		sprend.sprite = downSprite;
		moveVec = Vector3.zero;
		if (transform.position.y - 1 == Mathf.Ceil(Player.S.transform.position.y)) {
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile", "Boulder"}) )){
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		moveVec = Vector3.down;
		targetPos = pos + moveVec;
	}
	
	public void moveRight() {
		direction = Direction.right;
		moving = true;
		sprend.sprite = downSprite;
		moveVec = Vector3.zero;
		if (transform.position.y - 1 == Mathf.Ceil(Player.S.transform.position.y)) {
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile", "Boulder"}) )){
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		moveVec = Vector3.right;
		targetPos = pos + moveVec;
	}
	
	public void moveLeft() {
		direction = Direction.left;
		moving = true;
		sprend.sprite = downSprite;
		moveVec = Vector3.zero;
		if (transform.position.y - 1 == Mathf.Ceil(Player.S.transform.position.y)) {
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile", "Boulder"}) )){
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		
		moveVec = Vector3.left;
		targetPos = pos + moveVec;
	}

	public Vector3 pos {
		get { return transform.position;}
		set { transform.position = value;}
	}
	
	public void Pushing(Direction playerDir, KeyCode key){
		switch(playerDir){
		case Direction.down:
			sprend.sprite = upSprite;
			direction = Direction.down;
			moveDown();
			break;
		case Direction.up:
			sprend.sprite = downSprite;
			direction = Direction.up;
			moveUp();
			break;
		case Direction.left:
			sprend.sprite = rightSprite;
			direction = Direction.left;
			moveLeft();
			break;
		case Direction.right:
			sprend.sprite = leftSprite;
			direction = Direction.right;	
			moveRight();
			break;
		}
	}
	
	
	void FixedUpdate(){
		if (Main.S.resetBoulder1) {
			if (isBoulder1 && !Player.S.hasSnowshoes) {
				Vector3 newPos = new Vector3(-53, 87, 0);
				transform.position = newPos;
				Main.S.resetBoulder1 = false;
			}
		}
		if (Main.S.resetBoulder2 && !Player.S.hasSnowshoes) {
			if (!isBoulder1) {
				Vector3 newPos = new Vector3(-53, 88, 0);
				transform.position = newPos;
				Main.S.resetBoulder2 = false;
			}
		}
		if (!Main.S.inDialog && !Main.S.paused) {
			if (!moving) {
				targetPos = pos + moveVec;
			} else {
				if ((targetPos - pos).magnitude < moveSpeed * Time.fixedDeltaTime) {
					pos = targetPos;
					moving = false;
				} else {
					pos += (targetPos - pos).normalized * moveSpeed * Time.fixedDeltaTime;
				}
			}
			if (pos.x.ToString() == "-54" && pos.y.ToString() == "84") {
				Vector3 holePosition1 = new Vector3(-35, 84, 0);
				transform.position = holePosition1;
				onIce = true;
				gameObject.layer = 8;
				gameObject.tag = "Untagged";
				needsActivation = true;
			}
			if (pos.x.ToString() == "-59" && pos.y.ToString() == "92") {
				Vector3 holePosition2 = new Vector3(-40, 92, 0);
				transform.position = holePosition2;
				onIce = true;
				gameObject.layer = 8;
				gameObject.tag = "Untagged";
				needsActivation = true;
			}
			if (pos.x.ToString() == "-58" && pos.y.ToString() == "88") {
				Vector3 holePosition3 = new Vector3(-39, 88, 0);
				transform.position = holePosition3;
				onIce = true;
				gameObject.layer = 8;
				gameObject.tag = "Untagged";
				needsActivation = true;
			}
			if (pos.x.ToString() == "-51" && pos.y.ToString() == "92") {
				Vector3 holePosition4 = new Vector3(-30, 92, 0);
				transform.position = holePosition4;
				onIce = true;
				gameObject.layer = 8;
				gameObject.tag = "Untagged";
				needsActivation = true;
			}
			if (pos.x.ToString() == "-15" && pos.y.ToString() == "82") {
				List<string> thudSound = new List<string>();
				thudSound.Add("THUD!!");
				thudSound.Add("CRASH!");
				thudSound.Add("...");
				thudSound.Add("It sounds like the ground broke open outside!");
				Dialog.S.ShowMessage(thudSound);
				GiantHole.S.gameObject.SetActive(true);
				gameObject.SetActive(false);
			}
		}
		if (needsActivation && !reactivated && Player.S.hasSnowshoes) {
			gameObject.layer = 20;
			gameObject.tag = "Boulder";
			reactivated = true;
		}
	}
	
	Ray GetRay(){
		switch (direction){
		case Direction.down:
			return new Ray(pos, Vector3.down);
		case Direction.left:
			return new Ray(pos, Vector3.left);
		case Direction.right:
			return new Ray(pos, Vector3.right);
		case Direction.up:
			return new Ray(pos, Vector3.up);
		default:
			return new Ray();
		}		
	}
	
	int GetLayerMask(string[] layerNames){
		int layerMask = 0;
		foreach(string layer in layerNames){
			layerMask = layerMask | (1 << LayerMask.NameToLayer(layer));
		}
		return layerMask;
	}
}


