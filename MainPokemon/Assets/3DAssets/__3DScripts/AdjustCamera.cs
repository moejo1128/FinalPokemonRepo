using UnityEngine;
using System.Collections;

public class AdjustCamera : MonoBehaviour {

	static public AdjustCamera S;
	public float camSpeed;

	public void AdjustCam(bool isMoving){
		if (isMoving) {
			Vector3 temp = transform.position;
			temp.z = temp.z + 3;
			transform.position = Vector3.Lerp (transform.position, temp, camSpeed * Time.fixedTime);
		} else {
			print ("NVM");
		
		}



	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Zooming",1f , .025f);
	
	}

	public void Zooming(){
		Camera.main.fieldOfView--;
		if (Camera.main.fieldOfView == 60) {
			CancelInvoke();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
