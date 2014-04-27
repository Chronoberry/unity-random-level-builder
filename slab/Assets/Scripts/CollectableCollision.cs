using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour {

        private bool followPlayer = false;
        private PlayerControl player;

	void OnCollisionEnter2D(Collision2D col){
            Debug.Log (col.gameObject.tag);
            if (col.gameObject.tag == "Player"){
                player = col.gameObject.GetComponent<PlayerControl>();
                player.pickupCollectible(this.gameObject);
                followPlayer = true;
                Destroy(rigidbody2D);
                Destroy(this.collider2D);
                
                //Destroy(this.gameObject);
            }
	}
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
    
            if(followPlayer){
                transform.position = player.transform.position;
            }
	
	}
}
