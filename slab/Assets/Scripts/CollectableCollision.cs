using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour {

        private bool followPlayer = false;
        private PlayerControl player;
        private ParticleSystem ps;
        private bool following = false;
        private bool facingRight = true;

	void OnCollisionEnter2D(Collision2D col){
         
        if (col.gameObject.tag == "Player"){
        	player = col.gameObject.GetComponent<PlayerControl>();
            player.pickupCollectible(this.gameObject);
            followPlayer = true;
            //Destroy(rigidbody2D);
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
                //transform.position = player.transform.position;
            } else {
                ps.Emit(2);
            }
	
	}

        void FixedUpdate(){
            if(followPlayer){
                Vector3 followVector = 5f * (player.transform.position - transform.position);
                rigidbody2D.AddForce(new Vector2( followVector.x + ( 7f * Random.Range(-1f, 1f)), followVector.y + (7f * Random.Range(-1f, 1f))) );
				float flipThreshold = 0.5f;
				if(rigidbody2D.velocity.x > flipThreshold && !facingRight) {
                    FlipMe();
                }
				else if(rigidbody2D.velocity.x < -flipThreshold && facingRight) {
                    FlipMe();
                }

            }
        }

        void FlipMe () {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
	}
}
