using UnityEngine;
using System.Collections;

public class OctoObstacle : MonoBehaviour {

    public float stunDuration = 2000f;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerControl>().stunPlayer(stunDuration);
            col.gameObject.GetComponent<PlayerControl>().takeDamage(10);
        }
    }
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
