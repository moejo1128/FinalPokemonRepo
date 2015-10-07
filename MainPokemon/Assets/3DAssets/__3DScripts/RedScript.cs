using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedScript : MonoBehaviour 
{
	Animator anim;

	//all public gameobjects that will be triggered by redscript
	public GameObject Pokeball;
	public GameObject ballHeld;
	public GameObject FireMist;
	public GameObject Charmander;
	public GameObject newWorld;
	public GameObject hmmPoke;
	public GameObject thisBucket;
	public GameObject pickUpX;
	public GameObject hotFire;
	public GameObject bucket;
	public GameObject fireOut;
	public GameObject retChar;
	public GameObject missingText;
	public GameObject TBC;

	//public renderers for visuals of gameobjects 
	public Renderer newWorldRend;	
	public Renderer XRend;	
	public Renderer fireRend;
	public Renderer outRend;
	public Renderer charRend;
	public Renderer missingRend;
	public Renderer hmmRend;
	public Renderer bucketRend;
	public Renderer TBCRend;

	public float ThrowDistance;

	public AudioSource source;

	//bools needed to manipulate the visuals
	public bool foundPokeball;
	public bool havePokeball;

	public bool foundBucket;
	public bool haveBucket;

	public bool inFire;
	public bool seeChar;

	void Start ()
	{
		//getting animator
		anim = GetComponent<Animator>();
		//retrieving renderers from gameobjects
		newWorldRend = newWorld.GetComponent<Renderer> ();
		newWorldRend.enabled = true;

		hmmRend = hmmPoke.GetComponent<Renderer> ();
		bucketRend = thisBucket.GetComponent<Renderer> ();
		TBCRend = TBC.GetComponent<Renderer> ();

		//playing rain
		source = GetComponent<AudioSource>();
		source.Play ();
	}
	
	
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.Q)) {
			//quit
			Application.Quit();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			//restart
			Application.LoadLevel("SceneRed3D");
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			//move to object 1. Pokeball
			transform.position = new Vector3(300f,.07f,100f);
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			//move to obective 2. fire
			transform.position = new Vector3(269f,.07f,264f);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			//move to obective 3. bucket
			transform.position = new Vector3(236f,.07f,316f);
		}

		if(Input.GetKeyDown(KeyCode.X) && foundPokeball){
			//picking up pokeball
			print("Picked up Pokeball");
			havePokeball = true;
			Pokeball = GameObject.Find("Pokeball");
			Pokeball.SetActive(false);
			XRend.enabled = false;
			ballHeld.SetActive(true);
			foundPokeball = false;
			hmmRend.enabled = true;
		}

		if(Input.GetKeyDown(KeyCode.X) && foundBucket){
			//picking up bucket
			print("Picked up Bucket");
			haveBucket = true;
			bucket.SetActive(false);
			XRend.enabled = false;
			foundBucket = false;
			bucketRend.enabled = true;	
		}

		if (Input.GetKeyDown (KeyCode.X) && haveBucket && inFire) {
			//using bucket on fire
			print("Put out Fire!");
			Charmander.SetActive(true);
			FireMist.SetActive(false);
			outRend.enabled = false;
			
		}

		if (Input.GetKeyDown (KeyCode.X) && havePokeball && seeChar) {
			//catching charmander
			Charmander.SetActive(false);
			charRend.enabled = false;
			TBCRend.enabled = true;
		}
		
		
		//rotating left and right
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate(Vector3.down*1.5f, Space.World);
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate(Vector3.down*(-1.5f), Space.World);
		}

		//bool logic for animators
		if (Input.GetKey (KeyCode.UpArrow)) {
			print ("UpArrow");
			anim.SetBool ("isMoving", true);
			anim.SetBool ("Right", false);			
			anim.SetBool ("Up", true);
			anim.SetBool ("Left", false);
			
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			anim.SetBool ("isMoving", true);
			anim.SetBool ("Up", false);			
			anim.SetBool ("Right", true);
			anim.SetBool ("Left", false);
		}

		if (Input.GetKey(KeyCode.LeftArrow)){
			anim.SetBool ("isMoving", true);
			anim.SetBool ("Up", false);			
			anim.SetBool ("Right", false);
			anim.SetBool ("Left", true);
		}

		if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.RightArrow)){
			anim.SetBool ("isMoving", false);
			anim.SetBool ("Up", false);
			anim.SetBool ("Right", false);
			anim.SetBool ("Left", false);
		}

	}

	
	void OnTriggerEnter(Collider other){
		//what to do when in contact with other
		if(other.name == "Pokeball"){
			print("We're Here");
			foundPokeball = true;
			XRend = pickUpX.GetComponent<Renderer>();
			XRend.enabled = true;
		}

		else if(other.name == "FireMist" && !haveBucket){
			print("We're Here");
			inFire = true;
			fireRend = hotFire.GetComponent<Renderer>();
			fireRend.enabled = true;
		}


		else if(other.name == "FireMist" && haveBucket){
			print("We're Here");
			inFire = true;
			outRend = fireOut.GetComponent<Renderer>();
			outRend.enabled = true;
		}
		
		else if(other.name == "Bucket"){
			print("We're Here");
			foundBucket = true;
			XRend = pickUpX.GetComponent<Renderer>();
			XRend.enabled = true;
		}

		else if(other.name == "Charmander" && havePokeball){
			print("We're Here");
			charRend = retChar.GetComponent<Renderer>();
			charRend.enabled = true;
			seeChar = true;
		}

		else if(other.name == "Charmander" && !havePokeball){
			print("We're Here");
			missingRend = missingText.GetComponent<Renderer>();
			missingRend.enabled = true;
		}
		
	}
	
	void OnTriggerExit(Collider other){
		//what to do when leaving other
		if(other.name == "Pokeball"){
			print("We Left");
			foundPokeball = false;
			XRend = pickUpX.GetComponent<Renderer>();
			XRend.enabled = false;
		}

		else if(other.name == "FireMist"){
			print("We're Here");
			inFire = false;
			fireRend = hotFire.GetComponent<Renderer>();
			fireRend.enabled = false;
		}

		else if(other.name == "FireMist" && haveBucket){
			print("We're Here");
			inFire = false;
			outRend = fireOut.GetComponent<Renderer>();
			outRend.enabled = false;
		}
		
		if(other.name == "Bucket"){
			print("We Left");
			foundBucket = false;
			XRend = pickUpX.GetComponent<Renderer>();
			XRend.enabled = false;
		}

		else if(other.name == "Charmander" && havePokeball){
			print("We're Here");
			foundBucket = true;
			charRend = retChar.GetComponent<Renderer>();
			charRend.enabled = false;
			seeChar = false;
		}

		else if(other.name == "Charmander" && !havePokeball){
			print("We're Here");
			missingRend = missingText.GetComponent<Renderer>();
			missingRend.enabled = false;
		}
	}

}