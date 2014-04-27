using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

        private PlayerControl player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

            guiText.text = "Score : " + player.getScore();
	}
}
