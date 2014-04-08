using UnityEngine;
using System.Collections;

public class GUIEndingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//guiTexture.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

if (TimerScript.timeRemaining > 1) {
						guiTexture.enabled = false;
				} else {
			guiTexture.enabled = true;
				}

	
	}
}
