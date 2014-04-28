using UnityEngine;
using System.Collections;

public class OctoObstacle : MonoBehaviour {

    public float stunDuration = 2000f;
    public int damage = 2;
    private ParticleSystem ps;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerControl>().stunPlayer(stunDuration);
            col.gameObject.GetComponent<PlayerControl>().takeDamage(damage);
            ps.Emit(100);
        }
    }
    // Use this for initialization
    void Start () {
    
        ps = GetComponent<ParticleSystem>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
