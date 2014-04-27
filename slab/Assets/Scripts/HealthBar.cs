using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

        private PlayerControl player;
        private Vector3 healthScale;
        private float origX;

	// Use this for initialization
	void Start () {
	    origX = transform.position.x;
            healthScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

            transform.localScale = new Vector3( healthScale.x * player.getCurrentHealth() * 0.01f, transform.localScale.y, 0f);
	    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
}
