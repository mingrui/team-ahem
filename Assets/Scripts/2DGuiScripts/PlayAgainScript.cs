using UnityEngine;
using System.Collections;

public class PlayAgainScript : MonoBehaviour {

	public Texture orange;
	public Texture yellow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (TimerScript.timeRemaining > 1) {
			guiTexture.enabled = false;
		} else {
			guiTexture.enabled = true;
			Screen.showCursor = true;
		}
	
	}

	void OnMouseEnter() {
		guiTexture.texture = yellow;
	}
	
	void OnMouseExit() {
		guiTexture.texture = orange;
	}

	void OnMouseDown(){
		Time.timeScale = 1;
		StopDetectionScript.minionDead = 0;
		StopDetectionScript.minionStopCount = 0;
		StopDetectionScript.score = 1000;
		MinionHomeScript.minionCount = 0;
		TimerScript.timeRemaining = 240f;
		TileMakeScript.maxMinions = 30;
		Application.LoadLevel("Master_Scene_1");

		}
}
