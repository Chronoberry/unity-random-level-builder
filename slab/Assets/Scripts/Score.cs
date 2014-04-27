using UnityEngine;
using System;
using System.Collections;

public class Score : MonoBehaviour {

        private PlayerControl player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
            if (player == null) {
				try {
					player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
				}
				catch (Exception e) {
					;
				}
			}

            guiText.text = "Score : " + player.getScore();
	}
}
