using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start () {
        Vector3 playerStartPosition = new Vector3( transform.position.x, transform.position.y - 1, 0f);
        player = (GameObject)Instantiate( player, playerStartPosition, Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update () {
    }

}
