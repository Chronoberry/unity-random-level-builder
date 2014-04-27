using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

        private PlayerControl player;
        private Vector3 healthScale;

	// Use this for initialization
	void Start () {
	
            healthScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

            Debug.Log(transform.localScale);
            transform.localScale = new Vector3( healthScale.x * player.getCurrentHealth() * 0.01f, transform.localScale.y, 0f);
	
	}
}
