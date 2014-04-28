using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
        
        public AudioClip[] clips;

	// Use this for initialization
	void Start () {
	    Messenger.AddListener("take damage", playOuch);
            Messenger.AddListener("pickup treasure", playQuack);
	    Messenger.AddListener("level up", playLevelUp);
	    Messenger.AddListener("bonus speed", playWoosh);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

        void playOuch(){
            AudioSource.PlayClipAtPoint(clips[14], transform.position);
        }

        void playLevelUp(){
            AudioSource.PlayClipAtPoint(clips[0], transform.position);
        }

        void playQuack(){
            AudioSource.PlayClipAtPoint(clips[12], transform.position);
        }

        void playWoosh(){
            AudioSource.PlayClipAtPoint(clips[11], transform.position);
        }

        
}
