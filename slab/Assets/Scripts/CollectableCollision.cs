using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour {

	private bool followPlayer = false;
	private PlayerControl player;
	private ParticleSystem ps;
	private bool following = false;
	private bool facingRight = true;
	private bool dead = false;
	private float deathTime = 1f;
	
	public Sprite alternate;

	private SpriteRenderer renderer;

	void OnCollisionEnter2D(Collision2D col){
         
        if (col.gameObject.tag == "Player"){
        	player = col.gameObject.GetComponent<PlayerControl>();
            player.pickupCollectible(this.gameObject);
            followPlayer = true;
			this.collider2D.enabled = false;
            ps.Stop();
         }
	}
	// Use this for initialization
	void Start () {
    	ps = GetComponent<ParticleSystem>();
		renderer = GetComponent<SpriteRenderer>();
		if(Random.Range(0, 10) < 5){
			renderer.sprite = alternate;
		}
	}

	// Update is called once per frame
	void Update () {
    
	    if(followPlayer){
	        //transform.position = player.transform.position;
	    } else {
			ps.Emit(1);
	    }

		if(dead){
			if(deathTime < 0){
				Live ();
			} else{
				deathTime -= Time.deltaTime;
			}
		}
	
	}

        void FixedUpdate(){
            if(followPlayer){
                Vector3 followVector = player.transform.position - transform.position;
                rigidbody2D.AddForce(new Vector2( followVector.x + ( 5f * Random.Range(-1f, 1f)), followVector.y + (5f * Random.Range(-1f, 1f))) );
                if(rigidbody2D.velocity.x > 0.1 && !facingRight) {
                    FlipMe();
                }
                else if(rigidbody2D.velocity.x < -0.1 && facingRight) {
                    FlipMe();
                }

            }
			if(dead){
				rigidbody2D.AddForce(Vector2.up * 4);
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

	public void Die(){
		followPlayer = false;
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
		dead = true;
		this.collider2D.enabled = true;
	}

	public void Live(){
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
		this.collider2D.enabled = true;
		dead = false;
	}

	public bool isDead(){
		return dead;
	}
}
