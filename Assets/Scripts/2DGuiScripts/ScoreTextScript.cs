﻿using UnityEngine;
using System.Collections;

public class ScoreTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		guiText.text = "Score: " + StopDetectionScript.score.ToString ();
	
	}
}
