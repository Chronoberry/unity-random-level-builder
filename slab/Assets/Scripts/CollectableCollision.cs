using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour {

        private bool followPlayer = false;
        private PlayerControl player;
        private ParticleSystem ps;

	void OnCollisionEnter2D(Collision2D col){
         
        if (col.gameObject.tag == "Player"){
        	player = col.gameObject.GetComponent<PlayerControl>();
            player.pickupCollectible(this.gameObject);
            followPlayer = true;
            Destroy(rigidbody2D);
            Destroy(this.collider2D);
            ps.Stop();
         }
	}
	// Use this for initialization
	void Start () {
            ps = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
    
            if(followPlayer){
                transform.position = player.transform.position;
            } else {
                ps.Emit(2);
            }
	
	}
}
