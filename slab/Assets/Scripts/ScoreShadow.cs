﻿using UnityEngine;
using System.Collections;

public class ScoreShadow : MonoBehaviour {

    public GameObject guiCopy;		// A copy of the score.


    void Awake() {
        // Set the position to be slightly down and behind the other gui.
        Vector3 behindPos = transform.position;
        behindPos = new Vector3(guiCopy.transform.position.x - 0.09f, guiCopy.transform.position.y - 0.005f, guiCopy.transform.position.z - 1);
        transform.position = behindPos;
    }


    void Update() {
        // Set the text to equal the copy's text.
        guiText.text = guiCopy.guiText.text;
    }
}
