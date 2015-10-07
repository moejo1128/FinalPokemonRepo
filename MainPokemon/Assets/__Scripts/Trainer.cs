using UnityEngine;
using System.Collections;


public class Trainer : MonoBehaviour {

	public float castDist;
	public RaycastHit hitInfo;

	public Direction direction;
	public float walkSpeed;

	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public SpriteRenderer sprend;


	public Vector3 pos{
		get { return transform.position;}
		set { transform.position = value;}
	}


	void FixedUpdate(){

		//GoUp ();
 		if(Physics.Raycast(GetRay(), out hitInfo, castDist, GetLayerMask(new string[] {"Player"})) && 
		   ((Player.S.transform.position.x+Player.S.transform.position.y)%2 == 1 || 
		   (Player.S.transform.position.x+Player.S.transform.position.y)%2 == 0 )){

			print("I SEE YOU!!");
			Player.S.moving = false;

			StartCoroutine(Wait2Sec(1f));
		}
	}

	Ray GetRay(){
		//print ("my Direction: " + direction);
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
	
	IEnumerator Wait2Sec(float duration){
		yield return new WaitForSeconds(duration);   //Wait
		//Debug.Log("End Wait() function and the time is: "+Time.time);

		Player player = hitInfo.collider.gameObject.GetComponent<Player>();
		Vector3 gotoPlayer = player.transform.position;
		
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

		transform.position = Vector3.MoveTowards(transform.position, gotoPlayer, walkSpeed);
		Invoke("DisableCast", 3);
	}

}
