using UnityEngine;
using System;
using System.Collections;

public class Score : MonoBehaviour {

        private PlayerControl player;
        private GUIText uiText;

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

            if(uiText == null)
                uiText = transform.root.GetComponent<GUIText>();
	
            uiText.text = "x " + player.getTreasureCount();
	}
}
