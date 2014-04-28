using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {
    //Check for collision with the player
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerControl>().getTreasureCount() > 0) {
				Messenger.Broadcast("level up");
			}
            col.gameObject.GetComponent<PlayerControl>().dropOffCollectibles();
        }
    }
    // Use this for initialization
    void Start () {
    }

    void Awake() {
    }
    
    // Update is called once per frame
    void Update () {
    }
}
