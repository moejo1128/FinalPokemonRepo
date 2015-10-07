using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour {


	public AudioSource source;
	public AudioClip clip;

	public bool isPlayed;

	void Awake(){
		source = GetComponent<AudioSource> ();
		isPlayed = false;

	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Player") {
			print (source.name + " is now playing.");
			if (!isPlayed) {
				CancelInvoke();
				source.volume = .5f;
				source.Play ();	
				isPlayed = true;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.name == "Player") {
		
			print (source.name + " is over.");
		
			if (source.isPlaying) {
				InvokeRepeating ("fadeOut", .1f, .1f);
				isPlayed = false;

			}
		}
	}
	
	void fadeOut(){
		source.volume -= 1 * Time.deltaTime;
		//print (source.volume);

		if (source.volume <= 0) {

			source.Stop();
			CancelInvoke();
			source.volume = .5f;
		}
	}

}
