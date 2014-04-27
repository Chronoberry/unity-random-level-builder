using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

        private PlayerControl player;
        private GUIText uiText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

            if(uiText == null)
                uiText = transform.root.GetComponent<GUIText>();
	
            uiText.text = "x " + player.getTreasureCount();
	}
}
