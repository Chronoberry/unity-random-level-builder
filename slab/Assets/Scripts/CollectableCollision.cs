using UnityEngine;
using System.Collections;

public class CollectableCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<PlayerControl>().pickupCollectible("Treasure");
			Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
