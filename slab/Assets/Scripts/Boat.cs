using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

    public GameObject player;

    //Check for collision with the player
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<PlayerControl>().dropOffCollectibles();
        }
    }
    // Use this for initialization
    void Start () {
        spawnPlayer();
    }
    
    // Update is called once per frame
    void Update () {
        checkForDeath();
    }

    void checkForDeath(){
        if(player.GetComponent<PlayerControl>().getCurrentHealth() <= 0){
            //Destroy(player);
            //spawnPlayer();
            player.transform.position = transform.position;
            player.GetComponent<PlayerControl>().resetHealth();
        }
    }

    void spawnPlayer(){
        Vector3 playerStartPosition = new Vector3( transform.position.x, transform.position.y - 2, 0f);
        player = (GameObject)Instantiate( player, playerStartPosition, Quaternion.identity);
    }

}
