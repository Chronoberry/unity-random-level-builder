using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

    public GameObject player;

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
        spawnPlayer();

    }
    
    // Update is called once per frame
    void Update () {
        checkForDeath();
    }
	
    void checkForDeath(){
        if(player.GetComponent<PlayerControl>().getCurrentHealth() <= 0){
            player.transform.position = transform.position;
            player.GetComponent<PlayerControl>().resetHealth();
        }
    }

    void spawnPlayer(){
        Vector3 playerStartPosition = new Vector3(transform.position.x, transform.position.y-2, 0f);
        player = (GameObject)Instantiate(player, playerStartPosition, Quaternion.identity);
    }

}
