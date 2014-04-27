using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

    public GameObject player;

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<PlayerControl>().dropOffCollectibles();
		}
	}
    // Use this for initialization
    void Start () {
        Vector3 playerStartPosition = new Vector3( transform.position.x, transform.position.y - 2, 0f);
        player = (GameObject)Instantiate( player, playerStartPosition, Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update () {
    }

}
