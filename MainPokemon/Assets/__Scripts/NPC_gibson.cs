using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_gibson : MonoBehaviour {

	public string[] speech;
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public bool allowTurning = true;
	public bool allowMovement = true;
	public bool isTrainer = false;
	public float castDist;
	public RaycastHit hitInfo;
	public bool moving = false;
	public Vector3 targetPos;
	public Direction direction;
	public Vector3 moveVec = Vector3.zero;
	public float moveSpeed = 3;
	bool justMovedDown = true;
	bool readyToBattle;
	bool gaveSnowshoes = false;

	//public GameObject exMark;
	public SpriteRenderer sprend;

	// Use this for initialization
	void Start () {
		sprend = gameObject.GetComponent<SpriteRenderer> ();
		readyToBattle = true;
	}

	void initBattle(){
		readyToBattle = false;
		BattleScreen.S.isTrainerBattle = true;
		BattleScreen.S.enemyName = "GYM LEADER GIBSON";
		Pokemon pkm1 = new Pokemon ("JYNX", "JOX", 30, 52, 8, 8, 11, 10, 11, "ICE", "PSYCHIC");
		pkm1.moves = new List<Move>();
		pkm1.moves.Add (Move.getMove("BUBBLEBEAM"));
		pkm1.moves.Add (Move.getMove("CONFUSION"));
		Pokemon pkm2 = new Pokemon ("CLOYSTER", "CORY", 35, 25, 8, 8, 11, 12, 11, "WATER", "ICE");
		pkm2.moves = new List<Move>();
		pkm2.moves.Add (Move.getMove("WATER GUN"));
		pkm2.moves.Add (Move.getMove("AURORA BEAM"));
		Pokemon pkm3 = new Pokemon ("LAPRAS", "LARRY", 40, 25, 9, 8, 14, 9, 14, "WATER", "ICE");
		pkm3.moves = new List<Move>();
		pkm3.moves.Add (Move.getMove("BUBBLEBEAM"));
		pkm3.moves.Add (Move.getMove("AURORA BEAM"));
		pkm3.moves.Add (Move.getMove("BODY SLAM"));
		Pokemon pkm4 = new Pokemon ("ARTICUNO", "ARTY", 50, 25, 10, 12, 13, 11, 13, "ICE", "FLYING");
		pkm4.moves = new List<Move>();
		pkm4.moves.Add (Move.getMove("PECK"));
		pkm4.moves.Add (Move.getMove("ICE BEAM"));
		pkm4.moves.Add (Move.getMove("BUBBLEBEAM"));
		BattleScreen.S.enemyParty.Clear();
		BattleScreen.S.enemyParty.Add(pkm1);
		BattleScreen.S.enemyParty.Add(pkm2);
		BattleScreen.S.enemyParty.Add(pkm3);
		BattleScreen.S.enemyParty.Add(pkm4);
		BattleScreen.S.showBattleScreen();
	}

	public void PlayDialog(){
		if (readyToBattle) {
			initBattle();
		} else if (!gaveSnowshoes) {
			List<string> shoes = new List<string>();
			shoes.Add("You have defeated me! Take these SNOWSHOES as a reward!");
			shoes.Add("With the SNOWSHOES you won't slip on ice!");
			Dialog.S.ShowMessage (shoes);
			Item snowshoes = new Item();
			snowshoes.itemName = "SNOWSHOES";
			snowshoes.itemQuantity = 1;
			snowshoes.isTossable = false;
			snowshoes.isUseableInBattle = false;
			snowshoes.isUseableInWorld = false;
			Player.S.itemPack.Add(snowshoes);
			gaveSnowshoes = true;
			Player.S.hasSnowshoes = true;
		} else {
			Dialog.S.ShowMessage (speech);

		}
	}

	public void moveUp(){
		direction = Direction.up;
		moving = true;
		sprend.sprite = upSprite;
		if (transform.position.y + 1 == Mathf.Floor(Player.S.transform.position.y)) {
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile"}) )){
			moveVec = Vector3.zero;
			moving = true;
			return;
		}
		moveVec = Vector3.up;
	}

	public void moveDown() {
		direction = Direction.down;
		moving = true;
		sprend.sprite = downSprite;
		if (transform.position.y - 1 == Mathf.Ceil(Player.S.transform.position.y)) {
			moveVec = Vector3.zero;
			moving = false;
			return;
		}
		if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile"}) )){
			moveVec = Vector3.zero;
			moving = true;
			return;
		}
		moveVec = Vector3.down;
	}

	public Vector3 pos {
		get { return transform.position;}
		set { transform.position = value;}
	}

	public void FacePlayer(Direction playerDir){
		switch(playerDir){
		case Direction.down:
			direction = Direction.up;
			sprend.sprite = upSprite;
			break;
		case Direction.up:
			direction = Direction.down;
			sprend.sprite = downSprite;
			break;
		case Direction.left:
			direction = Direction.right;
			sprend.sprite = rightSprite;
			break;
		case Direction.right:
			direction = Direction.left;
			sprend.sprite = leftSprite;
			break;
		}
	}

	void FixedUpdate(){
		if (!Main.S.inDialog && !Main.S.paused) {
			if (!moving) {
				if (Random.value < 0.01 && allowTurning) {
					float randDir = Random.value;
					if (randDir < 0.25){
						FacePlayer (Direction.left);
					} else if (randDir < 0.5){
						FacePlayer (Direction.right);
					} else if (randDir < 0.75){
						FacePlayer (Direction.down);
					} else {
						FacePlayer (Direction.up);
					}
				}
				if (Random.value < 0.01 && allowMovement) {
					if (justMovedDown){
						moveUp ();
						justMovedDown = false;
					} else {
						moveDown ();
						justMovedDown = true;
					}
				}
				targetPos = pos + moveVec;
			} else {
				if ((targetPos - pos).magnitude < moveSpeed * Time.fixedDeltaTime) {
					pos = targetPos;
					moving = false;
				} else {
					pos += (targetPos - pos).normalized * moveSpeed * Time.fixedDeltaTime;
				}
			}
		}



		if (readyToBattle && isTrainer 
		    	&& !Physics.Raycast(GetRay(), out hitInfo, castDist, GetLayerMask(new string[] {"Boulder"}))
		    	&& Physics.Raycast(GetRay(), out hitInfo, castDist, GetLayerMask(new string[] {"Player"}))
		  		&& ((Player.S.transform.position.x+Player.S.transform.position.y)%2 == 1 || 
		 		(Player.S.transform.position.x+Player.S.transform.position.y)%2 == 0 )){

			Player player = hitInfo.collider.gameObject.GetComponent<Player>();
			Vector3 gotoPlayer = player.transform.position;
			print("I SEE YOU!!");
			Player.S.moving = false;
			allowMovement = false;
			allowTurning = false;
			if(direction == Direction.down){
				gotoPlayer.y++;
			}
			else if(direction == Direction.up){
				gotoPlayer.y--;
			}
			else if(direction == Direction.right){
				gotoPlayer.x--;
			}
			else{
				gotoPlayer.x++;
			}
			
			transform.position = Vector3.MoveTowards(transform.position, gotoPlayer, moveSpeed*.04f);
			Invoke("DisableCast", 3);

			if (pos == gotoPlayer){
				initBattle();
			}
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

	void DisableCast(){
		castDist = 0;
	}
	


}


