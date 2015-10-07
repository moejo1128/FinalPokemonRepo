using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction{
	down,
	left,
	up,
	right
}

public class Player : MonoBehaviour {
	
	public static Player S;
	public float moveSpeed;
	public float jumpSpeed;
	public int tileSize;
	
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;
	
	public SpriteRenderer sprend;
	
	public bool _________________;
	
	public RaycastHit hitInfo;
	public bool hasSnowshoes = false;
	public bool moving = false;
	public Vector3 targetPos;
	public Direction direction;
	public Vector3 moveVec;
	public Vector3 doorCurLoc;
	
	public List<Item> itemPack;
	public List<Pokemon> party;
	public int money;
	public string name;
	
	
	void Awake(){
		S = this;
	}
	
	void Start() {
		sprend = gameObject.GetComponent<SpriteRenderer> ();
		
		Item pokeball = new Item();
		pokeball.itemName = "POKEBALL";
		pokeball.itemQuantity = 5;
		pokeball.isUseableInBattle = true;
		pokeball.isUseableInWorld = false;
		pokeball.isTossable = true;
		itemPack.Add (pokeball);
		
		Item potion = new Item();
		potion.itemName = "POTION";
		potion.itemQuantity = 5;
		potion.isUseableInBattle = true;
		potion.isUseableInWorld = true;
		potion.isTossable = true;
		itemPack.Add (potion);

		
		Item parcel = new Item();
		parcel.itemName = "OAK'S PARCEL";
		parcel.itemQuantity = 1;
		parcel.isUseableInBattle = false;
		parcel.isUseableInWorld = false;
		parcel.isTossable = false;
		itemPack.Add (parcel);
		
		Pokemon charles = new Pokemon ("CHARMANDER", "CHARLES", 39, 4, 7, 11, 9, 10, 8, "FIRE", "");
		charles.moves = new List<Move>();
		charles.moves.Add (Move.getMove("SCRATCH"));
		charles.moves.Add (Move.getMove("GROWL"));
		charles.moves.Add (Move.getMove("EMBER"));
		party.Add (charles);
		
		Pokemon james = new Pokemon ("BULBASAUR", "BULBY", 45, 1, 7, 9, 9, 9, 12, "GRASS", "POISON");
		james.moves = new List<Move>();
		james.moves.Add (Move.getMove("TACKLE"));
		james.moves.Add (Move.getMove("GROWL"));
		james.moves.Add (Move.getMove("VINE WHIP"));
		party.Add (james);
		
		Pokemon joe = new Pokemon ("SQUIRTLE", "SQUIRT", 44, 4, 7, 9, 10, 8, 10, "WATER", "");
		joe.moves = new List<Move>();
		joe.moves.Add (Move.getMove("TACKLE"));
		joe.moves.Add (Move.getMove("TAIL WHIP"));
		joe.moves.Add (Move.getMove("WATER GUN"));
		party.Add (joe);
		
	}
	
	new public Rigidbody rigidbody{
		get {return gameObject.GetComponent<Rigidbody>();}
	}
	
	public Vector3 pos{
		get { return transform.position;}
		set { transform.position = value;}
	}
	
	void FixedUpdate(){
		if (Input.GetKeyDown(KeyCode.L)){
			Application.LoadLevel ("SceneRed3D");
		}
		if (!moving && !Main.S.inDialog && !Main.S.paused) {
			if(Input.GetKeyDown(KeyCode.Z)){
				print ("CheckForAction called");
				CheckForAction();
			}
			
			if(!hasSnowshoes && Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Ice"})) && 
			   !Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable"}))){
				Vector3 nextpos = transform.position;
				if (direction == Direction.up){
					nextpos.y = nextpos.y + 2.5f;
					moveVec = Vector3.up;
				}
				if (direction == Direction.down){
					nextpos.y = nextpos.y - 2.5f;
					moveVec = Vector3.down;
				}
				if (direction == Direction.left){
					nextpos.x = nextpos.x - 2.5f;
					moveVec = Vector3.left;
				}
				if (direction == Direction.right){
					nextpos.x = nextpos.y + 2.5f;
					moveVec = Vector3.right;
				}
				moving = true;
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				moveVec = Vector3.right;
				direction = Direction.right;
				sprend.sprite = rightSprite;
				moving = true;
			}
			
			else if (Input.GetKey (KeyCode.LeftArrow)) {
				moveVec = Vector3.left;
				direction = Direction.left;
				sprend.sprite = leftSprite;
				moving = true;
			}
			
			else if (Input.GetKey (KeyCode.UpArrow)) {
				moveVec = Vector3.up;
				direction = Direction.up;
				sprend.sprite = upSprite;
				moving = true;
			}
			
			else if (Input.GetKey (KeyCode.DownArrow)) {
				moveVec = Vector3.down;
				direction = Direction.down;
				sprend.sprite = downSprite;
				moving = true; 
			} else {
				moveVec = Vector3.zero;
				moving = false;
			}
			
			if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile", "Hole"}) ) && 
			        gameObject.GetComponent<BoxCollider>().enabled == false){
				//just incase box collider is disabled when we need it to not be disabled
				gameObject.GetComponent<BoxCollider>().enabled = true;
			}
			
			if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Immovable", "NPC", "Pickup", "Ledge","WaterTile", "Hole"}) )){
				moveVec = Vector3.zero;
				moving = false;
			}
			
			if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Ledge"})) && direction == Direction.down && Input.GetKey(KeyCode.DownArrow)){
				//check if the ledge underneath is a "Ledge" layer, if so and the player presses down, shift the player down
				Vector3 underLedge = transform.position;
				underLedge.y = underLedge.y - 5f;
				moveVec = 2 * Vector3.down;
				//moveVec += Vector3.down;
				moving = true;
			}
			
			if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Grass"}) ) && 
			   (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.UpArrow) || 
			 Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow))){
				if(Random.Range(0,100) < 15){
					print ("We are in grass.");
					float rand = Random.value;
					int randBonus = Mathf.FloorToInt(Random.value * 2);
					if (rand < 0.5){
						BattleScreen.S.isTrainerBattle = false;
						Pokemon pkm = new Pokemon ("RATTATA", "RATTATA", 30, 19, 3+randBonus, 9+randBonus, 8+randBonus, 8+randBonus, 7+randBonus, "NORMAL", "");
						pkm.moves = new List<Move>();
						pkm.moves.Add (Move.getMove("TACKLE"));
						pkm.moves.Add (Move.getMove("TAIL WHIP"));
						BattleScreen.S.enemyParty.Clear();
						BattleScreen.S.enemyParty.Add(pkm);
						BattleScreen.S.showBattleScreen();
					} else if (rand < 0.8){
						BattleScreen.S.isTrainerBattle = false;
						Pokemon pkm = new Pokemon ("PIDGEY", "PIDGEY", 30, 19, 3+randBonus, 10+randBonus, 7+randBonus, 8+randBonus, 7+randBonus, "FLYING", "");
						pkm.moves = new List<Move>();
						pkm.moves.Add (Move.getMove("SCRATCH"));
						pkm.moves.Add (Move.getMove("TAIL WHIP"));
						pkm.moves.Add (Move.getMove("GUST"));
						BattleScreen.S.enemyParty.Clear();
						BattleScreen.S.enemyParty.Add(pkm);
						BattleScreen.S.showBattleScreen();
					} else{
						BattleScreen.S.isTrainerBattle = false;
						Pokemon pkm = new Pokemon ("PIKACHU", "PIKACHU", 30, 19, 3+randBonus, 8+randBonus, 7+randBonus, 8+randBonus, 9+randBonus, "ELECTRIC", "");
						pkm.moves = new List<Move>();
						pkm.moves.Add (Move.getMove("TACKLE"));
						pkm.moves.Add (Move.getMove("TAIL WHIP"));
						pkm.moves.Add (Move.getMove("THUNDERSHOCK"));
						BattleScreen.S.enemyParty.Clear();
						BattleScreen.S.enemyParty.Add(pkm);
						BattleScreen.S.showBattleScreen();
					}
				}
			}
			if(Physics.Raycast(GetRay(), out hitInfo, 1f, GetLayerMask(new string[] {"Boulder"}) )){
				moveVec = Vector3.zero;
				moving = false;
				Boulder boulder = hitInfo.collider.gameObject.GetComponent<Boulder>();
				if(Input.GetKeyDown(KeyCode.UpArrow)){
					if (!boulder.moving) boulder.Pushing(direction, KeyCode.UpArrow);
				}
				if(Input.GetKeyDown(KeyCode.DownArrow)){
					if (!boulder.moving) boulder.Pushing(direction, KeyCode.DownArrow);
				}
				if(Input.GetKeyDown(KeyCode.RightArrow)){
					if (!boulder.moving) boulder.Pushing(direction, KeyCode.RightArrow);
				}
				if(Input.GetKeyDown(KeyCode.LeftArrow)){
					if (!boulder.moving) boulder.Pushing(direction, KeyCode.LeftArrow);
				}
			}
			targetPos = pos + moveVec;
		} 
		
		else{
			if((targetPos - pos).magnitude < moveSpeed * Time.fixedDeltaTime){
				pos = targetPos;
				moving = false;
				
			} else{
				pos += (targetPos - pos).normalized * moveSpeed * Time.fixedDeltaTime;
			}
			
			
		}
		
	}
	
	public void CheckForAction(){
		if (Physics.Raycast (GetRay (), out hitInfo, 1f, GetLayerMask (new string[] {"NPC"}))) {
			print ("hitINFO: " + hitInfo + "           ");

			bool npcFound = false;
			if (!npcFound){
				try {
					npcFound = true;
					NPC npc = hitInfo.collider.gameObject.GetComponent<NPC> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
						npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_david npc = hitInfo.collider.gameObject.GetComponent<NPC_david> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_kim npc = hitInfo.collider.gameObject.GetComponent<NPC_kim> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_marvin npc = hitInfo.collider.gameObject.GetComponent<NPC_marvin> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_sergey npc = hitInfo.collider.gameObject.GetComponent<NPC_sergey> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_jenna npc = hitInfo.collider.gameObject.GetComponent<NPC_jenna> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_keith npc = hitInfo.collider.gameObject.GetComponent<NPC_keith> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_hugh npc = hitInfo.collider.gameObject.GetComponent<NPC_hugh> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
			if (!npcFound){
				try {
					npcFound = true;
					NPC_gibson npc = hitInfo.collider.gameObject.GetComponent<NPC_gibson> ();
					if (npc.transform.position.x == Mathf.Floor (npc.transform.position.x) &&
					    npc.transform.position.y == Mathf.Floor (npc.transform.position.y)) {
						npc.FacePlayer (direction);
						npc.PlayDialog ();
					}
				} catch {
					npcFound = false;
				}
			}
		}
		
		if (Physics.Raycast (GetRay (), out hitInfo, 1f, GetLayerMask (new string[] {"Pickup"}))) {
			//picking up item
			print("Pickup Item " + hitInfo + "           ");
			Item item = hitInfo.collider.gameObject.GetComponent<Item>();
			item.PlayDialog();
			item.enabled = false;
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
	
	public void MoveThroughDoor(Vector3 doorLoc){
		if (doorLoc.z <= 0)
			doorLoc.z = transform.position.z;
		moving = false;
		moveVec = Vector3.zero;
		transform.position = doorLoc;
		doorCurLoc = transform.position;
		
	}
}
