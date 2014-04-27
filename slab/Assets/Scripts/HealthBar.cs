using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

        private PlayerControl player;
        private Vector3 healthScale;
        private float origX;
        private SpriteRenderer sprite;
        private ParticleSystem ps;

	// Use this for initialization
	void Start () {
            healthScale = transform.localScale;
            sprite = GetComponent<SpriteRenderer>();
            ps = GetComponent<ParticleSystem>();
            ps.Stop();

	    Messenger.AddListener("take damage", takeDamage);
	}
	
	// Update is called once per frame
	void Update () {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, (float)player.getCurrentHealth() / 100f);
	}
        
        void takeDamage(){

            ps.Emit(1);
        }
}
